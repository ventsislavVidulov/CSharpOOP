using InterfacesAndInheritance.Interfaces;
using System.Globalization;

namespace InterfacesAndInheritance
{
    internal class InventoryItem : AbstractItem
    {
        // 7. Inventory Superclass(20 points):
        // Create an InventoryItem superclass *(that extends AbstractItem).
        // Add instance variables for item ID and quantity.
        // Implement getters and setters for ID and quantity.
        private int Id { get; set; }
        private int Quantity { get; set; }

        public InventoryItem(int id, int quantity, string details, decimal value, string description) : base()
        {
            Id = id;
            Quantity = quantity;
            _details = details;
            _value = value;
            _description = description;
        }

        //IItem methods
        //public decimal CalculateValue()
        //{
        //    return _value;
        //}

        //public void DisplayDescription()
        //{
        //    Console.WriteLine($"{_description}");
        //}
        //public string GetItemDetails()
        //{
        //    return this._details;
        //}

        public override void DisplayIsBreakable()
        {
            throw new NotImplementedException();
        }

        public override void DisplayIsPerishable()
        {
            throw new NotImplementedException();
        }

        public override string GetCategory()
        {
            throw new NotImplementedException();
        }

        public override decimal GetPrice()
        {
            throw new NotImplementedException();
        }

        public override void HandleBreakage()
        {
            throw new NotImplementedException();
        }

        public override void HandlePerish()
        {
            throw new NotImplementedException();
        }

        public override bool IsBreakable()
        {
            throw new NotImplementedException();
        }

        public override bool IsPerishable()
        {
            throw new NotImplementedException();
        }

        public override void SetCategory(string category)
        {
            throw new NotImplementedException();
        }

        public override void SetPrice(decimal price)
        {
            throw new NotImplementedException();
        }
    }
}
