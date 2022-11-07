using AsyncWebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AsyncWebApplication1.Helpers;

namespace AsyncWebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[Route("Home/GetUsers")]
        public async Task <IActionResult> GetUsers()
        {
            var users = await UserHelper.GetUsersAsync();
            return View(users);
        }

        //[Route("Home/GetAddressesAsync")]
        public async Task <IActionResult> GetAddresses()
        {
            var address = await UserHelper.GetAddressesAsync();
            return View(address);
        }

        public async Task<IActionResult> GetUsersPerCity()
        {
            var combined = await UserHelper.GetUsersPerCityAsync();
            return View(combined);
        }

        [HttpGet]
        public IActionResult AddUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers(UserModel user)
        {
            await UserHelper.AddUsersAsync(user);
            Console.WriteLine("The new user has been added");
            return RedirectToAction("GetUsers");
        }

        [HttpGet]
        public IActionResult AddAddresses()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAddresses(AddressModel address)
        {
           await UserHelper.AddAddressesAsync(address);
            Console.WriteLine("The new address has been added");
            return RedirectToAction("GetAddresses");
        }

        [HttpGet]
        public IActionResult DeleteUsers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(int index)
        {
            await UserHelper.DeleteUsersAsync(index);
            return RedirectToAction("GetUsers");
        }
    }
}