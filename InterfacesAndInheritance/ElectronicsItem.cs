using InterfacesAndInheritance.Interfaces;

namespace InterfacesAndInheritance
{
    internal class ElectronicsItem : InventoryItem, Interfaces.IBreakable, Interfaces.ICategorizable, Interfaces.ISellable
    {
        // 8. Item Types(30 points):
        // Create subclasses for specific item types like ElectronicsItem, GroceryItem, and FragileItem that inherit from InventoryItem.
        // Implement constructors for these subclasses to set specific attributes like weight for fragile items.
        // Override relevant methods to calculate item values differently for each type.
        protected bool IsBreakable { get; private set; }
        private stirng

        public ElectronicsItem(int id, int quantity, string details, decimal value, string description) : base(id, quantity, details, value, description)
        {

        }

        public bool IsBreakable()
        {
            return IsBreakable;
        }

        public void HandleBreakage()
        {
            throw new NotImplementedException();
        }

        public void DisplayIsBreakable()
        {
            throw new NotImplementedException();
        }
    }
}
