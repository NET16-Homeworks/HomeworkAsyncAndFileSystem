using AsyncApplication.Models;
using AsyncApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsyncApplication.Controllers
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
            await AsyncProductService.GetCustomers();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Customers()
        {
            var result = await AsyncProductService.GetCustomers();
            return View(result);
        }

        public async Task<IActionResult> Products()
        {
            var result = await AsyncProductService.GetProducts();
            return View(result);
        }

        public async Task<IActionResult> Purchases()
        {
            var result = await AsyncProductService.GetPurchases();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerViewModel customer)
        {
            try
            {
                await AsyncProductService.AddCustomer(customer);
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.Message;
            }
            
            return View();
        }

        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            await AsyncProductService.DeleteCustomer(CustomerId);
            return Redirect("Customers");
        }

        [HttpGet]
        public async Task<IActionResult> AddPurchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchase(ProductViewModel product)
        {
            try
            {
                await AsyncProductService.AddProduct(product);
            }
            catch (Exception ex)
            {
                ViewBag.Info = ex.Message;
            }

            return View();
        }

        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            await AsyncProductService.DeleteProduct(ProductId);
            return Redirect("Products");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}