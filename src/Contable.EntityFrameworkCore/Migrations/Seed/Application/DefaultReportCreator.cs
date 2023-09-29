using Contable.Application;
using Contable.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contable.Migrations.Seed.Application
{
    public class DefaultReportCreator
    {
        private readonly List<Report> Reports = new List<Report>()
        {       new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflict,
                Description = "Reporte de situaciones conflictivas",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictAlert,
                Description = "Reporte de alertas de situaciones conflictivas",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictSensible,
                Description = "Reporte de situaciones sensibles",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictHelpMemory,
                Description = "Reporte de conflictos sociales para ayuda y memoria",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictSensibleHelpMemory,
                Description = "Reporte de situaciones sensibles para ayuda y memoria",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.CrisisCommittee,
                Description = "Reporte de comité de crisis",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.InterventionPlan,
                Description = "Reporte de plan de intervención",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SectorMeetSession,
                Description = "Reporte de reunión",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictAlertResume,
                Description = "Reporte Ejecutivo - Alerta de Situación Conflictiva",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictAlertHistory,
                Description = "Reporte de historial de envío de alertas",
                Enabled = true
            },
            new Report()
            {
                Id = 0,
                Name = ReportNames.SocialConflictTaskHistory,
                Description = "Reporte de historial de envío de notificaciones de tareas de conflictos",
                Enabled = true
            }
        };

        private readonly ContableDbContext _context;

        public DefaultReportCreator(ContableDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateReports();
        }

        private void CreateReports()
        {
            foreach (var report in Reports)
            {
                var defaultReport = _context
                    .Reports
                    .IgnoreQueryFilters()
                    .FirstOrDefault(p => p.Name == report.Name);

                if (defaultReport == null)
                {
                    _context.Reports.Add(report);
                    _context.SaveChanges();
                }
            }
        }
    }
}
