namespace AsyncHW.Models
{
    public sealed class UserViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid? AddressId { get; set; }
    }
}
