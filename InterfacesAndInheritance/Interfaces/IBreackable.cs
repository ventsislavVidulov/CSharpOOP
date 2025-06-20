namespace InterfacesAndInheritance.Interfaces
{
    internal interface IBreackable
    {
        // 3. Breakable Interface(10 points):
        // Create a Breakable interface to indicate items that can break.
        // Include methods for checking if an item is breakable and for handling item breakage.

        bool IsBreakable();
        void HandleBreakage();
        void DisplayIsBreakable(); 
    }
}
