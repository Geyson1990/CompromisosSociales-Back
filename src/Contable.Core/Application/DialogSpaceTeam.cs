using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceTeams")]
    public class DialogSpaceTeam : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceTeamConsts.DialogSpaceLeaderIdType)]
        [ForeignKey("DialogSpaceLeader")]
        public int DialogSpaceLeaderId { get; set; }
        public DialogSpaceLeader DialogSpaceLeader { get; set; }

        [Column(TypeName = DialogSpaceTeamConsts.NameType)]
        public string Name { get; set; }
    }
}
