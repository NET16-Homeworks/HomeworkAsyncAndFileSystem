using AsyncHW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsyncHW.Controllers
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
            return View();
        }
    }
}