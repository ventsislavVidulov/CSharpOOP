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
            // Check if the DB file exists
            if (!File.Exists(relativeDBPath))
            {
                try
                {
                    using (FileStream fs = new(relativeDBPath, FileMode.Create))
                    {
                        await JsonSerializer.SerializeAsync(fs, new List<User>(), options);
                        //await fs.DisposeAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error creating DB: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public async Task<List<User>?> GetAllUsers()
        {
            if (users == null)
            {
                try
                {
                    using (FileStream fs = new(relativeDBPath, FileMode.Open))
                    {
                        users = await JsonSerializer.DeserializeAsync<List<User>>(fs, options);
                        //await fs.DisposeAsync();
                    }
                }
                catch (JsonException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    Console.ResetColor();
                    users = null;
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

                try
                {
                    using (FileStream fw = new(relativeDBPath, FileMode.Create))
                    {
                        await JsonSerializer.SerializeAsync(fw, users, options);
                        //await fw.DisposeAsync();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{user.UserName} added successfully");
                        Console.ResetColor();
                        return user;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error adding user: {ex.Message}");
                    Console.ResetColor();
                    return null;
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
                    Console.WriteLine("Invalid username or password");
                    Console.ResetColor();
                    return null;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{user.UserName} logged in successfully");
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