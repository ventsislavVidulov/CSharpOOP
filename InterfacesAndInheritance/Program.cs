namespace InterfacesAndInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var laptopAcer = new InventoryItem(1, 10, "Laptop from ACER", 1500.00m, "Electronics");
            laptopAcer.DisplayDescription();
        }
    }
}
