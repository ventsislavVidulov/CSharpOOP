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
        public async Task<List<User>?> GetAllUsers()
        {
            return await dbManager.GetAllUsers();
        }

        public async Task<User?> AddUser(User user)
        {
            return await dbManager.AddUser(user);
        }

        public async Task<User?> LogIn(string userName, string password)
        {
            User? existingUser = await dbManager.GetUser(userName);
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


        public async Task AddReservation(User loggedInUser, int reservationId)
        {
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
