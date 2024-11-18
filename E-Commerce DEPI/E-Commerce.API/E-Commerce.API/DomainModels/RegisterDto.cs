using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,Compare("Password"),DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
    }
}
