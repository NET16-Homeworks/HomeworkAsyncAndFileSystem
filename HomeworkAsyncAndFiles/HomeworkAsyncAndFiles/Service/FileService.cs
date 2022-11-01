using HomeworkAsyncAndFiles.Models;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace HomeworkAsyncAndFiles.Helpers
{
    public static class FileService
    {
        private const string customersPath = "StaticFiles/Customers.json";
        private const string productsPath = "StaticFiles/Products.json";
        public async static Task<List<CustomerModel>> GetCustomersAsync()
        {
            var stringWithCustomers = await File.ReadAllTextAsync(customersPath);
            var listWithCustomers = JsonSerializer.Deserialize<List<CustomerModel>>(stringWithCustomers);
            return listWithCustomers;
        }
        public async static Task<List<ProductModel>> GetProductsAsync()
        {
            var stringWithProducts = await File.ReadAllTextAsync(productsPath);
            var listWithProducts = JsonSerializer.Deserialize<List<ProductModel>>(stringWithProducts);
            return listWithProducts;
        }
        public async static Task AddCustomerAsync(CustomerModel customer)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), customersPath);
            var writeStream = File.Open(filePath, FileMode.Open);
            var customerString = ",\n" + JsonSerializer.Serialize(customer) + "\n]";
            byte[] buffer = Encoding.Default.GetBytes(customerString);
            writeStream.Seek(-1, SeekOrigin.End);
            await writeStream.WriteAsync(buffer, 0, buffer.Length);
            writeStream.Close();
        }
        public async static Task AddProductAsync(ProductModel product)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), productsPath);
            var writeStream = File.Open(filePath, FileMode.Open);
            var productString = ",\n" + JsonSerializer.Serialize(product) + "\n]";
            byte[] buffer = Encoding.Default.GetBytes(productString);
            writeStream.Seek(-1, SeekOrigin.End);
            await writeStream.WriteAsync(buffer, 0, buffer.Length);
            writeStream.Close();
        }

        public async static Task DeleteCustomerAsync(int customerId)
        {
            var stringWithCustomers = await File.ReadAllTextAsync(customersPath);
            var listWithCustomers = JsonSerializer.Deserialize<List<CustomerModel>>(stringWithCustomers);
            var customer = listWithCustomers.Where(q => q.CustomerId == customerId).SingleOrDefault();
            listWithCustomers.Remove(customer);
            var changedCustomerString = JsonSerializer.Serialize(listWithCustomers);
            await File.WriteAllTextAsync(customersPath,changedCustomerString);
        }

        public async static Task DeleteProductAsync(int productNumber)
        {
            var stringWithProducts = await File.ReadAllTextAsync(productsPath);
            var listWithProducts = JsonSerializer.Deserialize<List<ProductModel>>(stringWithProducts);
            var product = listWithProducts.Where(q => q.Number == productNumber).SingleOrDefault();
            listWithProducts.Remove(product);
            var changedProductsString = JsonSerializer.Serialize(listWithProducts);
            await File.WriteAllTextAsync(productsPath, changedProductsString);
        }
    }
}


