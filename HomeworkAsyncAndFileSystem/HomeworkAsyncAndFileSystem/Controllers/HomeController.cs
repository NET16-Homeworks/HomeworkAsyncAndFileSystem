using HomeworkAsyncAndFileSystem.Models;
using HomeworkAsyncAndFileSystem.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            var customers = await ProductAndCustomerService.GetCustomersAsync();
            return View(customers);
        }

        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await ProductAndCustomerService.GetProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await ProductAndCustomerService.GetOrdersAsync();
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
            if (customer.FirstName?.Length < 2)
            {
                ModelState.AddModelError("FirstName", "Недопустимая длина строки. Имя должно иметь больше 1 символа");
            }

            if (customer.LastName?.Length < 2)
            {
                ModelState.AddModelError("LastName", "Недопустимая длина строки. Имя должно иметь больше 1 символа");
            }

            List<int> productIds = new List<int>();
            var _products = await ProductAndCustomerService.GetProductsAsync();
            foreach (var product in _products)
            {
                productIds.Add(product.Id);
            }
            if (!productIds.Contains(customer.OrderedProductId))
            {
                ModelState.AddModelError("OrderedProductId", "Такого продукта не существует");
            }

            List<string> errors = new List<string>();
            foreach (var item in ModelState)
            {
                string errorMessages = "";
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
                errors.Add(errorMessages);
            }
            if (ModelState.IsValid)
            {
                ProductAndCustomerService.AddCustomerAsync(customer);
                return RedirectToAction("Index");
            }
            else
                return View("InvalidInput", errors);
        }

        [HttpGet]
        public async Task<IActionResult> AddProductAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductModel product)
        {
            List<int> productIds = new List<int>();
            var _products = await ProductAndCustomerService.GetProductsAsync();
            foreach (var item in _products)
            {
                productIds.Add(item.Id);
            }
            if (productIds.Contains(product.Id))
            {
                ModelState.AddModelError("Id", "Такой продукт уже существует");
            }

            List<string> errors = new List<string>();
            foreach (var item in ModelState)
            {
                string errorMessages = "";
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
                    foreach (var error in item.Value.Errors)
                    {
                        errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                    }
                }
                errors.Add(errorMessages);
            }

            if (ModelState.IsValid)
            {
                ProductAndCustomerService.AddProductAsync(product);
                return RedirectToAction("Index");
            }
            else
                return View("InvalidInput", errors);

        }
    }
}