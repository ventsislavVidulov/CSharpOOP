namespace Encapsulation.DB.DataSchemas
{
    class ReservationInterval
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ReservedDays { get; set; }
        public ReservationInterval(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            ReservedDays = (int)(endDate - startDate).TotalDays;
        }
    }
}
