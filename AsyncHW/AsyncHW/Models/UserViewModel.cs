namespace AsyncHW.Models
{
    public sealed class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public AddressViewModel Address { get; set; }
    }
}
