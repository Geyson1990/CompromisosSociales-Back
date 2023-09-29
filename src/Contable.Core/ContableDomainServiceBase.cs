using Abp.Domain.Services;

namespace Contable
{
    public abstract class ContableDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected ContableDomainServiceBase()
        {
            LocalizationSourceName = ContableConsts.LocalizationSourceName;
        }
    }
}
