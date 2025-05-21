using Encapsulation.DB.DataSchemas;
using System.Text.Json;

namespace Encapsulation.DB.Data
{
    internal  class RoomsDBManager
    {

        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Rooms.json";
        private  JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        public  async Task CreateDB()
        {
            // Check if the file exists
            if (!File.Exists(relativeDBPath))
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fs, new List<Room>(), options);
                    await fs.DisposeAsync();
                }
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {
            using (FileStream fs = new(relativeDBPath, FileMode.Open))
            {
                List<Room> rooms = await JsonSerializer.DeserializeAsync<List<Room>>(fs, options);
                await fs.DisposeAsync();
                return rooms;
            }
        }

        public async Task<List<Room>> GetAllRoomsByRoomType(string roomType)
        {
            using (FileStream fs = new(relativeDBPath, FileMode.Open))
            {
                List<Room> rooms = await JsonSerializer.DeserializeAsync<List<Room>>(fs, options);
                List<Room> filteredRooms = rooms.FindAll(r => Enum.GetName(typeof(RoomType), r.RoomType) == roomType);
                await fs.DisposeAsync();
                return filteredRooms;
            }
        }

        public async Task AddRoom(Room room)
        {
            List<Room> rooms = GetAllRooms().Result;
            rooms.Add(room);
            using (FileStream fw = new(relativeDBPath, FileMode.Open))
            {
                await JsonSerializer.SerializeAsync(fw, rooms, options);
                await fw.DisposeAsync();
            }
        }

        public async Task ReserveRoom(string roomType, DateTime startDate, DateTime endDate)
        {
            List<Room> rooms = GetAllRoomsByRoomType(roomType).Result;
            if (rooms != null)
            {
                rooms[0].ReservedDates.Add(GetInterval(startDate, endDate));
            }
            else
            {
                Console.WriteLine("Room not found");
            }
        }

        private static Tuple<DateTime, DateTime> GetInterval(DateTime startDate, DateTime endDate)
        {
            return new Tuple<DateTime, DateTime>(startDate, endDate);
        }
    }
}
