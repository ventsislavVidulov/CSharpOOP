using Encapsulation.DB.DataSchemas;
using System.Text.Json;

namespace Encapsulation.DB.Data
{
    internal  class RoomsDBManager
    {
        private string relativeDBPath = @"../../../../Encapsulation/DB/Data/Rooms.json";
        private  JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        public  async void CreateDB()
        {
            // Check if the file exists
            if (!File.Exists(relativeDBPath))
            {
                using (FileStream fs = new(relativeDBPath, FileMode.Create))
                {
                    Thread.Sleep(10);
                    await JsonSerializer.SerializeAsync(fs, new List<Room>(), options);
                    await fs.DisposeAsync();
                }
            }
        }

        public async Task<List<Room>> GetAllRooms()
        {
            using (FileStream fs = new(relativeDBPath, FileMode.Open))
            {
                Thread.Sleep(10);
                List<Room> rooms = await JsonSerializer.DeserializeAsync<List<Room>>(fs, options);
                await fs.DisposeAsync();
                return rooms;
            }
        }

        public async void AddRoom(Room room)
        {
            Thread.Sleep(10);
            List<Room> rooms = GetAllRooms().Result;
            rooms.Add(room);
            using (FileStream fw = new(relativeDBPath, FileMode.Open))
            {
                Thread.Sleep(10);
                await JsonSerializer.SerializeAsync(fw, rooms, options);
                await fw.DisposeAsync();
            }
        }
    }
}
