using HomeworkAsyncAndFileSystem.Models;
using System;
using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class ProductAndCustomerService
    {
        public static async Task<List<CustomerModel>> GetCustomersAsync()
        {
            var fileResult = await File.ReadAllTextAsync("StaticFiles/Customers.json");

            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(fileResult);

            return customers;
        }

        public static async Task<List<ProductModel>> GetProductsAsync()
        {
            var fileResult = await File.ReadAllTextAsync("StaticFiles/Products.json");

            var products = JsonSerializer.Deserialize<List<ProductModel>>(fileResult);

            return products;
        }

        public static async Task<List<OrderModel>> GetOrdersAsync()
        {
            var customers = await GetCustomersAsync();
            var products = await GetProductsAsync();
            var orders = new List<OrderModel>();
            var joinOrders = products.Join(customers,
                                                q => q.Id,
                                                q => q.OrderedProductId,
                                                (product, customer) => new
                                                {
                                                    Product = product,
                                                    Customer = customer
                                                });
            foreach (var item in joinOrders)
            {
                var order = new OrderModel();
                order.ProductId = item.Product.Id;
                order.FirstName = item.Customer.FirstName;
                order.LastName = item.Customer.LastName;
                order.Order = String.Concat(item.Product.Manufacturer, " ", item.Product.Model);
                order.Price = item.Product.Price;
                orders.Add(order);
            }
            return orders;
        }

        public static async void AddCustomerAsync(CustomerModel customer)
        {
            var customers = await GetCustomersAsync();
            customers.Add(customer);
            using (FileStream fs = new FileStream("StaticFiles/Customers.json", FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync<List<CustomerModel>>(fs, customers);
            }
        }

        public static async void AddProductAsync(ProductModel product)
        {
            var products = await GetProductsAsync();
            products.Add(product);
            using (FileStream fs = new FileStream("StaticFiles/Products.json", FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync<List<ProductModel>>(fs, products);
            }
        }
    }
}
