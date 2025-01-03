﻿using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememeberMe { get; set; }
    }
}
