using Capstone.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void StoreTestObjectCreation()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)

            //Assert
            Assert.IsNotNull(testObject);
        }
        [TestMethod]
        public void StoreTestObjectCreation()
        {
            //Arrange
            Store testObject = new Store();

            //Act (done in arrange above)

            //Assert
            Assert.IsNotNull(testObject);
        }
    }
}
