namespace OfficeAccessControl.Application.DTO
{
    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? SupervisorId { get; set; }

    }
}
