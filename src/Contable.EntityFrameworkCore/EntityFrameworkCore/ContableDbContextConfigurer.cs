using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Contable.EntityFrameworkCore
{
    public static class ContableDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ContableDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ContableDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}