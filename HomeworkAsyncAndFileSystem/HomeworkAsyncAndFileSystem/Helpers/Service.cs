using HomeworkAsyncAndFileSystem.Models;
using System.Text;
using System.Text.Json;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public class Service
    {
        public async static Task<List<DriverModel>> GetDrivers()
        {
            var fileResult = await File.ReadAllTextAsync("StaticFiles/owners.json");
            var owners = JsonSerializer.Deserialize<List<DriverModel>>(fileResult);
            return owners;
        }

        public async static Task<List<CarModel>> GetCars()
        {
            var fileResult = await File.ReadAllTextAsync("StaticFiles/cars.json");
            var cars = JsonSerializer.Deserialize<List<CarModel>>(fileResult);
            return cars;
        }

        public async static Task<List<CarDriverModel>> GetCarDrivers()
        {
            var drivers = await GetDrivers();
            var cars = await GetCars();
            var CarDrivers = drivers.Join(cars, q => q.CarNumber, w => w.Number, (q, w) => new CarDriverModel
            {
                CarNumber = w.Number,
                CarName = w.Name,
                CarModel = w.Model,                              
                DriverFirstName = q.FirstName,
                DriverLastName = q.LastName,              

            }).ToList();
            return CarDrivers;
        }

        public async static Task AddDriver(DriverModel driver)
        {
            var pathToWrite = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles/Owners.json");
            var writeStream = File.Open(pathToWrite, FileMode.OpenOrCreate);
            var driverRecord = "," + JsonSerializer.Serialize(driver) + "\n]";
            var writeBuffer = Encoding.Default.GetBytes(driverRecord);
            writeStream.Seek(-2, SeekOrigin.End);
            await writeStream.WriteAsync(writeBuffer);
            writeStream.Close();
        }

        public async static Task AddCar(CarModel car)
        {
            var pathToWrite = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles/Cars.json");
            var writeStream = File.Open(pathToWrite, FileMode.OpenOrCreate);
            var carRecord = "," + JsonSerializer.Serialize(car) + "\n]";
            var writeBuffer = Encoding.Default.GetBytes(carRecord);
            writeStream.Seek(-2, SeekOrigin.End);
            await writeStream.WriteAsync(writeBuffer);
            writeStream.Close();
        }      

    }
}
