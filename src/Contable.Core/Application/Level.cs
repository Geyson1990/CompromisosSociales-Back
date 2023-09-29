using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application
{
    [Table("AppLevels")]
    public class Level : Entity
    {
        [Column(TypeName = LevelConsts.NameType)]
        public string Name { get; set; }

        [Column(TypeName = LevelConsts.Type)]
        public LevelType Type { get; set; }

        [Column(TypeName = LevelConsts.IndexType)]
        public int Index { get; set; }

        [Column(TypeName = LevelConsts.MinType)]
        public decimal Min { get; set; }

        [Column(TypeName = LevelConsts.MaxType)]
        public decimal Max { get; set; }

        [Column(TypeName = LevelConsts.ColorType)]
        public string Color { get; set; }
    }
}
