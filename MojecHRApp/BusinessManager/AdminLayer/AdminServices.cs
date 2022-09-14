using MojecHRApp.DAL;
using MojecHRApp.Models;
using System.Data.SqlClient;

namespace MojecHRApp.BusinessManager.AdminLayer
{
    public class AdminServices : IAdminServices
    {
        private HRDbContext _dbContext;
        protected readonly IConfiguration _config;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        public AdminServices(IConfiguration config, HRDbContext hRDbContext)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("DefaultConnectionString");
            ProviderName = "System.Data.SqlClient";
            _dbContext = hRDbContext;   
        }
        void connectionString()
        {
            con.ConnectionString = _config.GetConnectionString("DefaultConnectionString");
        }

        public EmailsTbl CreateEmailTbl(EmailsTbl emp)
        {
            _dbContext.EmailsTbl.Add(emp);
            _dbContext.SaveChanges();
            return emp;
        }

        public IEnumerable<EmailsTbl> GetAllEmailsTbls()
        {
            return _dbContext.EmailsTbl.ToList();
        }

        public IEnumerable<LoginTbl> GetLoginTbls()
        {
            return _dbContext.LoginTbl.ToList();
        }

        public StaffDetails GetStaffDetailsbyemail(string email)
        {
            var staffdetails = _dbContext.StaffDetails.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (staffdetails == null)
                return null;

            return staffdetails;
        }

       

        IEnumerable<Experience> IAdminServices.GetStaffExperiencebyemail(string email)
        {
            return _dbContext.Experience.Where(x => x.Username == email).ToList();
        }

        public IEnumerable<Files> GetStaffFilesbyemail(string email)
        {
            return _dbContext.Files.Where(x => x.Email == email).ToList();
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestbyemail(string email)
        {
            return _dbContext.LeaveRequest.Where(x => x.Email == email && x.Status == "Pending").ToList();
        }

        public LeaveRequest GetStaffRequestbyemail(string email)
        {
            var requestdetails = _dbContext.LeaveRequest.Where(x => x.Email == email).FirstOrDefault();

            if (requestdetails == null)
                return null;

            return requestdetails;
        }

        
    }
}
