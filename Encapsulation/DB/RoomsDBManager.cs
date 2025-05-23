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
                    //await fs.DisposeAsync();
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
                    //await fs.DisposeAsync();
                }
            }
            return rooms;
        }

        public async Task AddRoom(Room room)
        {
            List<Room> rooms = await GetAllRooms();
            rooms.Add(room);
            using (FileStream fw = new(relativeDBPath, FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fw, rooms, options);
                //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                rooms = null;
                //await fw.DisposeAsync();
            }
        }

        public async Task UpdateRoom(Room roomToUpdate)
        {
            List<Room> rooms = await GetAllRooms();
            int roomToUpdateIndex = rooms.FindIndex(r => r.RoomNumber == roomToUpdate.RoomNumber);
            if (roomToUpdateIndex != -1)
            {
                rooms[roomToUpdateIndex] = roomToUpdate;
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, rooms, options);
                    //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                    rooms = null;
                    //await fw.DisposeAsync();
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Room not found");
                Console.ResetColor();
            }
        }

        public async Task DeleteRoom(Room roomToRemove)
        {
            //TODO: check if the room has any reservations and resolve it before remove
            List<Room> rooms = await GetAllRooms();
            int roomToRemoveIndex = rooms.FindIndex(r => r.RoomNumber == roomToRemove.RoomNumber);

            if (roomToRemoveIndex != -1)
            {
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    rooms.RemoveAll(r => r.RoomNumber == roomToRemove.RoomNumber);
                    await JsonSerializer.SerializeAsync(fw, rooms, options);
                    //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                    rooms = null;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Room not found");
                Console.ResetColor();
            }
        }
    }
}
