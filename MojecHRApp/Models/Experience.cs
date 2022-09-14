using System.ComponentModel.DataAnnotations;

namespace MojecHRApp.Models
{
    public class Experience
    {
        [Key]
        public int ID {get;set;}
        public string? CompanyName {get;set;}
        public string? Role { get;set;}
        public string? Designation { get; set;}
        public string? From { get; set;}
        public string? To { get; set;}
        public string? Username { get; set;}
    }
}
