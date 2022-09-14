using MojecHRApp.Models;

namespace MojecHRApp.BusinessManager.StaffLayer
{
    public interface IStaffServices
    {
        StaffDetails Create(StaffDetails details);
        StaffDetails GetStaffDetailsbyemail(string email);
        void Update(StaffDetails details);
        Experience CreateExperience (Experience experience);
        IEnumerable <Experience> GetExperienceByEmail (string email);
        Experience GetExperienceByID(int id);
        void UpdateExperience (Experience experience);  

        IEnumerable <LeaveRequest> GetLeaveRequestbyEmail(string email);

        LeaveRequest CreateRequest(LeaveRequest leaveRequest);

    }
}
