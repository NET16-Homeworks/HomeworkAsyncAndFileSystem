using Microsoft.AspNetCore.Mvc;
using HomeworkAsyncAndFileSystem.Helpers;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string path = Constants.Path.GetUsersJSONFullPath();
            var users = await JSONReader.ReadAsync<List<UserViewModel>>(path);

            return View(users);
        }
    }
}
