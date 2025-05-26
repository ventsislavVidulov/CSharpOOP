using Encapsulation.DB.DataSchemas;
using Encapsulation.Interfaces;
using System.ComponentModel;
using System.Text.Json;


namespace Encapsulation.DB
{
    internal class UserDBManager : IUserDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Users.json";

        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private List<User>? cachedUsers;
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
            if (cachedUsers == null)
            {
                try
                {
                    using (FileStream fs = new(relativeDBPath, FileMode.Open))
                    {
                        cachedUsers = await JsonSerializer.DeserializeAsync<List<User>>(fs, options);
                    }
                }
                catch (JsonException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    Console.ResetColor();
                    cachedUsers = null;
                }
            }
            return cachedUsers;
        }
        public async Task<User?> GetUser(string userName)
        {
            List<User> users = await GetAllUsers();
            User existingUser = users.Find(eu => eu.UserName == userName);
            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found");
                Console.ResetColor();
                return null;
            }
        }

        public async Task<User?> AddUser(User userToBeAdded)
        {
            List<User> usersFromDB = await GetAllUsers();
            User existingUserInDB = usersFromDB.Find(ufDB => ufDB.UserName == userToBeAdded.UserName);
            //early exit if user exists
            if (existingUserInDB != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User already exists");
                Console.ResetColor();
                return null;
            }

            usersFromDB.Add(userToBeAdded);
            try
            {
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, usersFromDB, options);
                    //await fw.DisposeAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{userToBeAdded.UserName} added successfully");
                    Console.ResetColor();
                    return userToBeAdded;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error adding user: {ex.Message}");
                Console.ResetColor();
                return null;
            }
            finally
            {
                //reset the cached users so the next GetAllUsers() call returns the actual users

                //the cacheing may be optimized if we say "cachedUsers = usersFromDB;" this will omit one reading from DB in the next GetAllUsers() call,
                //but it rises concern for "one sorce of truth" policy
                cachedUsers = null;
            }

        }

        public async Task<User?> UpdateUser(User userToBeUpdated)
        {
            List<User> usersFromDB = await GetAllUsers();
            int userId = usersFromDB.FindIndex(savedUser => savedUser.UserName == userToBeUpdated.UserName);
            //early exit if user is not found
            if (userId == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User not found");
                Console.ResetColor();
            }
            try
            {
                usersFromDB[userId] = userToBeUpdated;
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, usersFromDB, options);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{userToBeUpdated.UserName} updated successfully");
                    Console.ResetColor();
                    return userToBeUpdated;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error updating user: {ex.Message}");
                Console.ResetColor();
                return null;
            }
            finally { cachedUsers = null; }
        }
    }
}