namespace SOLID_exercise.SRP
{
    internal class DBContext
    {
        public void SaveBookToDatabase(Book book)
        {
            Console.WriteLine($"Saving book '{book.GetBookSummary()}' to the database.");
        }
        public void SaveInvoiceToDatabase(Invoice invoice)
        {
            Console.WriteLine($"Saving invoice for '{invoice.CustomerName}' with amount {invoice.Amount} to the database.");
        }
    }
}
