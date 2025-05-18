using Encapsulation.DB.Data;
using Encapsulation.DB.DataSchemas;


namespace Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoomsManager roomManager = new RoomsManager();
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
            List<Room> rooms = roomManager.GetAllRooms().Result;
            ConsoleManager.ShowAllRooms(rooms);
        }
    }
}
