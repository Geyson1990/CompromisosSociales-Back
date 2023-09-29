using Abp.Domain.Entities;
using Contable.Application;
using Contable.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace Contable.Migrations.Seed.Application
{
    public class DefaultParameterBuilder
    {
        private readonly ContableDbContext _context;

        public DefaultParameterBuilder(ContableDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultParameters();
        }

        private void CreateDefaultParameters()
        {
            //Saving for CompromiseConsts.ParameterCategoryType
            var defaultCate = CreateParameterCategoryIfNotExists(CompromiseConsts.ParameterCategoryType, "Tipo de compromiso");
            if (defaultCate.Id > 0)
            {
                CreateParameterIfNotExists(defaultCate.Id, "PIP");
                CreateParameterIfNotExists(defaultCate.Id, "Actividad");
            }
                       
            //Saving for CompromiseConsts.ParameterCategoryPIPPhase
            CreateParameterCategoryIfNotExists(CompromiseConsts.ParameterCategoryPIPPhase, "Fase del PIP");

            //Saving for SocialConflictConsts.ParameterCategoryStatus
            defaultCate = CreateParameterCategoryIfNotExists(SocialConflictConsts.ParameterCategoryStatus, "Estado del caso conflictivo");      
            
            if (defaultCate.Id > 0)
            {
                CreateParameterIfNotExists(defaultCate.Id, "Pre-conflicto");
                CreateParameterIfNotExists(defaultCate.Id, "Conflicto");
                CreateParameterIfNotExists(defaultCate.Id, "Post-conflicto");
                CreateParameterIfNotExists(defaultCate.Id, "Situación sensible");
            }
        }

        private ParameterCategory CreateParameterCategoryIfNotExists(string code, string value)
        {
            var defaultCate = _context.ParameterCategories.IgnoreQueryFilters().FirstOrDefault(t => t.Code.Equals(code));
            if (defaultCate == null)
            {
                defaultCate = _context.ParameterCategories.Add(new ParameterCategory()
                {
                    Code = code,
                    Name = value
                }).Entity;
                _context.SaveChanges();
            }
            return defaultCate;
        }

        private Parameter CreateParameterIfNotExists(int categoryId, string parameterValue, int parent = 0)
        {
            var defaultParameter = _context.Parameters.IgnoreQueryFilters()                                                      
                                                      .FirstOrDefault(e => e.Value.Equals(parameterValue) 
                                                      && e.ParameterCategoryId == categoryId 
                                                      && e.ParentId == parent);

            if (defaultParameter == null)
            {
                defaultParameter = _context.Parameters.Add(new Parameter
                    {
                        Value = parameterValue,
                        ParameterCategoryId = categoryId,
                        ParentId = parent
                }).Entity;
                _context.SaveChanges();
            }
            
            return defaultParameter;
        }
    }
}
