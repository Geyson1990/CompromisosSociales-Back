using Microsoft.Extensions.Configuration;

namespace Contable.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
