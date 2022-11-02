using AsyncHW.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AsyncHW.Services
{
    public sealed class UserService
    {
        private List<UserViewModel> _users = new List<UserViewModel>();
        private List<AddressViewModel> _addresses = new List<AddressViewModel>();

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            using (var reader = File.OpenRead("JSONs/Users.json"))
            {
                _users = await JsonSerializer.DeserializeAsync<List<UserViewModel>>(reader);
                return _users;
            }
        }

        public async Task<List<AddressViewModel>> GetAddressesAsync()
        {
            using (var reader = File.OpenRead("JSONs/Addresses.json"))
            {
                _addresses = await JsonSerializer.DeserializeAsync<List<AddressViewModel>>(reader);
                return _addresses;
            }
        }
    }
}
