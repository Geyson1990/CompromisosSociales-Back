using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public int? SectorMeetId { get; set; }
        public int? State { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrWhiteSpace(Sorting))
                Sorting = "SessionTime DESC";
        }
    }
}
