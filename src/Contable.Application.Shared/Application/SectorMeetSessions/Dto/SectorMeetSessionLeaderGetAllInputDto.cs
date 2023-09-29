using Abp.Runtime.Validation;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.SectorMeetSessions.Dto
{
    public class SectorMeetSessionLeaderGetAllInputDto : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public int SectorMeetSessionId { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrWhiteSpace(Sorting))
                Sorting = "Id DESC";
        }
    }
}
