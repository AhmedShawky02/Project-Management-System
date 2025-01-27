﻿using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Accounts
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
