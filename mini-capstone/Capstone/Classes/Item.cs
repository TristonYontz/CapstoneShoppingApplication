using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Capstone.Classes
{
    public class Item
    {
        public int Quantity { get; set; } = 100;

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string IsWrapped { get; set; }

        public string Id { get; set; }
        
        public string TypeOfCandy { get; set; }

       
       
       public override string ToString()
        {
            string id = Id;
            string name = Name;
            string isWrapped = IsWrapped;
            string quantity = "";
            if (Quantity == 0)
            {
                quantity = quantity + "Sold Out";
            }
            else
            {
                quantity = quantity + $"{Quantity}";
            }
            string price = $"{ Price }";
            return id.PadRight(5) + name.PadRight(20) + isWrapped.PadRight(10) + quantity.PadRight(10) + price.PadRight(10);
        }
        

    }
}
