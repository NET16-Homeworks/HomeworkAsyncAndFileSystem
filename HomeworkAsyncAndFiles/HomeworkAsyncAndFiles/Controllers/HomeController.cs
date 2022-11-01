using HomeworkAsyncAndFiles.Helpers;
using HomeworkAsyncAndFiles.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace HomeworkAsyncAndFiles.Controllers
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
        public async Task<IActionResult> GetCustomersAsync()
        {
            var listWithCustomers = await FileService.GetCustomersAsync();
            return View(listWithCustomers.OrderBy(q => q.CustomerId));
        }
        public async Task<IActionResult> GetProductsAsync()
        {
            var listWithProducts = await FileService.GetProductsAsync();
            return View(listWithProducts.OrderBy(q => q.Number));
        }
        public async Task<IActionResult> GetOrdersAsync()
        {
            var listWithProducts = await FileService.GetProductsAsync();
            var listWithCustomers = await FileService.GetCustomersAsync();
            var relatedList = listWithCustomers.Join(listWithProducts, q => q.ProductNumber, w => w.Number, (q, w) => new OrderModel
            {
                ProductNumber = w.Number,
                ProductName = w.Name,
                ProductWeight = w.Weight,
                ProductPrice = w.Price,
                DateOfCreating = w.DateOfCreating,
                CustomerId = q.CustomerId,
                CustomersFirstName = q.FirstName,
                CustomersLastName = q.LastName,
                CustomersEmail = q.Email,
                CustomersAge = q.Age

            }).OrderBy(q => q.CustomerId);
            return View(relatedList);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerAsync(CustomerModel customer)
        {
            await FileService.AddCustomerAsync(customer);
            return View();
        }
        [HttpGet]
        public IActionResult AddCustomerAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductModel product)
        {
            await FileService.AddProductAsync(product);
            return View();
        }
        [HttpGet]
        public IActionResult AddProductAsync()
        {
            return View();
        }
        public async Task<IActionResult> DeleteCustomerAsync(int customerId)
        {
            await FileService.DeleteCustomerAsync(customerId);
            return RedirectToAction("GetCustomers");
        }
        public async Task<IActionResult> DeleteProductAsync(int productNumber)
        {
            await FileService.DeleteProductAsync(productNumber);
            return RedirectToAction("GetProducts");
        }
    }
}