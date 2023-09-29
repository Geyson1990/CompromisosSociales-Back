using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Abp;
using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Threading;
using Contable.Dto;
using Contable.Localization;
using Contable.Notifications;
using Contable.Storage;

namespace Contable.Gdpr
{
    public class UserCollectedDataPrepareJob : BackgroundJob<UserIdentifier>, ITransientDependency
    {
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly ISettingManager _settingManager;

        public UserCollectedDataPrepareJob(
            IBinaryObjectManager binaryObjectManager,
            ITempFileCacheManager tempFileCacheManager,
            ISettingManager settingManager)
        {
            _binaryObjectManager = binaryObjectManager;
            _tempFileCacheManager = tempFileCacheManager;
            _settingManager = settingManager;
        }

        [UnitOfWork]
        public override void Execute(UserIdentifier args)
        {
            using (UnitOfWorkManager.Current.SetTenantId(args.TenantId))
            {
                var userLanguage = AsyncHelper.RunSync(() => _settingManager.GetSettingValueForUserAsync(LocalizationSettingNames.DefaultLanguage, args.TenantId, args.UserId));
                var culture = CultureHelper.GetCultureInfoByChecking(userLanguage);

                using (CultureInfoHelper.Use(culture))
                {
                    var files = new List<FileDto>();

                    using (var scope = IocManager.Instance.CreateScope())
                    {
                        var providers = scope.ResolveAll<IUserCollectedDataProvider>();
                        foreach (var provider in providers)
                        {
                            var providerFiles = AsyncHelper.RunSync(() => provider.GetFiles(args));
                            if (providerFiles.Any())
                            {
                                files.AddRange(providerFiles);
                            }
                        }
                    }

                    var zipFile = new BinaryObject
                    {
                        TenantId = args.TenantId,
                        Bytes = CompressFiles(files)
                    };

                    // Save zip file to object manager.
                    AsyncHelper.RunSync(() => _binaryObjectManager.SaveAsync(zipFile));
                }
            }
        }

        private byte[] CompressFiles(List<FileDto> files)
        {
            using (var outputZipFileStream = new MemoryStream())
            {
                using (var zipStream = new ZipArchive(outputZipFileStream, ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        var fileBytes = _tempFileCacheManager.GetFile(file.FileToken);
                        var entry = zipStream.CreateEntry(file.FileName);

                        using (var originalFileStream = new MemoryStream(fileBytes))
                        using (var zipEntryStream = entry.Open())
                        {
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }

                return outputZipFileStream.ToArray();
            }
        }

    }
}
