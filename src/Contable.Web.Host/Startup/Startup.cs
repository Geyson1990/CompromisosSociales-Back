using System.IO;
using System;
using System.Linq;
using System.Reflection;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.AspNetZeroCore.Web.Authentication.JwtBearer;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using Abp.Hangfire;
using Abp.PlugIns;
using Castle.Facilities.Logging;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Contable.Authorization;
using Contable.Configuration;
using Contable.EntityFrameworkCore;
using Contable.Identity;
using Contable.Web.Chat.SignalR;
using Contable.Web.Common;
using Swashbuckle.AspNetCore.Swagger;
using Contable.Web.IdentityServer;
using Contable.Web.Swagger;
using Stripe;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using HealthChecks.UI.Client;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Contable.Web.HealthCheck;
using Newtonsoft.Json.Serialization;
using Owl.reCAPTCHA;
using HealthChecksUISettings = HealthChecks.UI.Configuration.Settings;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Contable.Web.Configuration;

namespace Contable.Web.Startup
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson();

            services.AddSignalR();
            services.AddControllers(options =>
            {
                options.Conventions.Add(new AppControllerDocumentationConvention());
            });

            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            if (bool.Parse(_appConfiguration["KestrelServer:IsEnabled"]))
            {
                ConfigureKestrel(services);
            }

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            //Identity server
            if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            {
                IdentityServerRegistrar.Register(services, _appConfiguration, options =>
                     options.UserInteraction = new UserInteractionOptions()
                     {
                         LoginUrl = "/UI/Login",
                         LogoutUrl = "/UI/LogOut",
                         ErrorUrl = "/Error"
                     });
            }

            if (WebConsts.SwaggerUiEnabled)
            {
                //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo() { Title = "GConflictos API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.ParameterFilter<SwaggerEnumParameterFilter>();
                    options.SchemaFilter<SwaggerEnumSchemaFilter>();
                    options.OperationFilter<SwaggerOperationIdFilter>();
                    options.OperationFilter<SwaggerOperationFilter>();
                    options.CustomDefaultSchemaIdSelector();
                }).AddSwaggerGenNewtonsoftSupport();
            }

            //Recaptcha
            services.AddreCAPTCHAV3(x =>
            {
                x.SiteKey = _appConfiguration["Recaptcha:SiteKey"];
                x.SiteSecret = _appConfiguration["Recaptcha:SecretKey"];
            });

            if (WebConsts.HangfireDashboardEnabled)
            {
                //Hangfire(Enable to use Hangfire instead of default job manager)
                services.AddHangfire(config =>
                {
                    config.UseSqlServerStorage(_appConfiguration.GetConnectionString("Default"));
                });
            }

            if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
            {
                services.AddAbpZeroHealthCheck();

                var healthCheckUISection = _appConfiguration.GetSection("HealthChecks")?.GetSection("HealthChecksUI");

                if (bool.Parse(healthCheckUISection["HealthChecksUIEnabled"]))
                {
                    services.Configure<HealthChecksUISettings>(settings =>
                    {
                        healthCheckUISection.Bind(settings, c => c.BindNonPublicProperties = true);
                    });
                    services.AddHealthChecksUI();
                }
            }

            //Configure Abp and Dependency Injection
            return services.AddAbp<ContableWebHostModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig(_hostingEnvironment.IsDevelopment()
                            ? "log4net.config"
                            : "log4net.Production.config")
                );

                options.PlugInSources.AddFolder(Path.Combine(_hostingEnvironment.WebRootPath, "Plugins"), SearchOption.AllDirectories);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //Initializes ABP framework.
            app.UseAbp(options =>
            {
                options.UseAbpRequestLocalization = false; //used below: UseAbpRequestLocalization
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error?statusCode={0}");
                app.UseExceptionHandler("/Error");
            }

            app.Use(async (context, next) => { await next(); if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value)) { context.Request.Path = "/index.html"; await next(); } });
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(DefaultCorsPolicyName); //Enable CORS!

            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (bool.Parse(_appConfiguration["IdentityServer:IsEnabled"]))
            {
                app.UseJwtTokenMiddleware("IdentityBearer");
                app.UseIdentityServer();
            }

            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                if (scope.ServiceProvider.GetService<DatabaseCheckHelper>().Exist(_appConfiguration["ConnectionStrings:Default"]))
                {
                    app.UseAbpRequestLocalization();
                }
            }

            if (WebConsts.HangfireDashboardEnabled)
            {
                //Hangfire dashboard &server(Enable to use Hangfire instead of default job manager)
                app.UseHangfireDashboard(WebConsts.HangfireDashboardEndPoint, new DashboardOptions
                {
                    Authorization = new[] { new AbpHangfireAuthorizationFilter(AppPermissions.Pages_Administration_HangfireDashboard) }
                });
                app.UseHangfireServer();
            }

            if (bool.Parse(_appConfiguration["Payment:Stripe:IsActive"]))
            {
                StripeConfiguration.ApiKey = _appConfiguration["Payment:Stripe:SecretKey"];
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapHub<ChatHub>("/signalr-chat");

                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

                if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
                {
                    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });
                }
            });

            if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksEnabled"]))
            {
                if (bool.Parse(_appConfiguration["HealthChecks:HealthChecksUI:HealthChecksUIEnabled"]))
                {
                    app.UseHealthChecksUI();
                }
            }

            if (WebConsts.SwaggerUiEnabled)
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger();
                // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(_appConfiguration["App:SwaggerEndPoint"], "GConflictos API V1");
                    options.IndexStream = () => Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("Contable.Web.wwwroot.swagger.ui.index.html");
                    options.InjectBaseUrl(_appConfiguration["App:ServerRootAddress"]);
                }); //URL: /swagger
            }
        }

        private void ConfigureKestrel(IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
            {
                options.Listen(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 443),
                    listenOptions =>
                    {
                        var certPassword = _appConfiguration.GetValue<string>("Kestrel:Certificates:Default:Password");
                        var certPath = _appConfiguration.GetValue<string>("Kestrel:Certificates:Default:Path");
                        var cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(certPath, certPassword);
                        listenOptions.UseHttps(new HttpsConnectionAdapterOptions()
                        {
                            ServerCertificate = cert
                        });
                    });
            });
        }
    }
}
