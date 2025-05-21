using Encapsulation.DB.Data;
using Encapsulation.DB.DataSchemas;

namespace Encapsulation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RoomsDBManager roomsDBManager = new RoomsDBManager();
            UserDBManager userDBManager = new UserDBManager();
            //User curentUser = null;
            //while (true)
            //{
            //    ConsoleManager.WelcomeMessage(curentUser);
            //    if (curentUser == null)
            //    {
            //        ;
            //        int choosenOption = ConsoleManager.NotLogetUser();
            //        if (choosenOption == 1)
            //        {
            //            curentUser = userDBManager.AddUser(ConsoleManager.AddUser()).Result;
            //            if (curentUser != null)
            //            {
            //                Console.WriteLine($"Welcome {curentUser.UserName}");
            //            }
            //        }
            //        else if (choosenOption == 2)
            //        {
            //            curentUser = userDBManager.LogIn(ConsoleManager.LogIn()).Result;
            //            if (curentUser != null)
            //            {
            //                Console.WriteLine($"Welcome {curentUser.UserName}");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (curentUser.Role == "user")
            //        {
            //            int choosenOption = ConsoleManager.LoggedUser();
            //            if (choosenOption == 1)
            //            {
            //                List<Room> rooms = roomsDBManager.GetAllRooms().Result;
            //                ConsoleManager.ShowAllRooms(rooms);
            //            }
            //            else if (choosenOption == 2)
            //            {
            //                curentUser = null;
            //                ConsoleManager.LogOut();
            //            }
            //            else if (choosenOption == 3)
            //            {
            //                int roomType = ConsoleManager.RoomType();
            //                List<Room> rooms = roomsDBManager.GetAllRoomsByRoomType(Enum.GetName(typeof(RoomType), roomType)).Result;
            //                Tuple<DateTime, DateTime> dates = ConsoleManager.ReservedDates();
                            
            //            }
            //        }
            //        else if (curentUser.Role == "admin")
            //        {
            //            int choosenOption = ConsoleManager.LoggedAdmin();
            //            if (choosenOption == 1)
            //            {
            //                List<Room> rooms = roomsDBManager.GetAllRooms().Result;
            //                ConsoleManager.ShowAllRooms(rooms);
            //            }
            //            else if (choosenOption == 2)
            //            {
            //                curentUser = null;
            //                ConsoleManager.LogOut();
            //            }
            //        }

            //    }
                await Task.Run(roomsDBManager.CreateDB);
                await roomsDBManager.AddRoom(new Room(101, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(102, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(103, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(104, RoomType.Deluxe));
                await roomsDBManager.AddRoom(new Room(201, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(202, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(203, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(204, RoomType.Deluxe));
                //Thread.Sleep(10);
            //}
        }
    }
}
