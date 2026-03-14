using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OfficeAccessControl.Infrastructure.Authorization;
using OfficeAccessControl.Infrastructure.Identity;

public class CanViewAttendanceHandler
    : AuthorizationHandler<CanViewAttendanceRequirement, string>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CanViewAttendanceHandler(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CanViewAttendanceRequirement requirement,
        string targetUserId)
    {
        var loggedInUserId = context.User.FindFirst("sub")?.Value;

        if (loggedInUserId == null)
            return;

        // Admin → full access
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return;
        }

        // Self access
        if (loggedInUserId == targetUserId)
        {
            context.Succeed(requirement);
            return;
        }

        // Supervisor access
        var targetUser = await _userManager.FindByIdAsync(targetUserId);

        if (targetUser?.SupervisorId == loggedInUserId) 
        {
            context.Succeed(requirement);
        }
    }
}