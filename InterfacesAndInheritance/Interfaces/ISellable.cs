using System.Diagnostics;
using System;

namespace InterfacesAndInheritance.Interfaces
{
    internal interface ISellable
    {
        // 5. Sellable Interface(10 points):
        // Create a Sellable interface to represent items that can be sold.
        // Include methods for setting and getting item prices.

        void SetPrice(decimal price);

        decimal GetPrice();

    }
}
