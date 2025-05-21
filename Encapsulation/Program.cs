using Encapsulation.Controlers;
using Encapsulation.DB;
using Encapsulation.DB.DataSchemas;

namespace Encapsulation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RoomsDBManager roomsDBManager = new RoomsDBManager();
            await Task.Run(roomsDBManager.CreateDB);
            RoomController roomController = new RoomController(roomsDBManager);
            UserDBManager userDBManager = new UserDBManager();
            List<Room> startUpRooms = await roomsDBManager.GetAllRooms();
            if (startUpRooms.Count == 0)
            {
                await roomsDBManager.AddRoom(new Room(101, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(102, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(103, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(104, RoomType.Deluxe));
                await roomsDBManager.AddRoom(new Room(201, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(202, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(203, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(204, RoomType.Deluxe));
            }
            User curentUser = null;
            while (true)
            {
                ConsoleManager.WelcomeMessage(curentUser);
                if (curentUser == null)
                {
                    ;
                    int choosenOption = ConsoleManager.NotLogetUser();
                    if (choosenOption == 1)
                    {
                        curentUser = await userDBManager.AddUser(ConsoleManager.AddUser());
                        if (curentUser != null)
                        {
                            Console.WriteLine($"Welcome {curentUser.UserName}");
                        }
                    }
                    else if (choosenOption == 2)
                    {
                        curentUser = await userDBManager.LogIn(ConsoleManager.LogIn());
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
                            List<Room> rooms = await roomController.GetAllRooms();
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }
                        else if (choosenOption == 3)
                        {
                            int roomType = ConsoleManager.RoomType();
                            List<Room> rooms = await roomController.GetAllRoomsByRoomType(Enum.GetName(typeof(RoomType), roomType));
                            ReservationInterval dates = ConsoleManager.ReservedDates();
                            await roomController.ReserveRoom(Enum.GetName(typeof(RoomType), roomType), dates);
                        }
                    }
                    else if (curentUser.Role == "admin")
                    {
                        int choosenOption = ConsoleManager.LoggedAdmin();
                        if (choosenOption == 1)
                        {
                            List<Room> rooms = await roomController.GetAllRooms();
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }
                    }

                }
            }
        }
    }
}
