using System.Linq;
using Abp.Application.Features;
using Microsoft.EntityFrameworkCore;
using Contable.Editions;
using Contable.EntityFrameworkCore;
using Contable.Features;
using Contable.Application;
using System.Collections.Generic;

namespace Contable.Migrations.Seed.Application
{
    public class DefaultLevelCreator
    {
        private readonly List<Level> DefaultPrimaryLevels = new List<Level>()
        {
            new Level()
            {
                Id = 0,
                Name = "Bajo",
                Index = 1,
                Min = (decimal)0.0,
                Max = (decimal)0.4,
                Type = LevelType.PRIMARY,
                Color = "#00B050"
            },
            new Level()
            {
                Id = 0,
                Name = "Medio",
                Index = 2,
                Min = (decimal)0.4,
                Max = (decimal)0.7,
                Type = LevelType.PRIMARY,
                Color = "#FFFF00"
            },
            new Level()
            {
                Id = 0,
                Name = "Alto",
                Index = 3,
                Min = (decimal)0.7,
                Max = (decimal)1.0,
                Type = LevelType.PRIMARY,
                Color = "#FF0000"
            }
        };

        private readonly List<Level> DefaultSecondaryLevels = new List<Level>()
        {
            new Level()
            {
                Id = 0,
                Name = "Nivel 01",
                Index = 1,
                Min = (decimal)0.0,
                Max = (decimal)0.1,
                Type = LevelType.SECONDARY,
                Color = "#1DC400"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 02",
                Index = 2,
                Min = (decimal)0.1,
                Max = (decimal)0.2,
                Type = LevelType.SECONDARY,
                Color = "#74D618"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 03",
                Index = 3,
                Min = (decimal)0.2,
                Max = (decimal)0.3,
                Type = LevelType.SECONDARY,
                Color = "#A6E612"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 04",
                Index = 4,
                Min = (decimal)0.3,
                Max = (decimal)0.4,
                Type = LevelType.SECONDARY,
                Color = "#D6F00E"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 05",
                Index = 5,
                Min = (decimal)0.4,
                Max = (decimal)0.5,
                Type = LevelType.SECONDARY,
                Color = "#FFF200"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 06",
                Index = 6,
                Min = (decimal)0.5,
                Max = (decimal)0.6,
                Type = LevelType.SECONDARY,
                Color = "#FFC04A"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 07",
                Index = 7,
                Min = (decimal)0.6,
                Max = (decimal)0.7,
                Type = LevelType.SECONDARY,
                Color = "#FF893B"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 08",
                Index = 8,
                Min = (decimal)0.7,
                Max = (decimal)0.8,
                Type = LevelType.SECONDARY,
                Color = "#FF5324"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 09",
                Index = 9,
                Min = (decimal)0.8,
                Max = (decimal)0.9,
                Type = LevelType.SECONDARY,
                Color = "#FF5324"
            },
            new Level()
            {
                Id = 0,
                Name = "Nivel 10",
                Index = 10,
                Min = (decimal)0.9,
                Max = (decimal)1.0,
                Type = LevelType.SECONDARY,
                Color = "#A23234"
            }
        };

        private readonly ContableDbContext _context;

        public DefaultLevelCreator(ContableDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreatePrimaryLevels();
            CreateSecondaryLevels();
        }

        private void CreatePrimaryLevels()
        {
            foreach(var level in DefaultPrimaryLevels)
            {
                var defaultLevel = _context
                    .Levels
                    .IgnoreQueryFilters()
                    .FirstOrDefault(p => p.Index == level.Index && p.Type == level.Type);

                if(defaultLevel == null)
                {
                    _context.Levels.Add(level);
                    _context.SaveChanges();
                }
            }
        }

        private void CreateSecondaryLevels()
        {
            foreach (var level in DefaultSecondaryLevels)
            {
                var defaultLevel = _context
                    .Levels
                    .IgnoreQueryFilters()
                    .FirstOrDefault(p => p.Index == level.Index && p.Type == level.Type);

                if (defaultLevel == null)
                {
                    _context.Levels.Add(level);
                    _context.SaveChanges();
                }
            }
        }
    }
}