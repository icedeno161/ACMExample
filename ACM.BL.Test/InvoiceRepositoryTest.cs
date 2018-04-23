using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.BL.Test
{
    [TestClass()]
    public class InvoiceRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void CalculateTotalAmountInvoicedTest()
        {
            //Arrange
            InvoiceRepository invoiceRepository = new InvoiceRepository();
            var invoiceList = invoiceRepository.Retrieve();

            //Act
            var totalAmmountInvoiced = invoiceRepository.CalculateTotalAmountInvoiced(invoiceList);

            //Analyze
            TestContext.WriteLine($"Total Amount Invoiced: {totalAmmountInvoiced:C}");

            //Assert
            Assert.IsNotNull(invoiceList);
        }

        [TestMethod()]
        public void CalculateTotalUnitsSoldTest()
        {
            //Arrange
            InvoiceRepository invoiceRepository = new InvoiceRepository();
            var invoiceList = invoiceRepository.Retrieve();

            //Act
            var totalAmountOfUnitsSold = invoiceRepository.CalculateTotalUnitsSold(invoiceList);

            //Analyze
            TestContext.WriteLine($"Total number of units sold: {totalAmountOfUnitsSold}");

            //Assert
            Assert.IsNotNull(invoiceList);
        }

        [TestMethod()]
        public void GetInvoiceTotalByIsPaidTest()
        {
            //Arrange
            InvoiceRepository invoiceRepository = new InvoiceRepository();
            var invoiceList = invoiceRepository.Retrieve();

            //Act
            var TotalByIsPaidList = invoiceRepository.GetInvoiceTotalByIsPaid(invoiceList);

            // Assert
            Assert.IsNotNull(invoiceList);
        }

        [TestMethod()]
        public void GetInvoiceTotalByIsPaidAndMonthTest()
        {
            //Arrange
            InvoiceRepository invoiceRepository = new InvoiceRepository();
            var invoiceList = invoiceRepository.Retrieve();

            //Act
            var TotalByIsPaidList = invoiceRepository.GetInvoiceTotalByIsPaidAndMonth(invoiceList);

            // Assert
            Assert.IsNotNull(invoiceList);
        }
    }
}