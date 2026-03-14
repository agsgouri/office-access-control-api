using Microsoft.AspNetCore.Identity;

namespace OfficeAccessControl.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? SupervisorId { get; set; }
    }
}
