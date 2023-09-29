using System.Collections.Generic;

namespace Contable.Web.Common
{
    public static class WebConsts
    {
        public const string SwaggerUiEndPoint = "/swagger";
        public const string HangfireDashboardEndPoint = "/hangfire";

        public static bool SwaggerUiEnabled = true;
        public static bool HangfireDashboardEnabled = false;

        public static List<string> ReCaptchaIgnoreWhiteList = new List<string>
        {
            ContableConsts.AbpApiClientUserAgent
        };
    }
}
