using AsyncHW.Constants;
using AsyncHW.Models;
using System.Text.Json;

namespace AsyncHW.Services
{
    public sealed class UserService
    {
        private List<UserViewModel> _users;
        private List<AddressViewModel> _addresses;

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            if (_users == null)
            {
                _users = await DeserializeAsync<List<UserViewModel>>(FilePath.UserFilePath);
            }

            return _users;
        }

        public async Task<List<AddressViewModel>> GetAddressesAsync()
        {
            if (_addresses == null)
            {
                _addresses = await DeserializeAsync<List<AddressViewModel>>(FilePath.AddressFilePath);
            }

            return _addresses;
        }

        public async Task<List<UserWithAddressDTOViewModel>> GetUsersWithAddressesAsync()
        {
            if (_users == null)
            {
                _users = await DeserializeAsync<List<UserViewModel>>(FilePath.UserFilePath);
            }
            
            if (_addresses == null)
            {
                _addresses = await DeserializeAsync<List<AddressViewModel>>(FilePath.AddressFilePath);
            }

            var usersWithAddresses = _users.Join(_addresses,
                                                 user => user.AddressId,
                                                 address => address.AddressId,
                                                 (user, address) => new UserWithAddressDTOViewModel
                                                 {
                                                     UserId = user.UserId,
                                                     FirstName = user.FirstName,
                                                     LastName = user.LastName,
                                                     Email = user.Email,
                                                     BirthDate = user.BirthDate,
                                                     AddressId = user.AddressId,
                                                     City = address.City,
                                                     Street = address.Street,
                                                     HouseNumber = address.HouseNumber
                                                 }).ToList();

            return usersWithAddresses; 
        }

        private async Task<T> DeserializeAsync<T>(string path)
        {
            using var reader = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<T>(reader);
        }
    }
}
