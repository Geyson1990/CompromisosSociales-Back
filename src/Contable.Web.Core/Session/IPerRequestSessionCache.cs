using System.Threading.Tasks;
using Contable.Sessions.Dto;

namespace Contable.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
