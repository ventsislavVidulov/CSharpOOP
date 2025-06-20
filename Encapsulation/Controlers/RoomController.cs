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


        public async Task<List<Room>> GetAllAvailableRoomsForGivenRoomType(string roomType)
        {
            List<Room> allRooms = await GetAllRooms();
            List<Room> availableFilteredRooms = allRooms
                .Where(r => Enum.GetName(typeof(RoomType), r.RoomType) == roomType && r.RoomStatus == 0)
                .ToList();
            //List<Room> filteredRooms = allRooms.FindAll(r => Enum.GetName(typeof(RoomType), r.RoomType) == roomType);
            //List<Room> availableFilteredRooms = filteredRooms.FindAll(r => r.RoomStatus == 0);
            return availableFilteredRooms;
        }

        public async Task ReserveRoom(string roomType, ReservationInterval reservationInterval)
        {
            List<Room> rooms = await GetAllAvailableRoomsForGivenRoomType(roomType);
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
                            //checking if the reservation interval is not overlapping with any of the saved intervals
                            foreach (var savedInterval in room.ReserveationIntervals)
                            {
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No available rooms");
                    Console.ResetColor();
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
