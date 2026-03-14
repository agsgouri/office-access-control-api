using Microsoft.Extensions.Caching.Memory;
using OfficeAccessControl.Application.DTO;
using OfficeAccessControl.Application.ServiceContracts;
using OfficeAccessControl.Core.Models;
using OfficeAccessControl.Core.Repositories;

namespace OfficeAccessControl.Application.Services
{
    public class AccessValidationService : IAccessValidationService
    {
        private readonly IOfficeLocationRepository _locationRepository;
        private readonly IAccessLogRepository _logRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;
        public AccessValidationService(
            IOfficeLocationRepository locationRepository,
            IAccessLogRepository logRepository,
            IAttendanceRepository attendanceRepository,
            IUserService userService,
            IMemoryCache cache)
        {
            _locationRepository = locationRepository;
            _logRepository = logRepository;
            _attendanceRepository = attendanceRepository;
            _userService = userService;
            _cache = cache;
        }

        public async Task<AccessResultDTO> ValidateAccessAsync(string userId, string locationId, AccessDirection direction)
        {
            var user = await _userService.GetUserAsync(userId);

            if (user == null)
            {
                await _logRepository.AddAsync(new AccessLog
                {
                    UserId = userId,
                    LocationId = locationId,
                    Timestamp = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = "User Not Found",
                    IsFlagged = false
                });


                return new AccessResultDTO
                {
                    IsSuccess = false,
                    FailureReason = "User Not found",
                    IsFlagged = false
                };
            }
            if (!user.IsActive)
            {
                await _logRepository.AddAsync(new AccessLog
                {
                    UserId = userId,
                    LocationId = locationId,
                    Timestamp = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = "User Inactive",
                    IsFlagged = true
                });


                return new AccessResultDTO
                {
                    IsSuccess = false,
                    FailureReason = "User inactive",
                    IsFlagged = true
                };
            }

            var cacheKey = $"location_{locationId}";

            if (!_cache.TryGetValue(cacheKey, out OfficeLocation? location))
            {
                location = await _locationRepository.GetByIdWithRolesAsync(locationId);

                if (location != null)
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    };

                    _cache.Set(cacheKey, location, cacheOptions);
                }
            }

            if (location == null)
            {
                await _logRepository.AddAsync(new AccessLog
                {
                    UserId = userId,
                    LocationId = locationId,
                    Timestamp = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = "Location Not Found",
                    IsFlagged = false
                });
                return new AccessResultDTO
                {
                    IsSuccess = false,
                    FailureReason = "Location not found",
                    IsFlagged = false
                };
            }
            var roles = await _userService.GetUserRolesAsync(userId);
            var isAllowed = location.AllowedRoles.Any(r => roles.Contains(r.RoleName));

            if (!isAllowed)
            {
                await _logRepository.AddAsync(new AccessLog
                {
                    UserId = userId,
                    LocationId = locationId,
                    Timestamp = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = "Role not allowed",
                    IsFlagged = true
                });

                return new AccessResultDTO
                {
                    IsSuccess = false,
                    FailureReason = "Role not allowed",
                    IsFlagged = true
                };
            }


            
            await HandleAttendanceAsync(userId, direction);
            await _logRepository.AddAsync(new AccessLog
            {
                UserId = userId,
                LocationId = locationId,
                Timestamp = DateTime.UtcNow,
                IsSuccess = true,
                FailureReason = null,
                IsFlagged = false
            });

            return new AccessResultDTO
            {
                IsSuccess = true,
                IsFlagged = false
            };
        }

        private async Task HandleAttendanceAsync(string userId, AccessDirection direction)
        {
            var today = DateTime.UtcNow.Date;

            var attendance = await _attendanceRepository
                .GetByUserAndDateAsync(userId, today);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    UserId = userId,
                    Date = today,
                    Status = "Present"
                };

                await _attendanceRepository.AddAsync(attendance);

                attendance = await _attendanceRepository
                    .GetByUserAndDateAsync(userId, today);
            }

            if (direction == AccessDirection.In)
            {
                var openSession = attendance?.Sessions
                    .FirstOrDefault(s => s.OutTime == null);

                if (openSession != null)
                {
                    // double IN → ignore or flag
                    return;
                }

                attendance?.Sessions.Add(new AttendanceSession
                {
                    InTime = DateTime.UtcNow
                });

                await _attendanceRepository.UpdateAsync(attendance);
            }

            if (direction == AccessDirection.Out)
            {
                var openSession = attendance.Sessions
                    .OrderByDescending(s => s.InTime)
                    .FirstOrDefault(s => s.OutTime == null);

                if (openSession == null)
                {
                    // OUT without IN → ignore or flag
                    return;
                }

                openSession.OutTime = DateTime.UtcNow;

                attendance.TotalMinutes = attendance.Sessions
                    .Where(s => s.OutTime != null)
                    .Sum(s => (s.OutTime.Value - s.InTime).TotalMinutes);

                await _attendanceRepository.UpdateAsync(attendance);
            }
        }
    }
}
