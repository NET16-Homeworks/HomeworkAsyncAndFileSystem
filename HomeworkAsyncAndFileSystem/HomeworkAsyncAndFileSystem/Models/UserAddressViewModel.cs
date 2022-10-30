using HomeworkAsyncAndFileSystem.Enums;

namespace HomeworkAsyncAndFileSystem.Models
{
    public class UserAddressViewModel
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

        public static UserViewModel GetUserModel(UserAddressViewModel combinedModel, Guid? Id)
        {
            return new UserViewModel()
            {
                Id = (Guid)(Id == null ? combinedModel.Id : Id),
                FirstName = combinedModel.FirstName,
                LastName = combinedModel.LastName,
                Email = combinedModel.Email,
                Sex = combinedModel.Sex,
            };
        }

        public static AddressViewModel GetAddressModel(UserAddressViewModel combinedModel, Guid? Id)
        {
            return new AddressViewModel()
            {
                UserId = (Guid)(Id == null ? combinedModel.Id : Id),
                Country = combinedModel.Country,
                City = combinedModel.City,
                StreetName = combinedModel.StreetName,
                StreetAddress = combinedModel.StreetAddress,
            };
        }
    }
}
