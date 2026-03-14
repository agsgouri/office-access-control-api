using OfficeAccessControl.Core.Models;

namespace OfficeAccessControl.Application.DTO
{
    public class AccessRequest
    {
        public string UserId { get; set; } = null!;
        public string LocationId { get; set; } = null!;
        public AccessDirection Direction { get; set; }
    }
}
