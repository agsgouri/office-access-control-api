using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAccessControl.Application.ServiceContracts;

namespace OfficeAccessControl.API.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IAuthorizationService _authorizationService;

        public AttendanceController(
            IAttendanceService attendanceService,
            IAuthorizationService authorizationService)
        {
            _attendanceService = attendanceService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAttendance(string userId)
        {
            var authResult = await _authorizationService
                .AuthorizeAsync(User, userId, "CanViewAttendance");

            if (!authResult.Succeeded)
                return Forbid();

            var data = await _attendanceService.GetByUserIdAsync(userId);

            return Ok(data);
        }
    }
}
