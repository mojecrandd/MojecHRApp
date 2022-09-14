namespace MojecHRApp.Models
{
    public class LeaveRequest
    {
        public int ID  { get; set; }
        public string? Email { get; set; }
        public string? Reason { get; set; }
        public string? LeaveDate { get; set; }
        public string? ReturnDate { get; set; }
        public string? Status { get; set; }
    }
}
