﻿namespace MojecHRApp.Models
{
    public class UserLoginModel
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty ;
        public string? Role { get; set; } = string.Empty;
        public int? UserId { get; set; } 
    }
}
