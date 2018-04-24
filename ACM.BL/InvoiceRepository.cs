using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class InvoiceRepository
    {
        /// <summary>
        /// Retrieves the list of invoices.
        /// </summary>
        /// <returns></returns>
        public List<Invoice> Retrieve()
        {
            List<Invoice> invoiceList = new List<Invoice>
                    {new Invoice()
                          { InvoiceId = 1,
                            CustomerId = 1,
                            InvoiceDate=new DateTime(2013, 6, 20),
                            DueDate=new DateTime(2013, 8,29),
                            IsPaid=null,
                            Amount=199.99M,
                            NumberOfUnits=20,
                            DiscountPercent=0M},
                    new Invoice()
                          { InvoiceId = 2,
                            CustomerId = 1,
                            InvoiceDate=new DateTime(2013, 7, 20),
                            DueDate=new DateTime(2013, 8,20),
                            IsPaid=null,
                            Amount=98.50M,
                            NumberOfUnits=10,
                            DiscountPercent=10M},
                    new Invoice()
                          { InvoiceId = 3,
                            CustomerId = 2,
                            InvoiceDate=new DateTime(2013, 7, 25),
                            DueDate=new DateTime(2013, 8,25),
                            IsPaid=null,
                            Amount=250M,
                            NumberOfUnits=25,
                            DiscountPercent=10M},
                    new Invoice()
                          { InvoiceId = 4,
                            CustomerId = 3,
                            InvoiceDate=new DateTime(2013, 7, 1),
                            DueDate=new DateTime(2013, 9,1),
                            IsPaid=true,
                            Amount=20M,
                            NumberOfUnits=2,
                            DiscountPercent=15M},
                    new Invoice()
                          { InvoiceId = 5,
                            CustomerId = 1,
                            InvoiceDate=new DateTime(2013, 8, 20),
                            DueDate=new DateTime(2013, 9,29),
                            IsPaid=true,
                            Amount=225M,
                            NumberOfUnits=22,
                            DiscountPercent=10M},
                    new Invoice()
                          { InvoiceId = 6,
                            CustomerId = 2,
                            InvoiceDate=new DateTime(2013, 8, 20),
                            DueDate=new DateTime(2013, 8,20),
                            IsPaid=false,
                            Amount=75M,
                            NumberOfUnits=8,
                            DiscountPercent=0M},
                    new Invoice()
                          { InvoiceId = 7,
                            CustomerId = 3,
                            InvoiceDate=new DateTime(2013,8, 25),
                            DueDate=new DateTime(2013, 9,25),
                            IsPaid=null,
                            Amount=500M,
                            NumberOfUnits=42,
                            DiscountPercent=10M},
                    new Invoice()
                          { InvoiceId = 8,
                            CustomerId = 4,
                            InvoiceDate=new DateTime(2013, 8, 1),
                            DueDate=new DateTime(2013, 9,1),
                            IsPaid=true,
                            Amount=75M,
                            NumberOfUnits=7,
                            DiscountPercent=0M}};

            return invoiceList;
        }

        /// <summary>
        /// Returns the list of invoices assigned to cutomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Invoice> Retrieve(int customerId)
        {
            var invoiceList = this.Retrieve();
            var filteredList = invoiceList.Where(i => i.CustomerId == customerId).ToList();

            return filteredList;
        }

        /// <summary>
        /// Returns the total amount of all invoices in the invoice list.
        /// </summary>
        /// <param name="invoiceList"></param>
        /// <returns></returns>
        public decimal CalculateTotalAmountInvoiced(List<Invoice> invoiceList) => invoiceList.Sum(i => i.TotalAmount);

        /// <summary>
        /// Calculates the total number of units sold in the invoice list.
        /// </summary>
        /// <param name="invoiceList"></param>
        /// <returns></returns>
        public int CalculateTotalUnitsSold(List<Invoice> invoiceList) => invoiceList.Sum(i => i.NumberOfUnits);

        /// <summary>
        /// returns the total invoice amounts grouped by Paid and unpaid.
        /// </summary>
        /// <param name="invoiceList"></param>
        /// <returns></returns>
        public dynamic GetInvoiceTotalByIsPaid(List<Invoice> invoiceList)
        {
            var query = invoiceList.GroupBy(i => i.IsPaid ?? false,
                                            i => i.TotalAmount,
                                            (groupKey, invTotal) => new
                                            {
                                                Key = groupKey,
                                                InvoicedAmount = invTotal.Sum()
                                            });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Key} : {item.InvoicedAmount:C}");
            }

            return query;
        }

        /// <summary>
        /// Returns invoice total that is due grouped by paid/unpaid and month.
        /// </summary>
        /// <param name="invoiceList"></param>
        /// <returns></returns>
        public dynamic GetInvoiceTotalByIsPaidAndMonth(List<Invoice> invoiceList)
        {
            var query = invoiceList.GroupBy(i => new
            {
                IsPaid = i.IsPaid ?? false,
                InvoiceMonth = i.InvoiceDate.ToString("MMMM")
            },
                                            i => i.TotalAmount,
                                            (groupKey, invTotal) => new
                                            {
                                                Key = groupKey,
                                                InvoicedAmount = invTotal.Sum()
                                            });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Key.IsPaid} / {item.Key.InvoiceMonth} : {item.InvoicedAmount:C}");
            }

            return query;
        }

        /// <summary>
        /// Calculate the average discount in invoices.
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        public decimal CalculateMean(List<Invoice> invoices) => invoices.Average(i => i.DiscountPercent);

        /// <summary>
        /// Calculate the median of Discount Percantage.
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        public decimal CalculateMedian(List<Invoice> invoices)
        {
            var query = invoices.OrderBy(i => i.DiscountPercent);
            var midpoint = query.Count() / 2;
            decimal median;

            if (IsEven(midpoint))
            {

                median = (query.ElementAt(midpoint).DiscountPercent + query.ElementAt(midpoint - 1).DiscountPercent) / 2;

                return median;
            }

            median = query.ElementAt(midpoint).DiscountPercent;

            return median;
        }

        /// <summary>
        /// Calculates the mode of Discount Percentage.
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        public decimal CalculateMode(List<Invoice> invoices) => invoices.GroupBy(inv => inv.DiscountPercent)
                                                                        .OrderByDescending(group => group.Count())
                                                                        .Select(group => group.Key)
                                                                        .FirstOrDefault();

        /// <summary>
        /// Returns whether an integer is even or not.
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        private static bool IsEven(int integer)
        {
            return integer % 2 == 0;
        }
    }
}
