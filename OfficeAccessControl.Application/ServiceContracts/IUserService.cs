using OfficeAccessControl.Application.DTO;

namespace OfficeAccessControl.Application.ServiceContracts
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(string userId);
    }
}
