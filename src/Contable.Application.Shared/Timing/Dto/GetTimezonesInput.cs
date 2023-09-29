using Abp.Configuration;

namespace Contable.Timing.Dto
{
    public class GetTimezonesInput
    {
        public SettingScopes DefaultTimezoneScope { get; set; }
    }
}
