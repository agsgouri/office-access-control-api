namespace OfficeAccessControl.Application.DTO
{
    public class AccessResultDTO
    {
        public bool IsSuccess { get; set; }
        public string? FailureReason { get; set; }
        public bool IsFlagged { get; set; }
    }
}
