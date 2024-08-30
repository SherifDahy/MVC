using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class SignInVM
    {
            public int Id { get; set; }
            [Required]
            [MaxLength(100)]
            [Display(Name = "Email Address")]
            public string Email { get; set; }
            [Required, MaxLength(100)]
            [DataType(DataType.Password)]
            public string Password { get; set; }
    }
}
