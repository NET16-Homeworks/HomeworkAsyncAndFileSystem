using AsyncHW.Constants;
using AsyncHW.Helpers;
using AsyncHW.Models;
using AsyncHW.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsyncHW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly UserService _userService = new UserService();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            await GenerateJsonsFiles();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _userService.GetAddressesAsync();
            return View(addresses);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersWithAddresses()
        {
            var usersWithAddresses = await _userService.GetUsersWithAddressesAsync();
            return View(usersWithAddresses);
        }

        private async Task GenerateJsonsFiles()
        {   
            var isJsonUsersEmpty = new FileInfo(FilePath.UserFilePath).Length == 0;
            var isJsonAddressesEmpty = new FileInfo(FilePath.AddressFilePath).Length == 0;

            if (isJsonAddressesEmpty && isJsonUsersEmpty)
            {
                var addressesGenerator = new AddressesGenerator();
                var usersGenerator = new UsersGenerator();

                addressesGenerator.Generate(20);
                usersGenerator.Generate(20, addressesGenerator.Addresses);

                await addressesGenerator.Serialize();
                await usersGenerator.Serialize();
            }

            return;
        }
    }
}