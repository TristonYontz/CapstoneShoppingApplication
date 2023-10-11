using Capstone.Classes;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CapstoneTests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void TakeMoneyTest()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)
            decimal result = testObject.TakeMoney(100);

            //Assert
            Assert.AreEqual(100, result);
        }
        [TestMethod]
        public void CheckBalanceTestUnder1000()
        {
            //Arrange
            Store testObject = new Store();
            testObject.Balance = 999;
            //Act (done in arrange above)
            bool result = testObject.CheckBalance(20);
            
            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckBalanceTestOver100()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)
            bool result = testObject.CheckBalance(101);

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckBalanceTestUnder0()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)
            bool result = testObject.CheckBalance(-10);

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckInventoryTestFalse()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 100;
            sourPatchKids.TypeOfCandy = "Sour";
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.CheckInventory(testDictionary, "C3");

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckInventoryTestTrue()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 100;
            sourPatchKids.TypeOfCandy = "Sour";
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.CheckInventory(testDictionary, "A1");

            //Assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void CheckAvailablitityTestUnder0()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 0;
            sourPatchKids.TypeOfCandy = "Sour";
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.CheckAvailability(testDictionary, "A1");

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckAvailablitityTestOver100()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 101;
            sourPatchKids.TypeOfCandy = "Sour";
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.CheckAvailability(testDictionary, "A1");

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void CheckAvailablitityTrue()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 1;
            sourPatchKids.TypeOfCandy = "Sour";
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.CheckAvailability(testDictionary, "A1");

            //Assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void EnoughStockTestFalse()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 10;
            sourPatchKids.TypeOfCandy = "Sour";
            sourPatchKids.Price = 10;
            testObject.Balance = 0;
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.EnoughStock(testDictionary, "A1", 15);

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void EnoughStockTestTrue()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 30;
            sourPatchKids.TypeOfCandy = "Sour";
            sourPatchKids.Price = 10;
            testObject.Balance = 0;
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.EnoughStock(testDictionary, "A1", 15);

            //Assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void EnoughMoneyTestBalanceOverPricefalse()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 1;
            sourPatchKids.TypeOfCandy = "Sour";
            sourPatchKids.Price = 10;
            testObject.Balance = 10;
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.EnoughMoney(testDictionary, "A1", 15);

            //Assert
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void EnoughMoneyTestBalanceOverPricetrue()
        {
            //Arrange
            Store testObject = new Store();
            Item sourPatchKids = new Item();
            sourPatchKids.Id = "A1";
            sourPatchKids.IsWrapped = "Y";
            sourPatchKids.Name = "Sour Patch Kids";
            sourPatchKids.Quantity = 1;
            sourPatchKids.TypeOfCandy = "Sour";
            sourPatchKids.Price = 10;
            testObject.Balance = 200;
            Dictionary<string, Item> testDictionary = new Dictionary<string, Item>();
            testDictionary.Add("A1", sourPatchKids);
            //Act (done in arrange above)
            bool result = testObject.EnoughMoney(testDictionary, "A1", 15);

            //Assert
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void ReturnChangeTest()
        {
            //Arrange
            Store testObject = new Store();
            testObject.Balance = 222.20m;
            //Act (done in arrange above)
            string result = testObject.ReturnChange();

            //Assert
            Assert.AreEqual("(11) Twenties, (0) Tens, (0) Fives, (2) Ones, (0) Quarters, (2) Dimes, (0) Nickles", result);
        }
        [TestMethod]
        public void SortCandiesTestReturnC()
        {
            //Arrange
            Store testObject = new Store();
            Item CandyTest = new Item();
            CandyTest.Id = "C1";
            testObject.shoppingCart.Add(CandyTest);
            //Act (done in arrange above)
            string result = testObject.SortCandies(0, testObject);

            //Assert
            Assert.AreEqual("Chocolate Confectionery", result);
        }
        [TestMethod]
        public void SortCandiesTestReturnL()
        {
            //Arrange
            Store testObject = new Store();
            Item CandyTest = new Item();
            CandyTest.Id = "L1";
            testObject.shoppingCart.Add(CandyTest);
            //Act (done in arrange above)
            string result = testObject.SortCandies(0, testObject);

            //Assert
            Assert.AreEqual("Licorce and Jellies", result);
        }
        [TestMethod]
        public void SortCandiesTestReturnH()
        {
            //Arrange
            Store testObject = new Store();
            Item CandyTest = new Item();
            CandyTest.Id = "H1";
            testObject.shoppingCart.Add(CandyTest);
            //Act (done in arrange above)
            string result = testObject.SortCandies(0, testObject);

            //Assert
            Assert.AreEqual("Hard Tack Confectionery", result);
        }
    }
}
