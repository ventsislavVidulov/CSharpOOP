namespace SOLID_exercise.SRP
{
    internal class Print
    {
        public void PrintInvoice(Invoice invoice)
        {
            Console.WriteLine($"Printing invoice for {invoice.CustomerName} with amount {invoice.Amount}");
        }
    }
}
