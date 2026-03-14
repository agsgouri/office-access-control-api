using OfficeAccessControl.Application.DTO;
using OfficeAccessControl.Core.Models;

namespace OfficeAccessControl.Application.ServiceContracts
{
    public interface IAccessValidationService
    {
        Task<AccessResultDTO> ValidateAccessAsync(string userId, string locationId, AccessDirection direciton);
    }
}
