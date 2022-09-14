using MojecHRApp.Models;

namespace MojecHRApp.BusinessManager.AdminLayer
{
    public interface IAdminServices
    {
        EmailsTbl CreateEmailTbl(EmailsTbl emp);
        IEnumerable<EmailsTbl> GetAllEmailsTbls();  
        IEnumerable<LoginTbl> GetLoginTbls();

        StaffDetails GetStaffDetailsbyemail(string email);

        IEnumerable <Experience> GetStaffExperiencebyemail(string email);

        IEnumerable <Files> GetStaffFilesbyemail(string email);

        IEnumerable<LeaveRequest> GetLeaveRequestbyemail(string email);

        LeaveRequest GetStaffRequestbyemail(string email);
    }
}
