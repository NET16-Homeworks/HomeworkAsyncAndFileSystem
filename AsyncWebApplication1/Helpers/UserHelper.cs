using AsyncWebApplication1.Models;
using System.Text.Json;
using System.IO;
using System.Text;

namespace AsyncWebApplication1.Helpers
{
    public static class UserHelper
    {
        public static async Task<List<UserModel>> GetUsersAsync()
        {
            var userFile = await File.ReadAllTextAsync("StaticFiles/Users.json");
            var users = JsonSerializer.Deserialize<List<UserModel>>(userFile);
            return users;
        }

        public static async Task<List<AddressModel>> GetAddressesAsync()
        {
            var addressFile = await File.ReadAllTextAsync("StaticFiles/Address.json");
            var addresses = JsonSerializer.Deserialize<List<AddressModel>>(addressFile);
            return addresses;
        }

        public static async Task<List<UsersCityModel>> GetUsersPerCityAsync()
        {
            var users = await GetUsersAsync();
            var addresses = await GetAddressesAsync();

            var joined = users.Join(addresses, u => u.Town, a => a.City,
                (u, a) => new { User = u, Address = a });

            var selected = joined.Select(j => new UsersCityModel()
            {
                FirstName = j.User.FirstName,
                LastName = j.User.LastName,
                City = j.Address.City,
            }).ToList();

            return selected;
        }

        //Реализовать возможность ввода данных для этих сущностей с помощью форм и добавление их в файл.
        
        public static async Task AddUsersAsync(UserModel user)
        {
            var users = await GetUsersAsync();

            users.Add(user);
            string text = ",";
            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text);
                fs.Seek(-1, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
            }

            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.Append))
            {
                await JsonSerializer.SerializeAsync(fs, user);
            }

            string text2 = "]";
            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text2);
                fs.Seek(0, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
            }
        }
        //add this with TASK<string>?  //Console.WriteLine($"New object with {user.FirstName}, {user.LastName}, {user.Town}, {user.Email} has been added tot he collection.");
        //using (StreamWriter streamWriter = new StreamWriter("StaticFiles/Users.json", true));            

        public static async Task AddAddressesAsync(AddressModel address)
        {
            var addresses = await GetAddressesAsync();
            addresses.Add(address);

            string text = ",";
            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text);
                fs.Seek(-1, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
            }

            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.Append))
            {
                await JsonSerializer.SerializeAsync(fs, address);
            }

            string text2 = "]";
            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text2);
                fs.Seek(0, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
            }
        }

        public static async Task DeleteUsersAsync(int index)
        {
            var users = await GetUsersAsync();
            users.RemoveAt(index);

            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync(fs, users);
            }
        }
    }
}
////kak verno vernut'massiv' failov?
//string [] userFile = await File.ReadAllLinesAsync("StaticFiles/Users.json");
//var users = JsonSerializer.Deserialize<UserModel[]>(userFile);
