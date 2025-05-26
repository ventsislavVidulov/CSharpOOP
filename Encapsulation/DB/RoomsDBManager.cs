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
                try
                {
                    // Create the file and write an empty list to it
                    using (FileStream fs = new(relativeDBPath, FileMode.Create))
                    {
                        await JsonSerializer.SerializeAsync(fs, (new List<Room>()), options);
                        //await fs.DisposeAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error creating DB: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public async Task<List<Room>?> GetAllRooms()
        {
            if (rooms == null)
            {
                try
                {
                    using (FileStream fs = new(relativeDBPath, FileMode.Open))
                    {
                        rooms = await JsonSerializer.DeserializeAsync<List<Room>>(fs, options);
                        //await fs.DisposeAsync();
                    }
                }
                catch (JsonException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    Console.ResetColor();
                }
            }
            return rooms;
        }

        public async Task AddRoom(Room room)
        {
            rooms = await GetAllRooms();
            rooms.Add(room);
            try
            {
                using (FileStream fw = new(relativeDBPath, FileMode.Create))
                {
                    await JsonSerializer.SerializeAsync(fw, rooms, options);
                    //await fw.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error adding room: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                //or if the writting failed removes the list with the newly added room
                rooms = null;
            }
        }

        public async Task UpdateRoom(Room roomToUpdate)
        {
            rooms = await GetAllRooms();
            int roomToUpdateIndex = rooms.FindIndex(r => r.RoomNumber == roomToUpdate.RoomNumber);
            if (roomToUpdateIndex != -1)
            {
                try
                {
                    rooms[roomToUpdateIndex] = roomToUpdate;
                    using (FileStream fw = new(relativeDBPath, FileMode.Create))
                    {
                        await JsonSerializer.SerializeAsync(fw, rooms, options);
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error updating room: {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                    //or if the writting failed removes the list with the newly added room
                    rooms = null;
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
                try
                {
                    using (FileStream fw = new(relativeDBPath, FileMode.Create))
                    {
                        rooms.RemoveAll(r => r.RoomNumber == roomToRemove.RoomNumber);
                        await JsonSerializer.SerializeAsync(fw, rooms, options);
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error deleting room: {ex.Message}");
                    Console.ResetColor();
                }
                finally
                {
                    //reset rooms so that the next call to GetAllRooms will read from the file the actual data
                    //or if the writting failed removes the list with the newly removed room
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
