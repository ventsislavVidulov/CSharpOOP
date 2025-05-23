using Encapsulation.DB.DataSchemas;
using Encapsulation.Interfaces;
using System.Text.Json;


namespace Encapsulation.DB
{
    internal class UserDBManager : IUserDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Users.json";

        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private List<User>? users;

        //public UserDBManager()
        //{
        //    Task.Run(CreateDB);
        //}
        public async Task CreateDB()
        {
            if (!File.Exists(relativeDBPath))
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, new List<User>(), options);
                    //await fs.DisposeAsync();
                }
            }
        }

        public async Task<List<User>?> GetAllUsers()
        {
            if (users == null)
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Open))
                {
                    users = await JsonSerializer.DeserializeAsync<List<User>>(fs, options);
                    //await fs.DisposeAsync();
                }
            }
            return users;
        }

        public async Task<User?> AddUser(User user)
        {
            List<User> users = await GetAllUsers();
            User existingUser = users.Find(users => users.UserName == user.UserName);
            if (existingUser == null)
            {
                users.Add(user);
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, users, options);
                    //await fw.DisposeAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("User added successfully");
                    Console.ResetColor();
                    return user;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User already exists");
                Console.ResetColor();
                return null;
            }
        }

        public async Task<User?> LogIn(User user)
        {
            List<User> users = await GetAllUsers();
            User existingUser = users.Find(eu => eu.UserName == user.UserName);
            if (existingUser != null)
            {
                if (existingUser.Password != user.Password)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid password");
                    Console.ResetColor();
                    return null;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User logged in successfully");
                Console.ResetColor();
                return existingUser;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid username or password");
                Console.ResetColor();
                return null;
            }
        }
    }
}
