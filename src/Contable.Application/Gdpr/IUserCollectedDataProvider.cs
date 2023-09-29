using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Contable.Dto;

namespace Contable.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
