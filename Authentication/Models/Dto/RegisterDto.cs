﻿using System.ComponentModel.DataAnnotations;

namespace Authentication.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Role { get; set; } = string.Empty;
    }
}
