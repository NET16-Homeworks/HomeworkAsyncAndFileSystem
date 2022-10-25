using AsyncApplication.Enums;

namespace AsyncApplication.Models
{
    public class CombinedViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public ProductType ProductType { get; set; }
        public double ProductCost { get; set; }
        public DateTime PurchasingDate { get; set; }

    }
}