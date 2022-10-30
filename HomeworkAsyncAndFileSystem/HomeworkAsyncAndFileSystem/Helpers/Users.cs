using Bogus;
using HomeworkAsyncAndFileSystem.Enums;
using HomeworkAsyncAndFileSystem.Models;

namespace HomeworkAsyncAndFileSystem.Helpers
{
    public static class Users
    {
        public static List<UserViewModel> GetList()
        {
            var user = new Faker<UserViewModel>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Sex, f => f.PickRandom<Sex>())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName((Bogus.DataSets.Name.Gender)u.Sex))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName((Bogus.DataSets.Name.Gender)u.Sex))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

            return user.Generate(100);
        }
    }
}
