using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Contable.EntityFrameworkCore;
using Contable.Migrations.Seed.Host;
using Contable.Migrations.Seed.Tenants;
using Contable.Migrations.Seed.Application;

namespace Contable.Migrations.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<ContableDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(ContableDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            //Host seed
            new InitialHostDbBuilder(context).Create();

            //Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();

            //Default Application Data
            new DefaultParameterBuilder(context).Create();
            new DefaultProcedureBuilder(context).Create();
            new DefaultLevelCreator(context).Create();
            new DefaultReportCreator(context).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
