using System;
using System.Collections.Generic;
using System.Linq;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Threading;
using Contable.MultiTenancy;
using Contable.Notifications;

namespace Contable.Editions
{
    public class MoveTenantsToAnotherEditionJob : BackgroundJob<MoveTenantsToAnotherEditionJobArgs>, ITransientDependency
    {
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly EditionManager _editionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public IEventBus EventBus { get; set; }

        public MoveTenantsToAnotherEditionJob(
            IRepository<Tenant> tenantRepository,
            EditionManager editionManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _tenantRepository = tenantRepository;
            _editionManager = editionManager;
            _unitOfWorkManager = unitOfWorkManager;

            EventBus = NullEventBus.Instance;
        }

        public override void Execute(MoveTenantsToAnotherEditionJobArgs args)
        {
            if (args.SourceEditionId == args.TargetEditionId)
            {
                return;
            }

            List<int> tenantIds;

            using (var uow = _unitOfWorkManager.Begin())
            {
                tenantIds = _tenantRepository.GetAll().Where(t => t.EditionId == args.SourceEditionId).Select(t => t.Id).ToList();
                uow.Complete();
            }

            if (!tenantIds.Any())
            {
                return;
            }

            var changedTenantCount = ChangeEditionOfTenants(tenantIds, args.SourceEditionId, args.TargetEditionId);

            if (changedTenantCount != tenantIds.Count)
            {
                Logger.Warn($"Unable to move all tenants from edition {args.SourceEditionId} to edition {args.TargetEditionId}");
                return;
            }

            NotifyUser(args);
        }

        private int ChangeEditionOfTenants(List<int> tenantIds, int sourceEditionId, int targetEditionId)
        {
            var changedTenantCount = 0;

            foreach (var tenantId in tenantIds)
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    var changed = ChangeEditionOfTenant(tenantId, sourceEditionId, targetEditionId);
                    if (changed)
                    {
                        changedTenantCount++;
                    }

                    uow.Complete();
                }
            }

            return changedTenantCount;
        }

        private void NotifyUser(MoveTenantsToAnotherEditionJobArgs args)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                var sourceEdition = AsyncHelper.RunSync(() => _editionManager.GetByIdAsync(args.SourceEditionId));
                var targetEdition = AsyncHelper.RunSync(() => _editionManager.GetByIdAsync(args.TargetEditionId));

                uow.Complete();
            }
        }

        private bool ChangeEditionOfTenant(int tenantId, int sourceEditionId, int targetEditionId)
        {
            try
            {
                var tenant = _tenantRepository.Get(tenantId);
                tenant.EditionId = targetEditionId;

                CurrentUnitOfWork.SaveChanges();

                EventBus.Trigger(new TenantEditionChangedEventData
                {
                    TenantId = tenant.Id,
                    OldEditionId = sourceEditionId,
                    NewEditionId = targetEditionId
                });

                return true;
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                return false;
            }
        }
    }
}
