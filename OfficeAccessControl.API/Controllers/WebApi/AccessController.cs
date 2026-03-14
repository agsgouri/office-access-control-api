using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAccessControl.Application.DTO;
using OfficeAccessControl.Application.ServiceContracts;
using OfficeAccessControl.Core.Repositories;

namespace OfficeAccessControl.API.Controllers.WebApi
{
    [ApiController]
    [Authorize]
    [Route("api/access")]
    public class AccessController : ControllerBase
    {
        private readonly IAccessValidationService _service;

        public AccessController(IAccessValidationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Validate([FromBody] AccessRequest request)
        {
            var result = await _service.ValidateAccessAsync(
                request.UserId,
                request.LocationId,request.Direction);

            return Ok(result);
        }

        
    }
}
