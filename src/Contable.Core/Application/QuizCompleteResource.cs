using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppQuizCompleteResources")]
    public class QuizCompleteResource : FullAuditedEntity
    {
        [Column(TypeName = QuizCompleteResourceConsts.QuizCompleteIdType)]
        [ForeignKey("QuizComplete")]
        public int QuizCompleteId { get; set; }
        public QuizComplete QuizComplete { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string CommonFolder { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string ResourceFolder { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string SectionFolder { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string FileName { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string Size { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string Extension { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string ClassName { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.AssetType)]
        public string Name { get; set; }

        [Column(TypeName = QuizCompleteResourceConsts.ResourceType)]
        public string Resource { get; set; }
    }
}
