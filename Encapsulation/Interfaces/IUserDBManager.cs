using Encapsulation.DB.DataSchemas;

namespace Encapsulation.Interfaces
{
    internal interface IUserDBManager
    {
        public Task CreateDB();

        public Task<List<User>?> GetAllUsers();

        public Task<User?> AddUser(User user);

        public Task<User?> LogIn(User user);
    }
}
