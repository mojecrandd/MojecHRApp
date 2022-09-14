using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojecHRApp.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginTbl",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginTbl", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "StaffDetails",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HMOID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HMOOrg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceofBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalGoverment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfChildren = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseDob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpousePresentWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseProffession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentHouseAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignationPointOfEntry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextOfKinFullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKFamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKDateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKCurrentNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKPreviousNationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOKHomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HealthIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IllnessInPast12Months = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AidForWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffDetails", x => x.StaffID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginTbl");

            migrationBuilder.DropTable(
                name: "StaffDetails");
        }
    }
}
