using Abp.Domain.Entities.Auditing;
using Contable.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppPersons")]
    public class Person : FullAuditedEntity
    {
        [Column(TypeName = PersonConsts.DocumentType)]
        public string Document { get; set; }

        [Column(TypeName = PersonConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = PersonConsts.NamesType)]
        public string Names { get; set; }

        [Column(TypeName = PersonConsts.Surname2Type)]
        public string Surname { get; set; }

        [Column(TypeName = PersonConsts.Surname2Type)]
        public string Surname2 { get; set; }

        [Column(TypeName = PersonConsts.PersonType)]
        public PersonType Type { get; set; }

        [Column(TypeName = PersonConsts.EmailAddressType)]
        public string EmailAddress { get; set; }

        [Column(TypeName = PersonConsts.EnabledType)]
        public bool Enabled { get; set; }

        [Column(TypeName = PersonConsts.ParentIdType)]
        public int? ParentId { get; set; }

        public User User { get; set; }

        [Column(TypeName = PersonConsts.TerritorialUnitIdType)]
        [ForeignKey("TerritorialUnit")]
        public int? TerritorialUnitId { get; set; }
        public TerritorialUnit TerritorialUnit { get; set; }

        public List<TerritorialUnitCoordinator> TerritorialUnits { get; set; }

        [Column(TypeName = PersonConsts.EnabledType)]
        public bool AlertSend { get; set; }
    }
}
