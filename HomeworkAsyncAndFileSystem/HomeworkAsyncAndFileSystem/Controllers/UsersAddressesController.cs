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
            var users = await JSONReader.Read<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.Read<List<AddressViewModel>>(addressesJSONPath);

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
            return View();  //add success message
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(UserAddressViewModel model)
        {
            Guid entityId = Guid.NewGuid();
            var newUser = UserAddressViewModel.GetUserModel(model, entityId);
            var newAddress =  UserAddressViewModel.GetAddressModel(model, entityId);

            var users = await JSONReader.Read<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.Read<List<AddressViewModel>>(addressesJSONPath);

            users.Add(newUser);
            addresses.Add(newAddress);

            await JSONWriter.Write(usersJSONPath, users);
            await JSONWriter.Write(addressesJSONPath, addresses);

            ModelState.Clear();

            return View();
        }
    }
}