namespace OfficeAccessControl.Application.Services
{
    using OfficeAccessControl.Application.DTO;
    using OfficeAccessControl.Application.ServiceContracts;
    using OfficeAccessControl.Core.Repositories;

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<IEnumerable<AttendanceDTO>>
            GetByUserIdAsync(string userId)
        {
            var attendances = await
                _attendanceRepository.GetByUserIdAsync(userId);

            return attendances.Select(a => new AttendanceDTO
            {
                Date = a.Date,
                TotalMinutes = a.TotalMinutes,
                Status = a.Status
            });
        }
    }
}
