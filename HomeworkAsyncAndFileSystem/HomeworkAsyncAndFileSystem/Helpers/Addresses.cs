using Bogus;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class Addresses
    {
        public static List<AddressViewModel> GetUserLinkedList(List<UserViewModel> users)
        {
            List<AddressViewModel> addresses = new();

            foreach (var user in users)
            {
                var address = new Faker<AddressViewModel>()
            .RuleFor(u => u.UserId, f => user.Id)
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.StreetName, f => f.Address.StreetName())
            .RuleFor(u => u.StreetAddress, f => f.Random.Int(1, 200));

                addresses.Add(address.Generate());
            }

            return addresses;
        }
    }
}
