using Microsoft.EntityFrameworkCore;
using OfficeAccessControl.Core.Models;
using OfficeAccessControl.Core.Repositories;
using OfficeAccessControl.Infrastructure.ApplicationDbContext;

namespace OfficeAccessControl.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Attendance>> GetByUserIdAsync(string userId)
        {
            return await _context.Attendances
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
        public async Task<Attendance?> GetByUserAndDateAsync(string userId, DateTime date)
        {
            return await _context.Attendances
                .Include(a => a.Sessions)
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.Date.Date == date.Date);
        }

        public async Task AddAsync(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }
    }
}
