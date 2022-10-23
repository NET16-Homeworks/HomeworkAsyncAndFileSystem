using HomeworkAsyncAndFileSystem.Models;
using HomeworkAsyncAndFileSystem.Helpers;
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

        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await ProductAndCustomerService.GetCustomersAsync() ;
            return View(customers);
        }

        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await ProductAndCustomerService.GetProductsAsync() ;
            return View(products);
        }

        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await ProductAndCustomerService.GetOrdersAsync() ;
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> AddCustomerAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(CustomerModel customer)
        {
            ProductAndCustomerService.AddCustomerAsync(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddProductAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductModel product)
        {
            ProductAndCustomerService.AddProductAsync(product);
            return RedirectToAction("Index");
        }
    }
}