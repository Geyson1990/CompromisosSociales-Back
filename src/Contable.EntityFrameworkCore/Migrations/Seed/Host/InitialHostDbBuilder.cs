using Contable.EntityFrameworkCore;

namespace Contable.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly ContableDbContext _context;

        public InitialHostDbBuilder(ContableDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
