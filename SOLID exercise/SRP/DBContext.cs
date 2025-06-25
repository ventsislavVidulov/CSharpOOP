namespace SOLID_exercise.SRP
{
    internal class DBContext
    {
        public void SaveToDatabase(Book book)
        {
            Console.WriteLine($"Saving book '{book.GetBookSummary()}' to the database.");
        }
    }
}
