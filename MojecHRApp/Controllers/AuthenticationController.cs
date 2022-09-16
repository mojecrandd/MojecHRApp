using Microsoft.AspNetCore.Mvc;
using MojecHRApp.Models;
using System.Data.SqlClient;
using System.Drawing;

namespace MojecHRApp.Controllers
{
    public class AuthenticationController : Controller
    {
        protected readonly IConfiguration _config;
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlCommand com2 = new SqlCommand();
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
            ConnectionString = _config.GetConnectionString("ConnectionString");
            ProviderName = "System.Data.SqlClient";
        }
        void connectionString()
        {
            con.ConnectionString = _config.GetConnectionString("ConnectionString");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginModel user)
        {
            try
            {
                user.Role = "";
                SqlDataReader dr;
                connectionString();
                con.Open();
                com.Connection = con;
                com.CommandText = "Select * from LoginTbl where Username = '" + user.UserName + "'and Password = '" + user.Password + "' and IsActive ='Active' ";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    user.UserName = dr["Username"].ToString();
                    user.Role = dr["Role"].ToString();
                    user.UserId = Convert.ToInt32(dr["UserID"].ToString());


                    if (user.Role == "Admin")
                    {
                        HttpContext.Session.SetString("Username", user.UserName);
                        TempData["save"] = "Login Successful";
                        return RedirectToAction("Dashboard", "Admin");
                    }

                    if (user.Role == "Staff")
                    {
                        HttpContext.Session.SetString("Username", user.UserName);
                        TempData["save"] = "Login Successful";
                        return RedirectToAction("RegisterStaffDetails", "Staff");
                    }

                    if (user.Role == "Management")
                    {
                        HttpContext.Session.SetString("Username", user.UserName);
                        TempData["save"] = "Login Successful";
                        return RedirectToAction("Dashboard", "Management");
                    }

                }

                TempData["delete"] = "Invalid Credentials Please Try Again";
                return View();
            }
            catch(Exception ex)
            {
                TempData["delete"] = "'" + ex + "'";
            }

            return View();
           

        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(LoginTbl user)
        {
            try
            {
                SqlDataReader dr;
                SqlDataReader dr2;
                connectionString();
                con.Open();
                com.Connection = con;
                com.CommandText = "Select * from EmailsTbl where Email = '" + user.Username + "'";
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    user.Username = dr["Email"].ToString();
                    dr.Close();
                    com2.Connection = con;
                    com2.CommandText = "Select * from LoginTbl where Username = '" + user.Username + "'";
                    dr2 = com2.ExecuteReader();
                    if(dr2.Read())
                    {
                        TempData["delete"] = "User With Email Already Exist";
                        return View();
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            con.Open();
                            SqlCommand cmd2 = new SqlCommand("RegisterModel", con);
                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd2.Parameters.AddWithValue("@Fullname", user.Fullname);
                            cmd2.Parameters.AddWithValue("@Username", user.Username);
                            cmd2.Parameters.AddWithValue("@Password", user.Password);
                            cmd2.Parameters.AddWithValue("@Role", "Staff");
                            cmd2.Parameters.AddWithValue("@IsActive", "Active");
                            cmd2.ExecuteNonQuery();
                        }
                    }

                   
                    TempData["save"] = "Registration Completed Successfully";
                    return RedirectToAction("Login");
                }           
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            TempData["delete"] = "Email has not been registered!";
            return RedirectToAction("Register");

        }


    }
}
