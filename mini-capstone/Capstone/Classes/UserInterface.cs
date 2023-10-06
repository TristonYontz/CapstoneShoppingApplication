using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Schema;

namespace Capstone.Classes
{
    class UserInterface
    {
        private Store store = new Store();
        private FileIo audit = new FileIo();
        Dictionary<string, Item> inventory;
        /// <summary>
        /// Provides all communication with human user.
        /// 
        /// All Console.Readline() and Console.WriteLine() statements belong 
        /// in this class.
        /// 
        /// NO Console.Readline() and Console.WriteLine() statements should be 
        /// in any other class
        /// 
        /// </summary>
        private void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome, please enter a number");
            Console.WriteLine("(1) Show Inventory");
            Console.WriteLine("(2) Make Sale");
            Console.WriteLine("(3) Quit");
        }
        public void Run()
        {
            inventory = store.StoreInventory();
            bool isDone = false;
            while (!isDone)
            {
                DisplayMenu();
                string userResponse = Console.ReadLine();
                switch (userResponse)
                {
                    case "1":
                        ShowInventory();
                        break;
                    case "2":
                        DisplaySubMenu();
                        break;
                    case "3":
                        isDone = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }
            }
        }

        private void ShowInventory()
        {

            Console.WriteLine();
            DisplayHeaderInventory();
            if (inventory.Count == 0)
            {
                Console.WriteLine("No items in list");
            }
            else
            {
                List<Item> temp = new List<Item>();
                foreach (Item item in inventory.Values)
                {
                    temp.Add(item);
                }

                temp = temp.OrderBy(x => x.Id).ToList();

                foreach (Item value in temp)
                {
                    Console.WriteLine(value);
                }
            }


        }
        private void DisplayHeaderInventory()
        {
            string id = "Id";
            string name = "Name";
            string wrapper = "Wrapper";
            string qty = "Qty";
            string price = "Price";
            Console.WriteLine($"{id.PadRight(5)}{name.PadRight(20)}{wrapper.PadRight(10)}{qty.PadRight(10)}{price.PadRight(10)}");
        }

        private void DisplaySubMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("(1) Take Money");
            Console.WriteLine("(2) Select Products");
            Console.WriteLine("(3) Complete Sale");
            Console.WriteLine($"Current Customer Balance: ${store.Balance}");
            string userResponse = Console.ReadLine();
            switch (userResponse)
            {
                case "1":
                    TakeMoney();
                    break;
                case "2":
                    SelectProducts();
                    break;
                case "3":
                    CompleteSale();
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice.");
                    break;
            }
        }

        private void TakeMoney()
        {
            Console.WriteLine("Deposit up to $100.");
            bool isInputValid = false;


            while (!isInputValid)
            {


                try
                {


                    string userResponse = Console.ReadLine();
                    decimal deposit = decimal.Parse(userResponse);
                    if (deposit + store.Balance < 1000.00m && deposit <= 100.00m)
                    {
                        store.TakeMoney(deposit);
                        audit.WritingAudit(store.MoneyRecevied(deposit));
                        Console.WriteLine($"Store's new balance: ${store.Balance}");
                        isInputValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input please choose a number between 1 and 100");
                        continue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter digits");
                    continue;
                }

            }
        }
        private void SelectProducts()
        {
            ShowInventory();
            Console.WriteLine("Please enter product Id");
            string userInputId = Console.ReadLine().ToUpper();
            if (!CheckStock(inventory.ContainsKey(userInputId)))
            {
                return;
            }
            Console.WriteLine("Please enter desired quantity");
            string userInputQuantity = Console.ReadLine();
            int quantitySelected = int.Parse(userInputQuantity);
            if (!checkAvailibility(inventory[userInputId].Quantity > 0 && inventory[userInputId].Quantity <= 100))
            {
                return;
            }
            if (!EnoughStock(quantitySelected <= inventory[userInputId].Quantity))
            {
                return;
            }
            if (!Transaction(store.Balance > inventory[userInputId].Price && store.Balance > 0))
            {
                return;
            }
            store.AddToCart(inventory[userInputId], quantitySelected);
            audit.WritingAudit($"{DateTime.UtcNow} {quantitySelected} {inventory[userInputId].Name} {inventory[userInputId].Id} {inventory[userInputId].Price * quantitySelected} {store.Balance}");
            DisplaySubMenu();
        }
        private bool CheckStock(bool containsKey)
        {
            if (containsKey)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Product not found");
                return false;
            }
        }
        private bool checkAvailibility(bool enoughAvailibity)
        {
            if (enoughAvailibity)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Sorry the product is sold out");
                return false;
            }
        }
        private bool EnoughStock(bool enoughStock)
        {
            if (enoughStock)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient stock");
                return false;
            }
        }
        private bool Transaction(bool enoughMoney)
        {
            if (enoughMoney)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient funds");
                return false;
            }
        }
        public void CompleteSale()
        {
            try
            {

                Console.WriteLine();
                if (store.shoppingCart.Count == 0)
                {
                    Console.WriteLine("No items in Cart");
                }
                else
                {
                    decimal totalAmountPurchased = 0;
                    for (int i = 0; i < store.shoppingCart.Count; i++)
                    {
                        totalAmountPurchased += (store.shoppingCart[i].Price * store.NumberSold[i]);
                        Console.WriteLine($"{store.NumberSold[i]} {store.shoppingCart[i].Name} {store.SortCandies(i, store)} ${store.shoppingCart[i].Price} ${store.shoppingCart[i].Price * store.NumberSold[i]}");
                    }
                    audit.WritingAudit($"{DateTime.UtcNow} Change Given: ${store.Balance} $0.00");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Total: ${totalAmountPurchased}");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Change: ${store.Balance}");
                    Console.WriteLine($"{store.ReturnChange()}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}

