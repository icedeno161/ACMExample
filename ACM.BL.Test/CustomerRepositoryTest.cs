using ACM.BL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ACM.BL.Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void FindTestExistingCustomer()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.Find(customerList, 2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.CustomerId);
            Assert.AreEqual("Baggins", result.LastName);
            Assert.AreEqual("Bilbo", result.FirstName);
        }

        [TestMethod]
        public void FindTestNotFound()
        {
            // Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            // Act
            var result = repository.Find(customerList, 42);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void SortByNameTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var sortedList = repository.SortByName(customerList);
            var actualLastName = sortedList.First().LastName;
            var actualFirstName = sortedList.First().FirstName;

            var expectedLastName = "Baggins";
            var expectedFirstName = "Bilbo";

            //Assert
            Assert.IsNotNull(sortedList);
            Assert.AreEqual(expectedLastName, actualLastName);
            Assert.AreEqual(expectedFirstName, actualFirstName);
        }

        [TestMethod()]
        public void SortByNameInReverseTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var sortedList = repository.SortByNameInReverse(customerList);
            var actualLastName = sortedList.First().LastName;
            var actualFirstName = sortedList.First().FirstName;

            var expectedLastName = "Gamgee";
            var expectedFirstName = "Samwise";

            //Assert
            Assert.IsNotNull(sortedList);
            Assert.AreEqual(expectedFirstName, actualFirstName);
            Assert.AreEqual(expectedLastName, actualLastName);
        }

        [TestMethod()]
        public void SortByCustomerTypeTest()
        {
            //Arrange
            CustomerRepository repository = new CustomerRepository();
            var customerList = repository.Retrieve();

            //Act
            var sortedList = repository.SortByCustomerType(customerList);
            var actual = sortedList.Last().CustomerTypeId;

            //Assert
            Assert.IsNotNull(sortedList);
            Assert.AreEqual(null, actual);
        }
    }
}
