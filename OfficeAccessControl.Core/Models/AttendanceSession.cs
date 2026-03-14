namespace OfficeAccessControl.Core.Models
{
    public class AttendanceSession
    {
        public int Id { get; set; }

        public int AttendanceId { get; set; }

        public DateTime InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public Attendance Attendance { get; set; } = null!;
    }
}
