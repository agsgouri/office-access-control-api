using OfficeAccessControl.Core.Models;
using OfficeAccessControl.Core.Repositories;
using OfficeAccessControl.Infrastructure.ApplicationDbContext;

namespace OfficeAccessControl.Infrastructure.Repositories
{

    public class AccessLogRepository : IAccessLogRepository
    {
        private readonly AppDbContext _context;

        public AccessLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AccessLog log)
        {
            _context.AccessLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
