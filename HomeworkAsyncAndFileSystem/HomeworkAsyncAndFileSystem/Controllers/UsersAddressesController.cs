using Microsoft.AspNetCore.Mvc;
using HomeworkAsyncAndFileSystem.Helpers;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Controllers
{
    public class UsersAddressesController : Controller
    {
        private string usersJSONPath = Constants.Path.GetUsersJSONFullPath();
        private string addressesJSONPath = Constants.Path.GetAddressesJSONFullPath();
        public async Task<IActionResult> Index()
        {
            var users = await JSONReader.ReadAsync<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.ReadAsync<List<AddressViewModel>>(addressesJSONPath);

            List<UserAddressViewModel> usersAddressesJoin = users.Join(
                addresses,
                user => user.Id,
                address => address.UserId,
                (user, address) => new UserAddressViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Sex = user.Sex,
                    Country = address.Country,
                    City = address.City,
                    StreetName = address.StreetName,
                    StreetAddress = address.StreetAddress,
                }
                ).ToList();

            return View(usersAddressesJoin);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(UserAddressViewModel model)
        {
            await AddNewUserToJSON(model);

            ModelState.Clear();

            return RedirectToAction("Index", "UsersAddresses");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await DeleteUserFromJSON(id);

            return RedirectToAction("Index", "UsersAddresses");
        }

        private async Task AddNewUserToJSON(UserAddressViewModel model)
        {
            Guid entityId = Guid.NewGuid();
            var newUser = UserAddressViewModel.GetUserModel(model, entityId);
            var newAddress = UserAddressViewModel.GetAddressModel(model, entityId);

            var users = await JSONReader.ReadAsync<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.ReadAsync<List<AddressViewModel>>(addressesJSONPath);

            users.Add(newUser);
            addresses.Add(newAddress);

            await JSONWriter.WriteAsync(usersJSONPath, users);
            await JSONWriter.WriteAsync(addressesJSONPath, addresses);
        }

        private async Task DeleteUserFromJSON(Guid id)
        {
            var users = await JSONReader.ReadAsync<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.ReadAsync<List<AddressViewModel>>(addressesJSONPath);

            var user = users.Find(user => user.Id == id);
            var address = addresses.Find(address => address.UserId == id);

            if (user == null || address == null)
            {
                throw new Exception("User not found!");
            }

            users.Remove(user);
            addresses.Remove(address);

            await JSONWriter.WriteAsync(usersJSONPath, users);
            await JSONWriter.WriteAsync(addressesJSONPath, addresses);
        }
    } 
}