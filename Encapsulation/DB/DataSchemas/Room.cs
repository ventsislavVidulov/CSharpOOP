namespace Encapsulation.DB.DataSchemas
{
    internal enum RoomType
    {
        Single = 1,
        Double,
        Suite,
        Deluxe
    }

    internal enum AvaiableAmenities
    {
        Jacuzzi,
        KingBed
    }

    internal enum RoomStatus
    {
        Available,
        Booked,
        UnderMaintenance
    }


    internal class Room
    {
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public int NumberOfBeds { get; set; }

        public int NumberOfOccupants { get; set; }

        public double PricePerNight { get; set; }

        public double CancellationFee { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public List<AvaiableAmenities> Amenities { get; set; } = new();

        public List<ReservationInterval> ReserveationIntervals { get; set; } = new();

        public Room(int roomNumber, RoomType roomType)
        {
            this.RoomNumber = roomNumber;
            RoomType = roomType;
            if (roomType == RoomType.Single)
            {
                NumberOfBeds = 1;
                PricePerNight = 100;
            }
            else if (roomType == RoomType.Double)
            {
                NumberOfBeds = 2;
                PricePerNight = 150;
            }
            else if (roomType == RoomType.Suite)
            {
                NumberOfBeds = 3;
                Amenities.Add(AvaiableAmenities.Jacuzzi);
                PricePerNight = 200;
            }
            else if (roomType == RoomType.Deluxe)
            {
                NumberOfBeds = 4;
                Amenities.Add(AvaiableAmenities.KingBed);
                Amenities.Add(AvaiableAmenities.Jacuzzi);
                PricePerNight = 300;
            }
            else
            {
                throw new ArgumentException("Invalid room type");
            }
            NumberOfOccupants = 0;
            CancellationFee = PricePerNight * Configuration.DiscountPercent;
            RoomStatus = RoomStatus.Available;
        }
    }
}
