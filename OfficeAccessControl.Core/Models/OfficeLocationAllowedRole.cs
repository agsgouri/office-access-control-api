namespace OfficeAccessControl.Core.Models
{
    public class OfficeLocationAllowedRole
    {
        public int Id { get; set; }

        public required string LocationId { get; set; }

        public required string RoleName { get; set; }

        public OfficeLocation? OfficeLocation { get; set; }
    }
}
