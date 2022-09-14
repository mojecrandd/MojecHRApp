using Microsoft.EntityFrameworkCore;
using MojecHRApp.Models;

namespace MojecHRApp.DAL
{
    public class HRDbContext:DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
        {

        }

        public DbSet<StaffDetails> StaffDetails { get; set; }
        public DbSet<LoginTbl> LoginTbl { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Files> Files { get; set; }

        public DbSet<EmailsTbl> EmailsTbl { get; set; }

        public DbSet<LeaveRequest> LeaveRequest { get; set; }
    }
}
