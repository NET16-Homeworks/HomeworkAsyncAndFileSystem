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
            var usersCity = new List<UsersCityModel>();

            var joined = users.Join(addresses, u => u.Town, a => a.City,
                (u, a) => new { User = u, Address = a });

            foreach (var item in joined)
            {
                UsersCityModel usm = new UsersCityModel();
                usm.FirstName = item.User.FirstName;
                usm.LastName = item.User.LastName;
                usm.City = item.Address.City;
                usersCity.Add(usm);
            }
            return usersCity;
        }

        //Реализовать возможность ввода данных для этих сущностей с помощью форм и добавление их в файл.
        
        public static async void AddUsersAsync(UserModel user)
        {
            var users = await GetUsersAsync();

            users.Add(user);
            string text = ",";
            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text);
                fs.Seek(-1, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
                fs.Close();
            }

            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.Append))
            {
                await JsonSerializer.SerializeAsync(fs, user);
                fs.Close();
            }

            string text2 = "]";
            using (FileStream fs = new FileStream("StaticFiles/Users.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text2);
                fs.Seek(0, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
                fs.Close();
            }
        }
        //add this with TASK<string>?  //Console.WriteLine($"New object with {user.FirstName}, {user.LastName}, {user.Town}, {user.Email} has been added tot he collection.");
        //using (StreamWriter streamWriter = new StreamWriter("StaticFiles/Users.json", true));            

        public static async void AddAddressesAsync(AddressModel address)
        {
            var addresses = await GetAddressesAsync();
            addresses.Add(address);

            string text = ",";
            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text);
                fs.Seek(-1, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
                fs.Close();
            }

            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.Append))
            {
                await JsonSerializer.SerializeAsync(fs, address);
                fs.Close();
            }

            string text2 = "]";
            using (FileStream fs = new FileStream("StaticFiles/Address.json", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(text2);
                fs.Seek(0, SeekOrigin.End);
                await fs.WriteAsync(input, 0, input.Length);
                fs.Close();
            }
        }

        public static async void DeleteUsersAsync(int index)
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
