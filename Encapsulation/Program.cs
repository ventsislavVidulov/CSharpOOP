using Encapsulation.DB.Data;
using Encapsulation.DB.DataSchemas;

namespace Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoomsDBManager roomsManager = new RoomsDBManager();
            UserDBManager userManager = new UserDBManager();
            User curentUser = null;
            while (true)
            {
                Console.WriteLine(curentUser?.Role);
                ConsoleManager.WelcomeMessage(curentUser);
                if (curentUser == null)
                {
                    ;
                    int choosenOption = ConsoleManager.NotLogetUser();
                    if (choosenOption == 1)
                    {
                        curentUser = userManager.AddUser(ConsoleManager.AddUser()).Result;
                        if (curentUser != null)
                        {
                            Console.WriteLine($"Welcome {curentUser.UserName}");
                        }
                    }
                    else if (choosenOption == 2)
                    {
                        curentUser = userManager.LogIn(ConsoleManager.LogIn()).Result;
                        if (curentUser != null)
                        {
                            Console.WriteLine($"Welcome {curentUser.UserName}");
                        }
                    }
                }
                else
                {
                    if (curentUser.Role == "user")
                    {
                        int choosenOption = ConsoleManager.LoggedUser();
                        if (choosenOption == 1)
                        {
                            List<Room> rooms = roomsManager.GetAllRooms().Result;
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }

                    }
                    else if (curentUser.Role == "admin")
                    {
                        int choosenOption = ConsoleManager.LoggedAdmin();
                        if (choosenOption == 1)
                        {
                            List<Room> rooms = roomsManager.GetAllRooms().Result;
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }
                    }

                }
                //roomManager.CreateDB();
                //roomManager.AddRoom(new Room(101, RoomType.Single));
                //roomManager.AddRoom(new Room(102, RoomType.Double));
                //roomManager.AddRoom(new Room(103, RoomType.Suite));
                //roomManager.AddRoom(new Room(104, RoomType.Deluxe));
                //roomManager.AddRoom(new Room(201, RoomType.Single));
                //roomManager.AddRoom(new Room(202, RoomType.Double));
                //roomManager.AddRoom(new Room(203, RoomType.Suite));
                //roomManager.AddRoom(new Room(204, RoomType.Deluxe));
                //Thread.Sleep(10);
            }
        }
    }
}
