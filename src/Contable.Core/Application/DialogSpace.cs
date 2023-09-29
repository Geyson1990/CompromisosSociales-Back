using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppDialogSpaces")]
    public class DialogSpace : FullAuditedEntity
    {
        [Column(TypeName = DialogSpaceConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = DialogSpaceConsts.YearType)]
        public int Year { get; set; }

        [Column(TypeName = DialogSpaceConsts.CountType)]
        public int Count { get; set; }

        [Column(TypeName = DialogSpaceConsts.GenerationType)]
        public bool Generation { get; set; }

        [Column(TypeName = DialogSpaceConsts.CaseNameType)]
        public string CaseName { get; set; }

        [Column(TypeName = DialogSpaceConsts.DialogSpaceTypeIdType)]
        [ForeignKey("DialogSpaceType")]
        public int DialogSpaceTypeId { get; set; }
        public DialogSpaceType DialogSpaceType { get; set; }

        [Column(TypeName = DialogSpaceConsts.SocialConflictIdType)]
        [ForeignKey("SocialConflict")]
        public int? SocialConflictId { get; set; }
        public SocialConflict SocialConflict { get; set; }

        [Column(TypeName = DialogSpaceConsts.PersonIdType)]
        [ForeignKey("Person")]
        public int? PersonId { get; set; }
        public Person Person { get; set; }

        [Column(TypeName = DialogSpaceConsts.LastDialogSpaceDocumentIdType)]
        [ForeignKey("DialogSpaceDocument")]
        public int? LastDialogSpaceDocumentId { get; set; }

        public List<DialogSpaceDocument> Documents { get; set; }
        public List<DialogSpaceLocation> Locations { get; set; }
        public List<DialogSpaceLeader> Leaders { get; set; }
    }
}