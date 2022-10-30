using Microsoft.AspNetCore.Mvc;
using HomeworkAsyncAndFileSystem.Helpers;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Controllers
{
    public class UsersAddressesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string usersJSONPath = Constants.Path.GetUsersJSONFullPath();
            string addressesJSONPath = Constants.Path.GetAddressesJSONFullPath();
            var users = await JSONReader.Read<List<UserViewModel>>(usersJSONPath);
            var addresses = await JSONReader.Read<List<AddressViewModel>>(addressesJSONPath);

            var usersAddressesJoin = users.Join(
                addresses,
                user => user.Id,
                address => address.UserId,
                (user, address) => new UsersAddressesViewModel()
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
    }
}