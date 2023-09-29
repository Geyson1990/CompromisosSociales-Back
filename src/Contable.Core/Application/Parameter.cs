using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;


namespace Contable.Application
{
    [Table("AppParameter")]
    public class Parameter : FullAuditedEntity
    {
        [Column(TypeName = ParameterConsts.ValueType)]
        public string Value { get; set; }

        [Column(TypeName = ParameterConsts.ParentType)]
        public int ParentId { get; set; }

        [ForeignKey("ParameterCategory")]
        public int ParameterCategoryId { get; set; }
        public ParameterCategory ParameterCategory { get; set; }

        [Column(TypeName = ParameterConsts.OrderType)] 
        public int Order { get; set; }

        [Column(TypeName = ParameterConsts.Type)]
        public ParameterType Type { get; set; }

        [Column(TypeName = ParameterConsts.StepType)]
        public ParameterStep Step { get; set; }
    }
}
