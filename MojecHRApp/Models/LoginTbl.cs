using System.ComponentModel.DataAnnotations;

namespace MojecHRApp.Models
{
    public class LoginTbl
    {
        [Key]
        public int UserID { get; set; }
        [Required, EmailAddress]
        public string? Username { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; } = string.Empty;
        public string? Fullname { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public string? IsActive { get; set; } = "Active";
    }
}
