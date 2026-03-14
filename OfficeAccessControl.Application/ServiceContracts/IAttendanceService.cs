using OfficeAccessControl.Application.DTO;

namespace OfficeAccessControl.Application.ServiceContracts
{

    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDTO>> GetByUserIdAsync(string userId);
    }
}
