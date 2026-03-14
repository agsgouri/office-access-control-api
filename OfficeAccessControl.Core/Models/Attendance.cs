namespace OfficeAccessControl.Core.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public DateTime Date { get; set; }

        public double TotalMinutes { get; set; }

        public string Status { get; set; } = null!; 

        public ICollection<AttendanceSession> Sessions { get; set; }
            = new List<AttendanceSession>();
    }
}
