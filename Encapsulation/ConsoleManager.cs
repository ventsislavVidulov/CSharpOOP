using Encapsulation.DB.DataSchemas;

namespace Encapsulation
{
    internal static class ConsoleManager
    {
        public static void ShowAllRooms(List<Room> rooms)
        {
            HashSet<int> floors = new HashSet<int>();
            foreach (var item in rooms)
            {
                int floor = item.RoomNumber / 100;
                floors.Add(floor);
            }
            foreach (var floor in floors)
            {
                int curentFloor = floor;
                foreach (var room in rooms)
                {
                    if (room.RoomNumber / 100 == curentFloor)
                    {
                        if (room.RoomStatus == RoomStatus.Available)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (room.RoomStatus == RoomStatus.Booked)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        string roomType = Enum.GetName(typeof(RoomType), room.RoomType) == "Suite" ? "Suite " : Enum.GetName(typeof(RoomType), room.RoomType);
                        Console.Write($"|{room.RoomNumber} {roomType} {room.NumberOfOccupants}/{room.NumberOfBeds}|");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        public static User AddUser()
        {
            Console.WriteLine("Enter user name");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            Console.WriteLine("Confirm password");
            string confirmPassword = Console.ReadLine();
            if (password != confirmPassword)
            {
                Console.WriteLine("Password and confirm password do not match");
                Console.WriteLine("Enter password");
                password = Console.ReadLine();
                Console.WriteLine("Confirm password");
                confirmPassword = Console.ReadLine();
            }
            return new User(userName, password);
        }

        public static User LogIn()
        {
            Console.WriteLine("Enter user name");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();
            return new User(userName, password);
        }

        public static void LogOut()
        {
            Console.WriteLine("Logged out");
        }

        public static void WelcomeMessage(User user)
        {
            Console.WriteLine("Welcome to the Hotel Management System");
            if(user != null)
            {
            Console.WriteLine($"Curent user: {user?.UserName}, role: {user?.Role}");
            }
    
        }

        public static int NotLogetUser()
        {
            Console.WriteLine("No user logged in");
            Console.WriteLine("1 -> Create new user");
            Console.WriteLine("2 -> Log in");
            return Int32.Parse(Console.ReadLine());
        }

        public static int LoggedAdmin() 
        {
            Console.WriteLine("1 -> Show all rooms");
            Console.WriteLine("2 -> Log out");
            return Int32.Parse(Console.ReadLine());
        }

        public static int LoggedUser()
        {
            Console.WriteLine("1 -> Show all rooms");
            Console.WriteLine("2 -> Log out");
            Console.WriteLine("3 -> Reserve a room");
            return Int32.Parse(Console.ReadLine());
        }

        public static int RoomType() {
            Console.WriteLine("Choose room type");
            Console.WriteLine("1 -> Single");
            Console.WriteLine("2 -> Double");
            Console.WriteLine("3 -> Suite");
            Console.WriteLine("4 -> Deluxe");
            return Int32.Parse(Console.ReadLine());
        }

        public static Tuple<DateTime, DateTime> ReservedDates()
        {
            Console.WriteLine("Enter start date (yyyy-mm-dd)");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter end date (yyyy-mm-dd)");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
