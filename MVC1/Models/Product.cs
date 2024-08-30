using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal Price { get; set; }
        public string DefaultImage { get; set; }
        public string  Images { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
