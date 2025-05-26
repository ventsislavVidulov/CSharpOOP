using Encapsulation.DB;
using Encapsulation.DB.DataSchemas;
using System.Text.Json;

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

        //public async Task<User?> LogIn(User user)
        //{
        //    return await dbManager.LogIn(user);
        //}

        //public async Task AddReservation(User user, int reservationId)
        //{
        //    await dbManager.AddReservation(user, reservationId);
        //}

        //TODO move this method to controller layer
        public async Task<User?> LogIn(string userName, string password)
        {
            User existingUser = await dbManager.GetUser(userName);
            if (existingUser != null)
            {
                if (existingUser.Password != password)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid username or password");
                    Console.ResetColor();
                    return null;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{existingUser.UserName} logged in successfully");
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

        //TODO move this method to controller layer
        public async Task AddReservation(User loggedInUser, int reservationId)
        {
            //List<User> users = await GetAllUsers();
            //int userId = users.FindIndex(savedUser => savedUser.UserName == user.UserName);
            //if (userId != -1)
            //{
            //    users[userId].Reservations.Add(reservationId);
            //    try
            //    {
            //        using (FileStream fw = new(relativeDBPath, FileMode.Create))
            //        {
            //            await JsonSerializer.SerializeAsync(fw, users, options);
            //            Console.WriteLine($"User reservations: {GetUser(user).Result.Reservations.Count}");
            //            Console.ForegroundColor = ConsoleColor.Green;
            //            Console.WriteLine($"{user.UserName} updated successfully");
            //            Console.ResetColor();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine($"Error updating user: {ex.Message}");
            //        Console.ResetColor();
            //    }
            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("User not found");
            //    Console.ResetColor();
            //}

            if (loggedInUser != null)
            {
                loggedInUser.Reservations.Add(reservationId);
                await dbManager.UpdateUser(loggedInUser);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{loggedInUser.UserName} updated successfully");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No user logged in");
                Console.ResetColor();
            }
        }
    }
}
