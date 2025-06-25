namespace SOLID_exercise.SRP
{
    public class Invoice(double amount, string customerName)
    {
        public double Amount { get; set; } = amount;
        public string CustomerName { get; set; } = customerName;
        //public void PrintInvoice()
        //{
        //    // Print invoice
        //}
        //public void SaveInvoice()
        //{
        //    // Save invoice to database
        //}
    }
}
