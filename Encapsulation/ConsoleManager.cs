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
                Console.WriteLine();
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
            }
        }
    }
}
