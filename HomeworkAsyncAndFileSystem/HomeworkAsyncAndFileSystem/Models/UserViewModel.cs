using HomeworkAsyncAndFileSystem.Enums;

namespace HomeworkAsyncAndFileSystem.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; } 
    }
}
