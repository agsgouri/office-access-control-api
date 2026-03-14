using Microsoft.AspNetCore.Identity;
using OfficeAccessControl.Application.DTO;
using OfficeAccessControl.Application.ServiceContracts;
using OfficeAccessControl.Infrastructure.Identity;

namespace OfficeAccessControl.Infrastructure
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO?> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                IsActive = user.LockoutEnd == null
            };
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }
    }
}
