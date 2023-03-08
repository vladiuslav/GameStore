﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class LoginData
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
