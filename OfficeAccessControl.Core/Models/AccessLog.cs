namespace OfficeAccessControl.Core.Models
{
    public class AccessLog
    {
        public int ID { get; set; }
        public required string  UserId { get; set; }
        public required string LocationId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSuccess { get; set; }
        public string? FailureReason { get; set; }
        public bool IsFlagged { get; set; }

        public AccessDirection Direction { get; set; }
    }
}
