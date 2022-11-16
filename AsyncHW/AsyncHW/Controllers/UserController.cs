using AsyncHW.Models;
using DI.Entities.Entities;
using DI.UserInFile.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AsyncHW.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            var result = users.Select(x => new UserViewModel
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                BirthDate = x.BirthDate,
                AddressId = x.AddressId,
            }).ToArray();

            return View(result);
        }

        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _userService.GetAddresses();
            var result = addresses.Select(x => new AddressViewModel
            {
                AddressId = x.AddressId,
                City = x.City,
                Street = x.Street,
                HouseNumber = x.HouseNumber,
            }).ToArray();

            return View(result);
        }

        public async Task<IActionResult> GetUsersWithAddresses()
        {
            var users = await _userService.GetUsers();
            var addresses = await _userService.GetAddresses();

            var result = users.Join(addresses,
                                    u => u.AddressId,
                                    a => a.AddressId,
                                    (u, a) => new UserWithAddressDTOViewModel
                                    {
                                        UserId = u.UserId,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        Email = u.Email,
                                        BirthDate = u.BirthDate,
                                        AddressId = a.AddressId,
                                        City = a.City,
                                        Street = a.Street,
                                        HouseNumber = a.HouseNumber,
                                    }).ToArray();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel user)
        {
            await _userService
                .AddUser(user.UserId, user.FirstName, user.LastName, user.Email, user.BirthDate, user.AddressId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressViewModel address)
        {
            await _userService
                .AddAddress(address.AddressId, address.City, address.Street, address.HouseNumber);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddAddress()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            await _userService.DeleteUser(userId);

            return RedirectToAction("GetUsers");
        }

        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            await _userService.DeleteAddress(addressId);

            return RedirectToAction("GetAddresses");
        }
    }
}
