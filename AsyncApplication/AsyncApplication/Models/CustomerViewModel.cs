using AsyncApplication.Enums;

namespace AsyncApplication.Models
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
        public List<int> ProductHashCode { get; set; }
        public DateTime CreationDate { get; set; }
        public int CustomerId { get; set;}
    }
}
