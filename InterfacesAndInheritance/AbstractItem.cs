using InterfacesAndInheritance.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InterfacesAndInheritance
{
    internal abstract class AbstractItem : IItem, ICategorizable, IBreakable, IPerishable, ISellable
    {
        // 6. *Abstract Item Class(15 points) :
        // Create an abstract class AbstractItem that implements the Item, Categorizable, Breakable, Perishable, and Sellable interfaces.
        // Implement common functionality such as getting item details.
        // Provide default implementations for category, breakable, perishable, and sellable attributes.
        protected string _details = "Unset details";
        protected decimal _value;
        protected string _description = "Unset description";

        //IItem methods
        public decimal CalculateValue()
        {
            return _value;
        }

        public void DisplayDescription()
        {
            Console.WriteLine($"{_description}");
        }
        public string GetItemDetails()
        {
            return this._details;
        }

        //ICategorizable methods
        public abstract string GetCategory();
        public abstract void SetCategory(string category);

        //IBreakable methods
        public abstract bool IsBreakable();
        public abstract void HandleBreakage();
        public abstract void DisplayIsBreakable();

        //IPerishable methods

        public abstract bool IsPerishable();
        public abstract void HandlePerish();
        public abstract void DisplayIsPerishable();

        //ISellable methods
        public abstract void SetPrice(decimal price);
        public abstract decimal GetPrice();

    }
}
