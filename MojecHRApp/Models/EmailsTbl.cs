using System.ComponentModel.DataAnnotations;

namespace MojecHRApp.Models
{
    public class EmailsTbl
    {
        [Key]
        public int ID { get; set; }
        public string? Email { get; set; }
        public string? StaffID { get; set; }



      
    }
}
