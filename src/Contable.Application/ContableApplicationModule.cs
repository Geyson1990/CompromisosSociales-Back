using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Contable.Authorization;

namespace Contable
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(ContableApplicationSharedModule),
        typeof(ContableCoreModule)
        )]
    public class ContableApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ContableApplicationModule).GetAssembly());
        }
    }
}