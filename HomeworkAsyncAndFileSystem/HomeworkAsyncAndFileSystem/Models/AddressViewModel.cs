namespace HomeworkAsyncAndFileSystem.Models
{
    public class AddressViewModel
    {
        public Guid UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetAddress { get; set; }
    }
}
