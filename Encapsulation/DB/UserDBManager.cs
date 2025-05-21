using Encapsulation.DB.DataSchemas;
using System.Reflection.Metadata;
using System.Text.Json;


namespace Encapsulation.DB
{
    internal class UserDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Users.json";

        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private List<User>? users;
        public async void CreateDB()
        {
            if (!File.Exists(relativeDBPath))
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Create))
                {
                    Thread.Sleep(10);
                    await JsonSerializer.SerializeAsync(fs, new List<User>(), options);
                    await fs.DisposeAsync();
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
                    await fs.DisposeAsync();
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
                using (FileStream fw = new(relativeDBPath, FileMode.Open))
                {
                    Thread.Sleep(10);
                    await JsonSerializer.SerializeAsync(fw, users, options);
                    await fw.DisposeAsync();
                    Console.WriteLine("User added successfully");
                    return user;
                }
            }
            else
            {
                Console.WriteLine("User already exists");
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
                    Console.WriteLine("Invalid password");
                    return null;
                }
                Console.WriteLine("User logged in successfully");
                return existingUser;
            }
            else
            {
                Console.WriteLine("Invalid username or password");
                return null;
            }
        }
    }
}
