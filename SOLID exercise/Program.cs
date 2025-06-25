namespace SOLID_exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new SRP.DBContext();
            var book = new SRP.Book("The Great Gatsby", "F. Scott Fitzgerald");
            dbContext.SaveToDatabase(book);
        }
    }
}
