using Bogus;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class Address
    {
        public static AddressViewModel GetAddress()
        {
            var address = new Faker<AddressViewModel>()
         .RuleFor(u => u.Country, f => f.Address.Country())
         .RuleFor(u => u.City, f => f.Address.City())
         .RuleFor(u => u.StreetName, f => f.Address.StreetName())
         .RuleFor(u => u.StreetAddress, f => f.Random.Int(1, 200));

            return address.Generate();
        }
    }
}
