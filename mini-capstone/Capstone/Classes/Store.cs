using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Capstone.Classes
{
    /// <summary>
    /// Most of the "work" (the data and the methods) of dealing with inventory and money 
    /// should be created or controlled from this class
    /// </summary>
    public class Store
    {
        public decimal Balance { get; set; } = 0;

        public decimal Deposit { get; set; } = 0;

        public List<Item> shoppingCart { get; set; } = new List<Item>();
        public List<int> NumberSold { get; set; } = new List<int>();

        private FileIo inventoryAccess = new FileIo();
        public decimal TakeMoney(decimal deposit)
        {
            return Balance += deposit;
        }
        public bool CheckBalance(decimal deposit)
        {
            bool checkBalance = false;
            if (deposit + Balance < 1000.00m && deposit <= 100.00m && deposit >= 1)
            {
                checkBalance = true;
            }
            return checkBalance;
        }
        public bool CheckInventory(Dictionary<string, Item> inventory, string userInputId)
        {
            bool checkAvailability = false;
            if (inventory.ContainsKey(userInputId))
            {
                checkAvailability = true;
            }
            return checkAvailability;

        }
        public bool CheckAvailability(Dictionary<string, Item> inventory, string userInputId)
        {
            bool checkAvailability = false;
            if(inventory[userInputId].Quantity > 0 && inventory[userInputId].Quantity <= 100)
            {
                checkAvailability = true;
            }
            return checkAvailability;
        }
        public bool EnoughStock(Dictionary<string, Item> inventory, string userInputId, int quantitySelected)
        {
            bool enoughStock = false;
            if(quantitySelected <= inventory[userInputId].Quantity)
            {
                enoughStock = true;
            }
            return enoughStock;
        }
        public bool EnoughMoney(Dictionary<string, Item> inventory, string userInputId, int quantitySelected)
        {
            bool enoughMoney = false;
            if (Balance > (inventory[userInputId].Price * quantitySelected) && Balance > 0)
            {
                enoughMoney = true;
            }
            return enoughMoney;
        }

        public Dictionary<string, Item> StoreInventory()
        {
            Dictionary<string, Item> storeInventory = new Dictionary<string, Item>();
            List<Item> itemsArray = inventoryAccess.MakeInventory();
            foreach (Item items in itemsArray)
            {
                storeInventory.Add(items.Id, items);
            }
            return storeInventory;
        }
        public void AddToCart(Item price, int quantity)
        {
            Balance -= (quantity * price.Price);
            price.Quantity = price.Quantity - quantity;
            shoppingCart.Add(price);
            NumberSold.Add(quantity);
        }
        public string ReturnChange()
        {
            int twenties = 0;
            int tens = 0;
            int fives = 0;
            int ones = 0;
            int quarters = 0;
            int dimes = 0;
            int nickles = 0;
            if(Balance > 20)
            {
                twenties = (int)(Balance / 20.00m);
                Balance -= (twenties * 20);
            }
            if (Balance > 10)
            {
                tens = (int)(Balance / 10.00m);
                Balance -= (tens * 10);
            }
            if (Balance > 5)
            {
                fives = (int)(Balance / 5.00m);
                Balance -= (fives * 5);
            }
            if (Balance > 1)
            {
                ones = (int)(Balance / 1.00m);
                Balance -= (ones * 1);
            }
            if (Balance > .25m)
            {
                quarters = (int)(Balance / .25m);
                Balance -= (quarters * .25m);
            }
            if (Balance > .10m)
            {
                dimes = (int)(Balance / .10m);
                Balance -= (dimes * .10m);
            }
            if (Balance > .05m)
            {
                nickles = (int)(Balance / .05m);
                Balance -= (nickles * .05m);
            }
            return $"({twenties}) Twenties, ({tens}) Tens, ({fives}) Fives, ({ones}) Ones, ({quarters}) Quarters, ({dimes}) Dimes, ({nickles}) Nickles";
        }
        public string SortCandies(int candy, Store item)
        {
            if (item.shoppingCart[candy].Id.Substring(0,1) == "C")
            {
                return "Chocolate Confectionery";
            }
            else if (item.shoppingCart[candy].Id.Substring(0, 1) == "S")
            {
                return "Sour Flavored Candies";
            }
            else if (item.shoppingCart[candy].Id.Substring(0, 1) == "L")
            {
                return "Licorce and Jellies";
            }
            else
            {
                return "Hard Tack Confectionery";
            }

        }
        public string MoneyRecevied(decimal deposit)
        {
            return $" {DateTime.UtcNow} MONEY RECIEVED: ${deposit} ${Balance}";
        }

    }
}
