using Microsoft.AspNetCore.Mvc;
using HomeworkAsyncAndFileSystem.Helpers;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Controllers
{
    public class AddressesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string path = Constants.Path.GetAddressesJSONFullPath();
            var addresses = await JSONReader.Read<List<AddressViewModel>>(path);

            return View(addresses);
        }
    }
}
