using Encapsulation.DB.DataSchemas;
using Encapsulation.Interfaces;
using System.Text.Json;

namespace Encapsulation.DB
{
    internal class RoomsDBManager : IRoomDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Rooms.json";

        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private List<Room>? rooms;
        
        public async Task CreateDB()
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

        public async Task<List<Room>?> GetAllRooms()
        {
            if (rooms == null)
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Open))
                {
                    rooms = await JsonSerializer.DeserializeAsync<List<Room>>(fs, options);
                    await fs.DisposeAsync();
                }
            }
            return rooms;
        }

        public async Task AddRoom(Room room)
        {
            List<Room> rooms = await GetAllRooms();
            rooms.Add(room);
            using (FileStream fw = new(relativeDBPath, FileMode.Open))
            {
                await JsonSerializer.SerializeAsync(fw, rooms, options);
                await fw.DisposeAsync();
            }
        }

        public async Task UpdateRoom(Room roomToUpdate)
        {
            List<Room> rooms = await GetAllRooms();
            int roomToUpdateIndex = rooms.FindIndex(r => r.RoomNumber == roomToUpdate.RoomNumber);
            if (roomToUpdateIndex != -1)
            {
                rooms[roomToUpdateIndex] = roomToUpdate;
                using (FileStream fw = new(relativeDBPath, FileMode.Open))
                {
                    await JsonSerializer.SerializeAsync(fw, rooms, options);
                    await fw.DisposeAsync();
                }
            }
            else
            {
                Console.WriteLine("Room not found");
            }
            ConsoleManager.ShowAllRooms(await GetAllRooms());
        }

        public Task DeleteRoom(Room roomToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
