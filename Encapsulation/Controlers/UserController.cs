using Encapsulation.DB;
using Encapsulation.DB.DataSchemas;

namespace Encapsulation.Controlers
{
    internal class UserController
    {
        UserDBManager dbManager;
        public UserController(UserDBManager dBManager)
        {
            this.dbManager = dBManager;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await dbManager.GetAllUsers();
        }

        public async Task<User?> AddUser(User user)
        {
            return await dbManager.AddUser(user);
        }

        public async Task<User?> LogIn(User user)
        {
            return await dbManager.LogIn(user);
        }
    }
}
