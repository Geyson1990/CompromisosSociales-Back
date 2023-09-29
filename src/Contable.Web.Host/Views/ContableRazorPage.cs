using Abp.AspNetCore.Mvc.Views;

namespace Contable.Web.Views
{
    public abstract class ContableRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ContableRazorPage()
        {
            LocalizationSourceName = ContableConsts.LocalizationSourceName;
        }
    }
}
