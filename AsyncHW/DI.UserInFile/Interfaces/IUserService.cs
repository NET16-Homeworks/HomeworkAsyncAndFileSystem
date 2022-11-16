using DI.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.UserInFile.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyCollection<User>> GetUsers();
        Task<IReadOnlyCollection<Address>> GetAddresses();
        Task<User> AddUser(Guid userId, string firstName, string lastName, string email, DateTime birthDate, Guid? addressId);
        Task<Address> AddAddress(Guid addressId, string City, string Street, string houseNumber);
        Task DeleteUser(Guid userId);
        Task DeleteAddress(Guid addressId);
    }
}
