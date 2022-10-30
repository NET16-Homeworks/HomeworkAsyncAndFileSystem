using HomeworkAsyncAndFileSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HomeworkAsyncAndFileSystem.Helpers;

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
            var users = Users.GetList();
            var addresses = Addresses.GetUserLinkedList(users);

            JSONWriter.Write(Constants.Path.directoryName, Constants.Path.usersFileName, users);
            JSONWriter.Write(Constants.Path.directoryName, Constants.Path.addressesFileName, addresses);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}