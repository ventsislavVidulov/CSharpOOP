using Encapsulation.Controlers;
using Encapsulation.DB;
using Encapsulation.DB.DataSchemas;

namespace Encapsulation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            RoomsDBManager roomsDBManager = new RoomsDBManager();
            await roomsDBManager.CreateDB();
            RoomController roomController = new RoomController(roomsDBManager);
            UserDBManager userDBManager = new UserDBManager();
            await userDBManager.CreateDB();
            UserController userController = new UserController(userDBManager);
            OrdersCounterDBManager ordersCounterDBManager = new OrdersCounterDBManager();
            await ordersCounterDBManager.CreateDB();
            DateTime date = DateTime.Now.Date;
            List<Room>? startUpRooms = await roomController.GetAllRooms();
            if (startUpRooms.Count == 0)
            {
                await roomsDBManager.AddRoom(new Room(101, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(102, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(103, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(104, RoomType.Deluxe));
                await roomsDBManager.AddRoom(new Room(201, RoomType.Single));
                await roomsDBManager.AddRoom(new Room(202, RoomType.Double));
                await roomsDBManager.AddRoom(new Room(203, RoomType.Suite));
                await roomsDBManager.AddRoom(new Room(204, RoomType.Deluxe));
            }
            User curentUser = null;
            while (true)
            {
                ConsoleManager.WelcomeMessage(curentUser);
                Console.WriteLine(date);
                if (curentUser == null)
                {
                    ;
                    int choosenOption = ConsoleManager.NotLogetUser();
                    if (choosenOption == 1)
                    {
                        curentUser = await userDBManager.AddUser(ConsoleManager.AddUser());
                        if (curentUser != null)
                        {
                            Console.WriteLine($"Welcome {curentUser.UserName}");
                        }
                    }
                    else if (choosenOption == 2)
                    {
                        string userName;
                        string password;
                        ConsoleManager.LogIn(out userName, out password);
                        curentUser = await userController.LogIn(userName, password);
                        if (curentUser != null)
                        {
                            Console.WriteLine($"Welcome {curentUser.UserName}");
                        }
                    }
                }
                else
                {
                    if (curentUser.Role == "user")
                    {
                        int choosenOption = ConsoleManager.LoggedUser();
                        if (choosenOption == 1)
                        {
                            List<Room> rooms = await roomController.GetAllRoomsByDate(date);
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }
                        else if (choosenOption == 3)
                        {
                            //TODO must be formed some kind of transaction when updating rooms and user
                            int roomType = ConsoleManager.RoomType();
                            List<Room> rooms = await roomController.GetAllRoomsByRoomType(Enum.GetName(typeof(RoomType), roomType));
                            var dates = ConsoleManager.ReservedDates();
                            var counter = await ordersCounterDBManager.GetCounter();
                            curentUser.Reservations.Add(counter.CurrentOrderId);
                            var reservationInterval = new ReservationInterval(dates.Item1, dates.Item2, counter.CurrentOrderId);
                            await ordersCounterDBManager.IncreaseOrderId();
                            await roomController.ReserveRoom(Enum.GetName(typeof(RoomType), roomType), reservationInterval);
                        }
                        else if (choosenOption == 4)
                        {
                            date = ConsoleManager.ChangeDate();
                        }
                    }
                    else if (curentUser.Role == "admin")
                    {
                        int choosenOption = ConsoleManager.LoggedAdmin();
                        if (choosenOption == 1)
                        {
                            List<Room> rooms = await roomController.GetAllRoomsByDate(date);
                            ConsoleManager.ShowAllRooms(rooms);
                        }
                        else if (choosenOption == 2)
                        {
                            curentUser = null;
                            ConsoleManager.LogOut();
                        }
                    }
                }
            }
        }
    }
}
