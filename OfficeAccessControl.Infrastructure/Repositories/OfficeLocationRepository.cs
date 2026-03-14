using Microsoft.EntityFrameworkCore;
using OfficeAccessControl.Core.Models;
using OfficeAccessControl.Core.Repositories;
using OfficeAccessControl.Infrastructure.ApplicationDbContext;

namespace OfficeAccessControl.Infrastructure.Repositories
{
    public class OfficeLocationRepository : IOfficeLocationRepository
    {
        private readonly AppDbContext _context;

        public OfficeLocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OfficeLocation?> GetByIdWithRolesAsync(string id)
        {
            return await _context.OfficeLocations
                .Include(l => l.AllowedRoles)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}
