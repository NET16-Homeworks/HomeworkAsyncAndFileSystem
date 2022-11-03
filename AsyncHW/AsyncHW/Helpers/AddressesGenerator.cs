using AsyncHW.Constants;
using AsyncHW.Models;
using Bogus;
using System.Text.Json;

namespace AsyncHW.Helpers
{
    public sealed class AddressesGenerator
    {
        public List<AddressViewModel> Addresses { get; private set; }
        public void Generate(int count)
        {
            var addressFaker = new Faker<AddressViewModel>()
                .RuleFor(x => x.AddressId, faker => Guid.NewGuid())
                .RuleFor(x => x.City, faker => faker.Address.City())
                .RuleFor(x => x.Street, faker => faker.Address.StreetAddress())
                .RuleFor(x => x.HouseNumber, faker => faker.Address.BuildingNumber());
            Addresses = addressFaker.Generate(count);
        }

        public async Task Serialize()
        {
            using (var writer = File.OpenWrite(FilePath.AddressFilePath))
            {
                await JsonSerializer.SerializeAsync(writer, Addresses, new JsonSerializerOptions() { WriteIndented = true });
            }
        }
    }
}
