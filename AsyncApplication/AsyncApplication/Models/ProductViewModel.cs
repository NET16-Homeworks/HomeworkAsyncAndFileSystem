using AsyncApplication.Enums;

namespace AsyncApplication.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public double Cost { get; set; }
        public DateTime PurchasingDate { get; set; }
        public string CustomerEmail { get; set; }
        public int ProductId { get; set; } 
    }
}
