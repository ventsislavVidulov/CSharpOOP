using Encapsulation.DB.DataSchemas;
using Encapsulation.Interfaces;

namespace Encapsulation.Controlers
{
    internal class RoomController
    {
        private List<Room> rooms = new();

        private IRoomDBManager roomDBManager;
        public RoomController(IRoomDBManager roomDBManager)
        {
            this.roomDBManager = roomDBManager;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            rooms = await roomDBManager.GetAllRooms();
            return rooms;
        }

        public async Task<List<Room>> GetAllRoomsByRoomType(string roomType)
        {
            if (rooms.Count == 0)
            {
                await GetAllRooms();
            }
            List<Room> filteredRooms = rooms.FindAll(r => Enum.GetName(typeof(RoomType), r.RoomType) == roomType);
            List<Room> avaiableFilteredRooms = filteredRooms.FindAll(r => r.RoomStatus == 0);
            return avaiableFilteredRooms;
        }

        public async Task ReserveRoom(string roomType, ReservationInterval reservationInterval)
        {
            List<Room> rooms = await GetAllRoomsByRoomType(roomType);
            if (rooms != null)
            {
                rooms[0].ReserveationIntervals.Add(reservationInterval);
                await roomDBManager.UpdateRoom(rooms[0]);
                Console.WriteLine(rooms[0].ReserveationIntervals);

            }
            else
            {
                Console.WriteLine("Room not found");
            }
        }
    }
}
