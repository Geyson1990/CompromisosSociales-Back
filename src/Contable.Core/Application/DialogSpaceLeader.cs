using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaceLeaders")]
    public class DialogSpaceLeader : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceLeaderConsts.DialogSpaceIdType)]
        [ForeignKey("DialogSpace")]
        public int DialogSpaceId { get; set; }
        public DialogSpace DialogSpace { get; set; }

        [Column(TypeName = DialogSpaceLeaderConsts.DirectoryGovernmentIdType)]
        [ForeignKey("DirectoryGovernment")]
        public int DirectoryGovernmentId { get; set; }
        public DirectoryGovernment DirectoryGovernment { get; set; }

        [Column(TypeName = DialogSpaceLeaderConsts.IndexType)]
        public int Index { get; set; }

        public List<DialogSpaceTeam> Teams { get; set; }
    }
}
