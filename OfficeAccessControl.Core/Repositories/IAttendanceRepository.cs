using OfficeAccessControl.Core.Models;

namespace OfficeAccessControl.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance?> GetByUserAndDateAsync(string userId, DateTime date);
        Task<IEnumerable<Attendance>> GetByUserIdAsync(string userId);
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
    }
}
