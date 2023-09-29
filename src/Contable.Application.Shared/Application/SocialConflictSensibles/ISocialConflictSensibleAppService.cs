using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SocialConflictSensibles.Dto;
using Contable.Application.Ubigeos.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SocialConflictSensibles
{
    public interface ISocialConflictSensibleAppService : IApplicationService
    {
        Task<EntityDto> Create(SocialConflictSensibleCreateDto input);
        Task Delete(EntityDto input);
        Task<PagedResultDto<SocialConflictSensibleGetAllDto>> GetAll(SocialConflictSensibleGetAllInputDto input);
        Task<SocialConflictSensibleGetDataDto> Get(NullableIdDto input);
        Task Update(SocialConflictSensibleUpdateDto input);
        Task<EntityDto> CreateNote(SocialConflictSensibleNoteCreateDto input);
        Task<EntityDto> UpdateNote(SocialConflictSensibleNoteUpdateDto input);
        Task DeleteNote(EntityDto input);
        Task<EntityDto> CreateResource(SocialConflictSensibleCreateResourceDto input);
        Task DeleteResource(EntityDto input);
        Task<FileDto> GetMatrizToExcel(SocialConflictSensibleGetAllInputDto input);
    }
}
