using Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModels
{
    public class EmployeeVM
    {
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name ="First Name")]
        public string firstName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Required]
        [StringLength(11)]
        [Display(Name = "Phone Number")]
        public string phoneNumber { get; set; }
        [Required]
        [Range(minimum:0,maximum:100000)]
        [Display(Name = "Salary")]
        public decimal salary { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string title { get; set; }
        [Required]
        [RegularExpression("^[1-9]\\d*$|^-[1-9]\\d*$",ErrorMessage = "Please Select any of Dept.")]
        public int DepartmentId { get; set; }
    }
}
