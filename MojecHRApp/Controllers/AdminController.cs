using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MojecHRApp.BusinessManager.AdminLayer;
using MojecHRApp.BusinessManager.StaffLayer;
using MojecHRApp.DAL;
using MojecHRApp.Models;
using System.Drawing;
using static MojecHRApp.BusinessManager.MailService.MailJetService;

namespace MojecHRApp.Controllers
{
    public class AdminController : Controller
    {
       
        public IAdminServices _adminService { get; set; }
        private HRDbContext _dbContext { get; set; }
        private readonly IMailService _mailService;
        IConfiguration _config;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public AdminController(IAdminServices adminServices, IConfiguration config, IMailService mailService, HRDbContext dbContext)
        {
            _adminService = adminServices;
            _config = config;
            ConnectionString = _config.GetConnectionString("ConnectionString");
            ProviderName = "System.Data.SqlClient";
            _mailService = mailService;
            _dbContext = dbContext;
        }

        void connectionString()
        {
            con.ConnectionString = _config.GetConnectionString("ConnectionString");
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            connectionString();
            con.Open();
            com.Connection = con;
            SqlCommand cmd = new SqlCommand("select  Count(*) from  LoginTbl ", con);
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            ViewBag.TotalStaff = r;

            SqlCommand cmd2 = new SqlCommand("select  Count(*) from  LeaveRequest ", con);
            int  e= Convert.ToInt32(cmd2.ExecuteScalar());
            ViewBag.Leave = e;

            return View();

        }
        public IActionResult Emails()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            var email = _adminService.GetAllEmailsTbls();
            return View(email); 
        }
        public IActionResult CreateEmail()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateEmail(EmailsTbl emails)
        {
            _adminService.CreateEmailTbl(emails);
            TempData["save"] = "Data Added Successfully";
            return RedirectToAction("Emails");
        }
        [HttpGet]
        public IActionResult ActivateAccount(string Id)
        {
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update LoginTbl set IsActive = 'Active' where Username = '" + Id + "'", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            TempData["save"] = "Account Acitvated Successfully";
            return RedirectToAction("Emails");
        }
        [HttpGet ]
        public IActionResult DeactivateAccount(string Id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update LoginTbl set IsActive = 'In-Active' where Username = '" + Id + "'", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            TempData["delete"] = "Account Deactivated Successfully";
            return RedirectToAction("Emails");
        } 
        public IActionResult AllStaff()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var staff = _adminService.GetLoginTbls();
            return View(staff);
        }
        [HttpGet]
        public IActionResult GetStaffDetails(string? Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            
            var staffdetails = _adminService.GetStaffDetailsbyemail(Id);
            return View(staffdetails);
        }
        [HttpGet]
        public IActionResult GetStaffExperience(string? Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var experience = _adminService.GetStaffExperiencebyemail(Id);
            return View(experience);
        }
        [HttpGet]
        public IActionResult GetStaffFiles(string? Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var files = _adminService.GetStaffFilesbyemail(Id);
            return View(files);
        }
        public FileResult DownloadFiles(int Id)
        {
            Files model = PopulateFile().Find(x => x.Id == Convert.ToInt32(Id));
            string? fileName = model.Name;
            string? contentType = model.ContentType;
            byte[]? bytes = model.Data;
            return File(bytes, contentType, fileName);
        }
        private List<Files> PopulateFile()
        {

            List<Files> files = new List<Files>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string query = "select * from Files";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new Files
                            {
                                Id = Convert.ToInt32(sdr["ID"]),
                                Name = sdr["Name"].ToString(),
                                ContentType = sdr["ContentType"].ToString(),
                                Data = (byte[])sdr["Data"],
                                FileType = sdr["FileType"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return files;
        }
        public IActionResult StaffLeave()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var staff = _adminService.GetLoginTbls();
            return View(staff);
        }
        public IActionResult GetStaffRequest(string Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var staff = _adminService.GetLeaveRequestbyemail(Id);
            return View(staff);
        }
        public IActionResult GetStaffRequestDetails(string Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var leave = _adminService.GetStaffRequestbyemail(Id);
            return View(leave);
        }
        public async Task <IActionResult> AcceptLeave(string Id)
        {
            string email = Id;
            string subject = "Leave Request";
            string content = "Your Leave has been accepted by the HR.";

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update LeaveRequest set Status = 'Accepted' where Email = '" + Id + "'", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }

            await _mailService.SendEmailAsync(email, subject, content);

           

            TempData["save"] = "Leave request accepted successfully";
            return RedirectToAction("StaffLeave");
        }
        public async Task <IActionResult> DeclineLeave(string Id)
        {
            string email = Id;
            string subject = "Leave Request";
            string content = "Your Leave has been Decline by the HR.";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update LeaveRequest set Status = 'Declined' where Email = '" + Id + "'", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            await _mailService.SendEmailAsync(email, subject, content);
            TempData["save"] = "Leave request declined successfully";
            return RedirectToAction("StaffLeave");
        }
        public IActionResult SendPublicNotice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendPublicNotice(string to, string subject, string content)
        {
            SqlDataReader dr;
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from EmailsTbl";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                to = dr["Email"].ToString();
                subject = "Public Notice";
                await _mailService.SendEmailAsync(to, subject, content);
                
                
            }
            TempData["save"] = "Public Notice Sent Successfully";
            return View();




        }


        public IActionResult DisableEditing(string Id)
        {
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd =  new SqlCommand("Update StaffDetails set IsEdit = 'Yes' where EmailAddress = '"+Id+"'",con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Update Files set IsEdit = 'Yes' where Email = '" + Id + "'", con);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Update Experience set IsEdit = 'Yes' where Username = '" + Id + "'", con);
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand("Update LoginTbl set IsEdit = 'Yes' where Username = '" + Id + "'", con);
                cmd4.CommandType = System.Data.CommandType.Text;
                cmd4.ExecuteNonQuery();
                con.Close();

                TempData["save"] = "Profile Update Disabled";
                return RedirectToAction("AllStaffForLock");


            }
        }

        public IActionResult EnableEditing(string Id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update StaffDetails set IsEdit = 'No' where EmailAddress = '" + Id + "'", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("Update Files set IsEdit = 'No' where Email = '" + Id + "'", con);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand("Update Experience set IsEdit = 'No' where Username = '" + Id + "'", con);
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.ExecuteNonQuery();
                SqlCommand cmd4 = new SqlCommand("Update LoginTbl set IsEdit = 'No' where Username = '" + Id + "'", con);
                cmd4.CommandType = System.Data.CommandType.Text;
                cmd4.ExecuteNonQuery();
                con.Close();

                TempData["save"] = "Profile Update Enable";
                return RedirectToAction("AllStaffForLock");


            }
        }

        public IActionResult AllStaffForLock()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            var staff = _adminService.GetLoginTbls();
            return View(staff);
        }


    }
}
