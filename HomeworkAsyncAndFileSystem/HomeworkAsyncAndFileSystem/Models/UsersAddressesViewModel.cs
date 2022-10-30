using HomeworkAsyncAndFileSystem.Enums;

namespace HomeworkAsyncAndFileSystem.Models
{
    public class UsersAddressesViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetAddress { get; set; }
    }
}
