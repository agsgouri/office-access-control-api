namespace OfficeAccessControl.Core.Models
{
    public class OfficeLocation
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public ICollection<OfficeLocationAllowedRole> AllowedRoles { get; set; }
            = new List<OfficeLocationAllowedRole>();
    }
}
