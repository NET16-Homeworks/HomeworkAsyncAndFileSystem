using Microsoft.Build.Framework;

namespace HomeworkAsyncAndFileSystem.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
