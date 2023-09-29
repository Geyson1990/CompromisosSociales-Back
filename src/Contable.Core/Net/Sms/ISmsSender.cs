using System.Threading.Tasks;

namespace Contable.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}