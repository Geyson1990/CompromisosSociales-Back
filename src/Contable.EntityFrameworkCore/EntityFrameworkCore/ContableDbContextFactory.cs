using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Contable.Configuration;
using Contable.Web;

namespace Contable.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ContableDbContextFactory : IDesignTimeDbContextFactory<ContableDbContext>
    {
        public ContableDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ContableDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            ContableDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ContableConsts.ConnectionStringName));

            return new ContableDbContext(builder.Options);
        }
    }
}