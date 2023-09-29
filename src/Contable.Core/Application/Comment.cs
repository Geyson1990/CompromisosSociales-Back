using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppComments")]
   public class Comment : FullAuditedEntity<long>
   {
        [Column(TypeName = CommentsConst.DescriptionType)]
        public string Description { get; set; }
        public User User { get; set; }
        public CommentType Type { get; set; }
        public TaskManagement TaskManagement { get; set; }
   }
}
