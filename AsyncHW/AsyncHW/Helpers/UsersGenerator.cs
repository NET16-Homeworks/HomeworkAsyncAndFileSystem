using AsyncHW.Constants;
using AsyncHW.Models;
using Bogus;
using System.Net;
using System.Text.Json;

namespace AsyncHW.Helpers
{
    public sealed class UsersGenerator
    {
        public List<UserViewModel> Users { get; private set; }
        public void Generate(int count, List<AddressViewModel> addresses)
        {
            var userFaker = new Faker<UserViewModel>()
                .RuleFor(x => x.UserId, faker => Guid.NewGuid())
                .RuleFor(x => x.FirstName, faker => faker.Person.FirstName)
                .RuleFor(x => x.LastName, faker => faker.Person.LastName)
                .RuleFor(x => x.Email, faker => faker.Person.Email)
                .RuleFor(x => x.BirthDate, faker => faker.Person.DateOfBirth)
                .RuleFor(x => x.AddressId, faker => faker.PickRandom(addresses).AddressId);
            Users = userFaker.Generate(count);
        }

        public async Task Serialize()
        {
            using (var writer = File.OpenWrite(FilePath.UserFilePath))
            {
                await JsonSerializer.SerializeAsync(writer, Users, new JsonSerializerOptions() { WriteIndented = true});
            }
        }
    }
}
