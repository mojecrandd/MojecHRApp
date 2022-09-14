using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MojecHRApp.BusinessManager.StaffLayer;
using MojecHRApp.DAL;
using MojecHRApp.Migrations;
using MojecHRApp.Models;
using System.Configuration.Provider;

namespace MojecHRApp.Controllers
{
    public class StaffController : Controller
    {
        IConfiguration _config;
        
        public IStaffServices _staffservices { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        private HRDbContext _dbContext;
        public StaffController(IStaffServices staffservices,HRDbContext dbContext, IConfiguration config)
        {
            _staffservices = staffservices;
            _dbContext = dbContext;
            _config = config;
            ConnectionString = _config.GetConnectionString("ConnectionString");
            ProviderName = "System.Data.SqlClient";
        }
        public IActionResult Dashboard()
       {
          
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View();
       }
        public IActionResult RegisterStaffDetails()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }

            string? username = HttpContext.Session.GetString("Username");
            var staffdetails = _dbContext.StaffDetails.Where(x => x.EmailAddress == username).FirstOrDefault();

            if (staffdetails == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("EditStaffDetails");
            }
                 
           
        }
        [HttpPost]
        public IActionResult RegisterStaffDetails([FromForm]StaffDetails details)
       {
            string? username = HttpContext.Session.GetString("Username");     
            details.EmailAddress = username;
            _staffservices.Create(details);         
            return View();
       }
        [HttpGet]
        public IActionResult EditStaffDetails(string? Email)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            Email = HttpContext.Session.GetString("Username");
            var staffdetails = _staffservices.GetStaffDetailsbyemail(Email);
            return View(staffdetails);
        }
        [HttpPost]
        public IActionResult EditStaffDetails([FromForm]StaffDetails details)
        {
            details.EmailAddress = HttpContext.Session.GetString("Username");
            _staffservices.Update(details);
            TempData["save"] = "Data updated successfully";
            return View();
        }
        [HttpGet]
        public IActionResult GetStaffExperience(string? email)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            email = HttpContext.Session.GetString("Username");
            var experience =  _staffservices.GetExperienceByEmail(email);
            return View(experience);
        } 
        public IActionResult CreateExperience()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            string? username = HttpContext.Session.GetString("Username");
            experience.Username = username;
            _staffservices.CreateExperience(experience);
            TempData["save"] = "Data Added Successfully";
            return RedirectToAction("GetStaffExperience");
        }
        public IActionResult EditExperience(int Id)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            var staffdetails = _staffservices.GetExperienceByID(Id);
            return View(staffdetails);
        }
        [HttpPost]
        public IActionResult EditExperience(Experience experience)
        {
            experience.Username = HttpContext.Session.GetString("Username");
            _staffservices.UpdateExperience(experience);
            TempData["save"] = "Data updated successfully";
            return RedirectToAction("GetStaffExperience");
        }
        public IActionResult UploadFile()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult UploadFile(List<IFormFile> postedFiles, Files files)
        {
            files.Email = HttpContext.Session.GetString("Username");
            foreach(IFormFile postedFile in postedFiles)
            {
                files.Name = Path.GetFileName(postedFile.FileName);
                files.ContentType = postedFile.ContentType;
                files.Data = null;

                using(MemoryStream ms = new MemoryStream())
                {
                    postedFile.CopyTo(ms);
                    files.Data = ms.ToArray();  
                }

                using (SqlConnection con  = new SqlConnection(ConnectionString))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "Insert into Files(Name,ContentType,Data,Email,FileType) Values(@Name,@ContentType,@Data,@Email,@FileType)";
                        cmd.Parameters.AddWithValue("@Name", files.Name);
                        cmd.Parameters.AddWithValue("@ContentType", files.ContentType);
                        cmd.Parameters.AddWithValue("@Data", files.Data);
                        cmd.Parameters.AddWithValue("@Email", files.Email);
                        cmd.Parameters.AddWithValue("@FileType",files.FileType);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                
            }

            TempData["save"] = "Document Uploaded Successfully";
            return RedirectToAction("StaffFiles");
        }
        private  List<Files> PopulateFiles(string? email)
        {
            email = HttpContext.Session.GetString("Username");
            List<Files> files = new List<Files>();
            using (SqlConnection con  = new SqlConnection(ConnectionString))
            {
                string query = "select * from Files where Email = @Email";
                using(SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Email", email);
                    con.Open();
                    using(SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new Files
                            {
                                Id = Convert.ToInt32(sdr["ID"]),
                                Name = sdr["Name"].ToString(),
                                ContentType = sdr["ContentType"].ToString(),
                                Data =(byte[]) sdr["Data"],
                                FileType = sdr["FileType"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return files;
        }
        public IActionResult StaffFiles(string? email)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View(PopulateFiles(email));
        }
        public IActionResult DeleteFiles(int Id)
        {
            var files = _dbContext.Files.Find(Id);
            _dbContext.Files.Remove(files);
            _dbContext.SaveChanges();   
            return RedirectToAction("StaffFiles");
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
        
        public IActionResult LeaveRequest(string? email)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");        
            }
            email = HttpContext.Session.GetString("Username");
            var request = _staffservices.GetLeaveRequestbyEmail(email);
            return View(request);
        }

        public IActionResult NewRequest()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult NewRequest(LeaveRequest leaveRequest)
        {
            string? username = HttpContext.Session.GetString("Username");
            leaveRequest.Email = username;
            _staffservices.CreateRequest(leaveRequest);
            TempData["save"] = "Request Sent Successfully";
            return RedirectToAction("LeaveRequest");
        }
      




    }
}
