using System.ComponentModel.DataAnnotations;

namespace MojecHRApp.Models
{
    public class StaffDetails
    {

	[Key]
     public int StaffID { get; set; }
     public string? Firstname { get; set; }
	 public string? Middlename { get; set; }
	 public string? Lastname { get; set; }
	 public string? Phonenumber { get; set; }
	public string? HomeAddress { get; set; }
	public string?  DOB { get; set; }
	public string? Nationality { get; set; }
	public string? StateOfOrigin { get; set; }
	public string?  HMOID { get; set; }
    public string? HMOOrg { get; set; }
	public string?  PlaceofBirth { get; set; }
	public string? BloodGroup { get; set;}
	public string? LocalGoverment { get; set; }
	public string? TypeOfID { get; set; }
	public string? IDNo { get; set; }
	public string? IDExpiryDate { get; set; }
    public string? PlaceOfIssues { get; set; }
	public string? DateOfIssues { get; set; }
    public string? MaritalStatus { get; set; }
	public string? SpouseName { get; set; }
	public string? NoOfChildren { get; set; }
	public string? SpouseNationality { get; set; }
	public string? SpouseDob { get; set ; }
	public string? SpousePresentWork { get; set; }
	public string? SpouseProffession { get; set; }
    public string? FatherName { get; set; }
	public string? MotherName { get; set; }
	public string? ParentHouseAddress { get; set; }
	public string? EmailAddress { get; set; }
	public string? Role { get; set; }
	public string? Department { get; set; }
	public string? DateOfEntry { get; set; }
	public string? Position { get; set; }
	public string? Duration { get; set; }
	public string? CurrentDesignation { get; set;}
	public string? CurrentDepartment { get; set; }
	public string? LineManager { get; set; }
	public string? DesignationPointOfEntry { get; set; }
	public string? NextOfKinFullname { get; set; }
	public string? NOKFamilyName { get; set; }
	public string? NOKDateOfBirth { get; set; }
	public string? NOKCurrentNationality { get; set; }
    public string? NOKPreviousNationality { get; set; }
	public string? NOKContactNumber { get; set; }
	public string? NOKEmailAddress { get; set; }
	public string? NOKHomeAddress { get; set; }
	public string? HealthIssues { get; set; }
	public string? IllnessInPast12Months { get; set; }
	 public string? AidForWork { get; set; }
	 public string? Allergy { get; set; }
	 public string? EmployeeID { get; set; }

    }
}
