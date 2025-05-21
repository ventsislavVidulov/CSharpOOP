using Encapsulation.DB.DataSchemas;

namespace Encapsulation.Interfaces
{
    internal interface IRoomDBManager
    {
        public Task CreateDB();
        public Task<List<Room>> GetAllRooms();
        public Task AddRoom(Room room);
        public Task UpdateRoom(Room roomToUpdate);
        public Task DeleteRoom(Room roomToRemove);
    }
}
