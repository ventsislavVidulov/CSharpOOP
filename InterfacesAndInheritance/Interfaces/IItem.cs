namespace InterfacesAndInheritance.Interfaces
{
    internal interface IItem
    {
        // 1. Item Interface(15 points):
        // Define methods for getting item details, calculating value, and displaying the
        // item's description.

        string GetItemDetails();

        decimal CalculateValue();

        void DisplayDescription();  
    }
}
