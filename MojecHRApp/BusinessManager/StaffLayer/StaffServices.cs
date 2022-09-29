using MojecHRApp.DAL;
using MojecHRApp.Models;

namespace MojecHRApp.BusinessManager.StaffLayer
{
    public class StaffServices : IStaffServices
    {
        private HRDbContext _dbContext;

        public StaffServices(HRDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public StaffDetails Create(StaffDetails details)
        {
            _dbContext.StaffDetails.Add(details);
            _dbContext.SaveChanges();
            return details;
        }

        public LeaveRequest CreateRequest(LeaveRequest leaveRequest)
        {
            leaveRequest.Status = "Pending";
            _dbContext.LeaveRequest.Add(leaveRequest);
            _dbContext.SaveChanges();
            return leaveRequest;
        }



        public Experience CreateExperience(Experience experience)
        {
            
            _dbContext.Experience.Add(experience);
            _dbContext.SaveChanges();
            return experience;
        }

        public Experience GetExperienceByID(int id)
        {
            var staffexperience = _dbContext.Experience.Where(x => x.ID == id).FirstOrDefault();

            if (staffexperience == null)
                return null;

            return staffexperience;
        }

        public IEnumerable<LeaveRequest> GetLeaveRequestbyEmail(string email)
        {
            return _dbContext.LeaveRequest.Where(x => x.Email == email).ToList();
        }

        public StaffDetails GetStaffDetailsbyemail(string email)
        {
            var staffdetails = _dbContext.StaffDetails.Where(x =>x.EmailAddress == email).FirstOrDefault();

            if (staffdetails == null)
                return null;

            return staffdetails;

        }
        public void Update(StaffDetails details)
        {


            var accountToBrUpdated = _dbContext.StaffDetails.Where(x => x.EmailAddress == details.EmailAddress).SingleOrDefault();
            accountToBrUpdated.Firstname = details.Firstname;
            accountToBrUpdated.Middlename = details.Middlename;
            accountToBrUpdated.Lastname = details.Lastname;
            accountToBrUpdated.Phonenumber = details.Phonenumber;
            accountToBrUpdated.HomeAddress = details.HomeAddress;
            accountToBrUpdated.DOB = details.DOB;
            accountToBrUpdated.Nationality = details.Nationality;
            accountToBrUpdated.StateOfOrigin = details.StateOfOrigin;
            accountToBrUpdated.HMOID = details.HMOID;
            accountToBrUpdated.HMOOrg = details.HMOOrg;
            accountToBrUpdated.PlaceofBirth = details.PlaceofBirth;
            accountToBrUpdated.BloodGroup = details.BloodGroup;
            accountToBrUpdated.LocalGoverment = details.LocalGoverment;
            accountToBrUpdated.TypeOfID = details.TypeOfID;
            accountToBrUpdated.IDNo = details.IDNo;
            accountToBrUpdated.IDExpiryDate = details.IDExpiryDate;
            accountToBrUpdated.PlaceOfIssues = details.PlaceOfIssues;
            accountToBrUpdated.DateOfIssues = details.DateOfIssues;
            accountToBrUpdated.MaritalStatus = details.MaritalStatus;
            accountToBrUpdated.SpouseName = details.SpouseName;
            accountToBrUpdated.NoOfChildren = details.NoOfChildren;
            accountToBrUpdated.SpouseNationality = details.SpouseNationality;
            accountToBrUpdated.SpouseDob = details.SpouseDob;
            accountToBrUpdated.SpousePresentWork = details.SpousePresentWork;
            accountToBrUpdated.SpouseProffession = details.SpouseProffession;
            accountToBrUpdated.FatherName = details.FatherName; 
            accountToBrUpdated.MotherName = details.MotherName;
            accountToBrUpdated.ParentHouseAddress = details.ParentHouseAddress;
            accountToBrUpdated.EmailAddress = details.EmailAddress;
            accountToBrUpdated.Role = details.Role;
            accountToBrUpdated.Department = details.Department;
            accountToBrUpdated.DateOfEntry = details.DateOfEntry;
            accountToBrUpdated.Position = details.Position;
            accountToBrUpdated.Duration = details.Duration;
            accountToBrUpdated.CurrentDesignation = details.CurrentDesignation;
            accountToBrUpdated.CurrentDepartment = details.CurrentDepartment;
            accountToBrUpdated.LineManager = details.LineManager;
            accountToBrUpdated.DesignationPointOfEntry = details.DesignationPointOfEntry;
            accountToBrUpdated.NextOfKinFullname = details.NextOfKinFullname;
            accountToBrUpdated.NOKFamilyName = details.NOKFamilyName;
            accountToBrUpdated.NOKDateOfBirth = details.NOKDateOfBirth;
            accountToBrUpdated.NOKCurrentNationality = details.NOKCurrentNationality;
            accountToBrUpdated.NOKPreviousNationality = details.NOKPreviousNationality;
            accountToBrUpdated.NOKContactNumber = details.NOKContactNumber;
            accountToBrUpdated.NOKEmailAddress = details.NOKEmailAddress;
            accountToBrUpdated.NOKHomeAddress = details.NOKHomeAddress;
            accountToBrUpdated.HealthIssues = details.HealthIssues;
            accountToBrUpdated.IllnessInPast12Months = details.IllnessInPast12Months;
            accountToBrUpdated.AidForWork = details.AidForWork;
            accountToBrUpdated.Allergy = details.Allergy;
            accountToBrUpdated.EmployeeID = details.EmployeeID;
            _dbContext.StaffDetails.Update(accountToBrUpdated);
            _dbContext.SaveChanges();
        }

        public void UpdateExperience(Experience experience)
        {
            var accountToBrUpdated = _dbContext.Experience.Where(x => x.ID == experience.ID).SingleOrDefault();
            accountToBrUpdated.CompanyName = experience.CompanyName;
            accountToBrUpdated.Role = experience.Role;
            accountToBrUpdated.Designation = experience.Designation;
            accountToBrUpdated.From = experience.From;
            accountToBrUpdated.To = experience.To;
            _dbContext.Experience.Update(accountToBrUpdated);
            _dbContext.SaveChanges();

        }

        IEnumerable<Experience> IStaffServices.GetExperienceByEmail(string email)
        {
            return _dbContext.Experience.Where(x => x.Username == email).ToList();
        }
    }
}
