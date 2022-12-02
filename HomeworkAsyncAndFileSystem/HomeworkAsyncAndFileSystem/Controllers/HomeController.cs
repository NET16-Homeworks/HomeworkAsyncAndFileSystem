using HomeworkAsyncAndFileSystem.Helpers;
using HomeworkAsyncAndFileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HomeworkAsyncAndFileSystem.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetDrivers()
        {
            var owners = await Service.GetDrivers();
            return View(owners);
        }

        public async Task<IActionResult> GetCars()
        {
            var cars = await Service.GetCars();
            return View(cars);
        }

        public async Task<IActionResult> GetCarDrivers()
        {
            var carDrivers =  await Service.GetCarDrivers();
            return View(carDrivers);
        }

        [HttpGet]
        public IActionResult AddDriver()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDriver(DriverModel driver)
        {
            await Service.AddDriver(driver);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(CarModel car)
        {
            await Service.AddCar(car);
            return RedirectToAction("Index");
        }
    }
}