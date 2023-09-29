using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Contable
{
    [DependsOn(typeof(ContableCoreSharedModule))]
    public class ContableApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ContableApplicationSharedModule).GetAssembly());
        }
    }
}