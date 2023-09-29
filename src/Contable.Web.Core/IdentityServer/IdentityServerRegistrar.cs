using System;
using Abp.IdentityServer4;
using IdentityServer4.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Contable.Authorization.Users;
using Contable.EntityFrameworkCore;

namespace Contable.Web.IdentityServer
{
    public static class IdentityServerRegistrar
    {
        public static void Register(IServiceCollection services, IConfigurationRoot configuration, Action<IdentityServerOptions> setupOptions)
        {
            services.AddIdentityServer(setupOptions)
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients(configuration))
                .AddAbpPersistedGrants<ContableDbContext>()
                .AddAbpIdentityServer<User>();
        }

        public static void Register(IServiceCollection services, IConfigurationRoot configuration)
        {
            Register(services, configuration, options => { });
        }
    }
}
