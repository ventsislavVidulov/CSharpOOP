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
            string userName = "";
            string password = "";
            string confirmPassword = "";
            while (userName == "")
            {
                Console.WriteLine("Enter user name");
                userName = Console.ReadLine();
            }
            while (password == "")
            {
                Console.WriteLine("Enter password");
                password = Console.ReadLine();
            }
            while (confirmPassword == "")
            {
                Console.WriteLine("Confirm password");
                confirmPassword = Console.ReadLine();
            }
            if (password != confirmPassword)
            {
                while (password == "")
                {
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine();
                }
                while (confirmPassword == "")
                {
                    Console.WriteLine("Confirm password");
                    confirmPassword = Console.ReadLine();
                }
            }
            return new User(userName, password);
        }

        public static void LogIn(out string userName, out string password)
        {
            userName = "";
            password = "";
            while (userName == "")
            {
                Console.WriteLine("Enter user name");
                userName = Console.ReadLine();
            }
            while (password == "")
            {
                Console.WriteLine("Enter password");
                password = Console.ReadLine();
            }
        }

        public static void LogOut()
        {
            Console.WriteLine("Logged out");
        }

        public static void WelcomeMessage(User? user)
        {
            if (user != null)
            {
                Console.WriteLine($"Curent user: {user?.UserName}, role: {user?.Role}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome to the Hotel Management System");
                Console.ResetColor();
            }

        }

        public static int NotLogetUser()
        {
            Console.WriteLine("No user logged in");
            Console.WriteLine("1 -> Create new user");
            Console.WriteLine("2 -> Log in");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }

        public static int LoggedAdmin()
        {
            Console.WriteLine("1 -> Show all rooms");
            Console.WriteLine("2 -> Log out");
            //Console.WriteLine("3 -> Add new room");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }

        public static int LoggedUser()
        {
            Console.WriteLine("1 -> Show all rooms");
            Console.WriteLine("2 -> Log out");
            Console.WriteLine("3 -> Reserve a room");
            Console.WriteLine("4 -> Change the date to see the aviability of rooms for that date");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }

        public static int RoomType()
        {
            Console.WriteLine("Choose room type");
            Console.WriteLine("1 -> Single");
            Console.WriteLine("2 -> Double");
            Console.WriteLine("3 -> Suite");
            Console.WriteLine("4 -> Deluxe");
            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }

        public static Tuple<DateTime, DateTime> ReservedDates()
        {
            Console.WriteLine("Enter start date (yyyy-mm-dd)");
            DateTime.TryParse(Console.ReadLine(), out DateTime startDate);
            Console.WriteLine("Enter end date (yyyy-mm-dd)");
            DateTime.TryParse(Console.ReadLine(), out DateTime endDate);
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }

        public static DateTime ChangeDate()
        {
            Console.WriteLine("Enter date to check room aviability (yyyy-mm-dd)");
            DateTime.TryParse(Console.ReadLine(), out DateTime date);
            return date;
        }
    }
}
