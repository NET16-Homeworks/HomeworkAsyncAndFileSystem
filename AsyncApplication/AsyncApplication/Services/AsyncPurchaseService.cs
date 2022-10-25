using AsyncApplication.Models;
using System.Text.Json;

namespace AsyncApplication.Services
{
    public static class AsyncProductService
    {
        public static async Task<List<CustomerViewModel>> GetCustomers()
        {
            using (FileStream fs = new FileStream("Jsons/Customers.json", FileMode.OpenOrCreate))
            {
                List<CustomerViewModel> customers = await JsonSerializer.DeserializeAsync<List<CustomerViewModel>>(fs);
                return customers;
            }
        }

        public static async Task<List<ProductViewModel>> GetProducts()
        {
            using (FileStream fs = new FileStream("Jsons/Products.json", FileMode.OpenOrCreate))
            {
                List<ProductViewModel> products = await JsonSerializer.DeserializeAsync<List<ProductViewModel>>(fs);
                return products;
            }
        }

        public static async Task<List<CombinedViewModel>> GetPurchases()
        {
            List<CustomerViewModel> customers = GetCustomers().Result;
            List<ProductViewModel> products = GetProducts().Result;

            var joinResult = customers.Join(products,
                c => c.Email,
                p => p.CustomerEmail,
                (p, c) => new
                {
                    p.FirstName,
                    p.LastName,
                    p.Email,
                    ProductName = c.Name,
                    ProductType = c.Type,
                    ProductCost = c.Cost,
                    c.PurchasingDate
                });
            List<CombinedViewModel> combined = new List<CombinedViewModel>();
            foreach (var result in joinResult)
            {
                combined.Add(new CombinedViewModel() { Email = result.Email, FirstName = result.FirstName, LastName = result.LastName, ProductCost = result.ProductCost, ProductName = result.ProductName, ProductType = result.ProductType, PurchasingDate = result.PurchasingDate });
            }

            return combined;
        }

        public static async void AddCustomer(CustomerViewModel customer)
        {
            if (!GetCustomers().Result.Any(q => q.Email == customer.Email))
            {
                List<CustomerViewModel> customers = GetCustomers().Result;
                customer.CreationDate = DateTime.Now;
                customer.CustomerId = customer.GetHashCode();
                customers.Add(customer);

                using (FileStream fs = new FileStream("Jsons/Customers.json", FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, customers);
                }
            }
        }

        public static async void AddProduct(ProductViewModel product)
        {
            List<CustomerViewModel> customers = GetCustomers().Result;
            if (customers.Any(q => q.Email == product.CustomerEmail))
            {
                CustomerViewModel customer = customers.First(q => q.Email == product.CustomerEmail);
                List<ProductViewModel> products = GetProducts().Result;
                product.PurchasingDate = DateTime.Now;
                product.ProductId = product.GetHashCode();
                if (customer.ProductHashCode != null)
                {
                    customer.ProductHashCode.Add(product.ProductId);
                }
                else
                {
                    List<int> productHashCodes = new List<int>();
                    productHashCodes.Add(product.ProductId);
                    customer.ProductHashCode = productHashCodes;
                }
                products.Add(product);
                using (FileStream fs = new FileStream("Jsons/Products.json", FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, products);
                }

                customers.First(q => q.Email == product.CustomerEmail).ProductHashCode.Add(product.ProductId);
                using (FileStream fs = new FileStream("Jsons/Customers.json", FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, customers);
                }
            }
        }

        public static async void DeleteCustomer(int Id)
        {
            List<CustomerViewModel> customers = GetCustomers().Result;
            if (customers.Any(q => q.CustomerId == Id))
            {
                var customer = customers.First(q => q.CustomerId == Id);
                if (customer.ProductHashCode != null)
                {

                    foreach (var product in customer.ProductHashCode)
                    {
                        DeleteProduct(product);
                    }
                }
                customers.Remove(customer);
                File.WriteAllText("Jsons/Customers.json", "[]");
            }

            using (FileStream fs = new FileStream("Jsons/Customers.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync(fs, customers);
            }
        }

        public static async void DeleteProduct(int Id)
        {
            var customers = GetCustomers().Result;
            var products = GetProducts().Result;
            if (products.Any(q => q.ProductId == Id))
            {
                var product = products.First(q => q.ProductId == Id);
                if (customers.Any())
                {
                    customers.First(q => q.Email == product.CustomerEmail).ProductHashCode.Remove(Id);
                }
                products.Remove(product);
                File.WriteAllText("Jsons/Products.json", "[]");
                File.WriteAllText("Jsons/Customers.json", "[]");

                using (FileStream fs = new FileStream("Jsons/Products.json", FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, products);
                }
                using (FileStream fs = new FileStream("Jsons/Customers.json", FileMode.OpenOrCreate))
                {
                    await JsonSerializer.SerializeAsync(fs, customers);
                }
            }
        }
    }
}
