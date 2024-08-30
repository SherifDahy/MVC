using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ViewModels
{
    public class SignUpVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        [Compare("ConfirmPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
