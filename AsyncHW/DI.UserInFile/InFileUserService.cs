using DI.Entities.Entities;
using DI.Exceptions;
using DI.UserInFile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DI.UserInFile
{
    public sealed class InFileUserService : IUserService
    {
        private readonly IFileManager _fileManager;
        private readonly IFileSystemPathProvider _fileSystemPathProvider;

        public InFileUserService(IFileManager fileManager, IFileSystemPathProvider fileSystemPathProvider)
        {
            _fileManager = fileManager;
            _fileSystemPathProvider = fileSystemPathProvider;
        }

        #region IUsersService Members
        public async Task<IReadOnlyCollection<User>> GetUsers()
        {
            using var reader = File.OpenRead(_fileSystemPathProvider.GetUsersPath());
            return await JsonSerializer.DeserializeAsync<List<User>>(reader);
        }

        public async Task<IReadOnlyCollection<Address>> GetAddresses()
        {
            using var reader = File.OpenRead(_fileSystemPathProvider.GetAddressesPath());
            return await JsonSerializer.DeserializeAsync<List<Address>>(reader);
        }

        public async Task<User> AddUser(Guid userId, string firstName, string lastName, string email, DateTime birthDate, Guid? addressId)
        {
            var users = GetUsers().GetAwaiter().GetResult().ToList();
            if (users.Any(x => x.UserId == userId))
            {
                throw new ObjectExistException("User");
            }

            var newUser = new User
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                BirthDate = birthDate,
                AddressId = addressId
            };

            users.Add(newUser);

            await WriteUsers(users);

            return newUser;
        }

        public async Task<Address> AddAddress(Guid addressId, string city, string street, string houseNumber)
        {
            var addresses = GetAddresses().GetAwaiter().GetResult().ToList();
            if (addresses.Any(x => x.AddressId == addressId))
            {
                throw new ObjectExistException("Address");
            }

            var newAddress = new Address
            {
                AddressId = addressId,
                City = city,
                Street = street,
                HouseNumber = houseNumber
            };

            addresses.Add(newAddress);

            await WriteAddresses(addresses);

            return newAddress;
        }

        public async Task DeleteUser(Guid userId)
        {
            var users = GetUsers().GetAwaiter().GetResult().ToList();
            var user = users.SingleOrDefault(x => x.UserId == userId);
            if (user is null)
            {
                throw new ObjectNotFountException("User");
            }

            users.Remove(user);

            await WriteUsers(users);
        }

        public async Task DeleteAddress(Guid addressId)
        {
            var addresses = GetAddresses().GetAwaiter().GetResult().ToList();
            var address = addresses.SingleOrDefault(x => x.AddressId == addressId);
            if (address is null)
            {
                throw new ObjectNotFountException("Address");
            }

            addresses.Remove(address);

            await WriteAddresses(addresses);
        }
        #endregion

        private Task WriteUsers(IEnumerable<User> users)
        {
            return _fileManager.Write(_fileSystemPathProvider.GetUsersPath(),
                                      JsonSerializer.Serialize(users,
                                                               new JsonSerializerOptions
                                                               {
                                                                   WriteIndented = true,
                                                               }));
        } 
        
        private Task WriteAddresses(IEnumerable<Address> addresses)
        {
            return _fileManager.Write(_fileSystemPathProvider.GetAddressesPath(),
                                      JsonSerializer.Serialize(addresses,
                                                               new JsonSerializerOptions
                                                               {
                                                                   WriteIndented = true,
                                                               }));
        }
    }
}
