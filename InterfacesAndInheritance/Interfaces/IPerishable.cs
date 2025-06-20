namespace InterfacesAndInheritance.Interfaces
{
    internal interface IPerishable
    {
        // 4. Perishable Interface(10 points):
        // Create a Perishable interface to represent items that can perish.
        // Include methods for checking if an item is perishable and for handling item expiration.

        bool IsPerishable();

        void HandleExpiration();

        void DisplayIsPerishable();
    }
}
