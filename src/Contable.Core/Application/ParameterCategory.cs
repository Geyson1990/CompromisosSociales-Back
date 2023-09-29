
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contable.Application
{
    [Table("AppParameterCategory")]
    public class ParameterCategory : FullAuditedEntity
    {
        [Column(TypeName = ParameterConsts.CodeType)]
        public string Code { get; set; }

        [Column(TypeName = ParameterConsts.NameType)]
        public string Name { get; set; }

        public List<Parameter> Parameters { get; set; }
    }
}
