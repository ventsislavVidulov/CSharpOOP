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
            if (rooms.Count == 0)
            {
                rooms = await roomDBManager.GetAllRooms();
            }
            return rooms;

        }

        public async Task<List<Room>> GetAllRoomsByDate(DateTime date)
        {
            rooms = await GetAllRooms();
            foreach (var room in rooms)
            {
                foreach (var interval in room.ReserveationIntervals)
                {
                    if (interval.StartDate <= date && interval.EndDate > date)
                    {
                        room.RoomStatus = RoomStatus.Booked;
                        room.NumberOfOccupants = room.NumberOfBeds;
                    }
                    else
                    {
                        room.RoomStatus = RoomStatus.Available;
                        room.NumberOfOccupants = 0;
                    }
                }
            }
            return rooms;
        }


        public async Task<List<Room>> GetAllRoomsByRoomType(string roomType)
        {
            List<Room> filteredRooms = rooms.FindAll(r => Enum.GetName(typeof(RoomType), r.RoomType) == roomType);
            List<Room> avaiableFilteredRooms = filteredRooms.FindAll(r => r.RoomStatus == 0);
            return avaiableFilteredRooms;
        }

        public async Task ReserveRoom(string roomType, ReservationInterval reservationInterval)
        {
            List<Room> rooms = await GetAllRoomsByRoomType(roomType);
            //Cheking if this room type exists
            if (rooms != null)
            {
                bool isRoomAvailable = false;
                foreach (var room in rooms)
                {
                    if (!isRoomAvailable)
                    {
                        //If the room hasnt any reservation intervals
                        if (room.ReserveationIntervals.Count == 0)
                        {
                            isRoomAvailable = true;
                            room.ReserveationIntervals.Add(reservationInterval);
                            await roomDBManager.UpdateRoom(room);
                            Console.WriteLine($"Room {room.RoomNumber} reserved from {reservationInterval.StartDate} to {reservationInterval.EndDate}");
                            return;
                        }
                        else
                        {
                            foreach (var savedInterval in room.ReserveationIntervals)
                            {
                                //checking if the reservation interval is not overlapping with any of the saved intervals
                                if ((reservationInterval.StartDate >= savedInterval.EndDate && reservationInterval.EndDate >= savedInterval.EndDate)
                                    || (reservationInterval.StartDate <= savedInterval.StartDate && reservationInterval.EndDate <= savedInterval.StartDate))
                                {
                                    isRoomAvailable = true;
                                    room.ReserveationIntervals.Add(reservationInterval);
                                    await roomDBManager.UpdateRoom(room);
                                    Console.WriteLine($"Room {room.RoomNumber} reserved from {reservationInterval.StartDate} to {reservationInterval.EndDate}");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                if (!isRoomAvailable)
                {
                    Console.WriteLine("No available rooms");
                }
            }
            else
            {
                Console.WriteLine("Room not found");
            }
        }
    }
}
