using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Contable
{
    public class ContableCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ContableCoreSharedModule).GetAssembly());
        }
    }
}