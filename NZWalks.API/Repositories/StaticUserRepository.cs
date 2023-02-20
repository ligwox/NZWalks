using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>() {
        //new User() {
        //    FirstName = "First",
        //    LastName = "Last",
        //    Username = "Bob",
        //    Email = "bob@mail",
        //    Id = new Guid(),
        //    Password = "123",
        //    Roles = new List<string> { "reader" }
        //},
        //new User() {
        //    FirstName = "First",
        //    LastName = "Last",
        //    Username = "Max",
        //    Email = "max@mail",
        //    Id = new Guid(),
        //    Password = "333",
        //    Roles = new List<string> { "writer", "reader" }
        //}
        };
        public async Task<User> Authenticate(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) 
            && x.Password.Equals(password));

            return user;
        }
    }
}
