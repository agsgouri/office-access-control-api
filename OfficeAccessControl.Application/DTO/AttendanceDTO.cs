namespace OfficeAccessControl.Application.DTO
{
    public class AttendanceDTO
    {
        public DateTime Date { get; set; }
        public double TotalMinutes { get; set; }
        public string Status { get; set; } = null!;
    }
}
