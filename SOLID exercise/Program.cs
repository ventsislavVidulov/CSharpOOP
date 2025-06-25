using SOLID_exercise.OCP.Logger;
using SOLID_exercise.OCP.Logger.LoggerMethods;

namespace SOLID_exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SRP

            var dbContext = new SRP.DBContext();
            var printService = new SRP.Print();

            var book = new SRP.Book("The Great Gatsby", "F. Scott Fitzgerald");
            dbContext.SaveBookToDatabase(book);

            var invoice = new SRP.Invoice(250.00, "John Doe");
            dbContext.SaveInvoiceToDatabase(invoice);
            printService.PrintInvoice(invoice);

            //OCP

            var consoleLogger = new LogToConsole();
            var fileLogger = new LogToFile();
            var emasilLogger = new LogToEmail();

            var logger = new Logger(consoleLogger);
            logger.Log("This is a log message");
            logger.LoggerService = fileLogger;
            logger.Log("This is another log message");
            logger.LoggerService = emasilLogger;
            logger.Log("This is a log message sent via email");
        }
    }
}
