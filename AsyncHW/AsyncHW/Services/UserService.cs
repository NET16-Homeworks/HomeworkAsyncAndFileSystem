using AsyncHW.Constants;
using AsyncHW.Models;
using System.Text.Json;

namespace AsyncHW.Services
{
    public sealed class UserService
    {
        //private List<UserViewModel> _users = new List<UserViewModel>();
        //private List<AddressViewModel> _addresses = new List<AddressViewModel>();

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            using (var reader = File.OpenRead(FilePath.UserFilePath))
            {
                return await JsonSerializer.DeserializeAsync<List<UserViewModel>>(reader);
            }
        }

        public async Task<List<AddressViewModel>> GetAddressesAsync()
        {
            using (var reader = File.OpenRead(FilePath.AddressFilePath))
            {
                return await JsonSerializer.DeserializeAsync<List<AddressViewModel>>(reader);
            }
        }
    }
}
