namespace SOLID_exercise.SRP
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        // ... other properties
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
        //public void SaveToDatabase()
        //{
        //    // Save book to the database
        //}
        public string GetBookSummary()
        {
            return Title + "by" + Author;
        }
    }
}
