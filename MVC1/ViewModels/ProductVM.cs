using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50),Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [MaxLength (150),Display(Name = "Product Description")]
        public string ProductDesc { get; set; }
        [Required]
        [Range(0,100000),Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required, MaxLength(50),Display(Name = "Default Image")]
        public string DefaultImage { get; set; }
        [Required, MaxLength(150),Display(Name = "Images")]
        public string Images { get; set; }

        [RegularExpression("^[1-9]\\d*$|^-[1-9]\\d*$", ErrorMessage = "Please Select any of Category.")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
    }
}
