using Encapsulation.DB.Data;
using Encapsulation.DB.DataSchemas;


namespace Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoomsManager roomManager = new RoomsManager();
            roomManager.CreateDB();
            roomManager.AddRoom(new Room(101, RoomType.Single));
            roomManager.AddRoom(new Room(102, RoomType.Double));
            Thread.Sleep(10);
            List<Room> rooms = roomManager.GetAllRooms().Result;
            foreach (var item in rooms)
            {
                Console.WriteLine(item.NumberOfOccupants);
            }
        }
    }
}
