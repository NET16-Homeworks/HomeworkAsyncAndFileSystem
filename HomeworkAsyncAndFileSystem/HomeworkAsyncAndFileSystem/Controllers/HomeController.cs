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

        public async Task<IActionResult> Index()
        {
            await GenerateJSONs();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task GenerateJSONs()
        {
            string directoryName = Constants.Path.directoryName;
            string usersFileName = Constants.Path.usersFileName;
            string addressesFileName = Constants.Path.addressesFileName;

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            if (!System.IO.File.Exists(Path.Combine(directoryName, usersFileName)))
            {
                var users = Users.GetList();
                await JSONWriter.Write(directoryName, usersFileName, users);

                if (!System.IO.File.Exists(Path.Combine(directoryName, addressesFileName)))
                {
                    var addresses = Addresses.GetUserLinkedList(users);
                    await JSONWriter.Write(directoryName, addressesFileName, addresses);
                }
            }
        }
    }
}