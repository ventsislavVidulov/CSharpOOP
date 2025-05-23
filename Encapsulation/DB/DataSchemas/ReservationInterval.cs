namespace Encapsulation.DB.DataSchemas
{
    class ReservationInterval
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ReservedDays { get; set; }

        public int ReservationId { get; set; }
        public ReservationInterval(DateTime startDate, DateTime endDate, int reservationId)
        {
            StartDate = startDate;
            EndDate = endDate;
            ReservedDays = (int)(endDate - startDate).TotalDays;
            ReservationId = reservationId;
        }
    }
}
