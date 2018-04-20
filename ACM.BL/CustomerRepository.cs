using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class CustomerRepository
    {
        public Customer Find(List<Customer> customerList, int customerId)
        {
            if (customerList == null) { throw new ArgumentNullException(nameof(customerList), "customerList passed to Find() is null."); }
            if (customerId < 0) { throw new ArgumentException(nameof(customerId), "customerId passed to Find() is not valid"); }

            Customer foundCustomer = null;

            foundCustomer = customerList.FirstOrDefault(c =>
                                c.CustomerId == customerId);

            return foundCustomer;

        }

        public List<Customer> Retrieve()
        {
            InvoiceRepository invoiceRepository = new InvoiceRepository();

            List<Customer> custList = new List<Customer>
                    {new Customer()
                          { CustomerId = 1,
                            FirstName="Frodo",
                            LastName = "Baggins",
                            EmailAddress = "fb@hob.me",
                            CustomerTypeId=1,
                            InvoiceList = invoiceRepository.Retrieve(1)
                    },
                    new Customer()
                    {
                        CustomerId = 2,
                        FirstName = "Bilbo",
                        LastName = "Baggins",
                        EmailAddress = "bb@hob.me",
                        CustomerTypeId = null,
                        InvoiceList = invoiceRepository.Retrieve(2)
                    },
                    new Customer()
                    {
                        CustomerId = 3,
                        FirstName = "Samwise",
                        LastName = "Gamgee",
                        EmailAddress = "sg@hob.me",
                        CustomerTypeId = 1,
                        InvoiceList = invoiceRepository.Retrieve(3)
                    },
                    new Customer()
                    {
                        CustomerId = 4,
                        FirstName = "Rosie",
                        LastName = "Cotton",
                        EmailAddress = "rc@hob.me",
                        CustomerTypeId = 2,
                        InvoiceList = invoiceRepository.Retrieve(4)
                    }};
            return custList;
        }

        /// <summary>
        /// returns a list of Customer Names and their customerId number
        /// </summary>
        /// <param name="customerList">list of customers</param>
        /// <returns></returns>
        public dynamic GetNamesAndId(List<Customer> customerList) => customerList
                                                                        .Select(c => new
                                                                        {
                                                                            Name = $"{c.LastName}, {c.FirstName}",
                                                                            c.CustomerId
                                                                        }).ToList();

        /// <summary>
        /// Returns just the names of the user in a Customer type list.
        /// </summary>
        /// <param name="customers">customer list.</param>
        /// <returns></returns>
        public IEnumerable<string> GetNames(List<Customer> customers) => customers.Select(c => $"{c.LastName}, {c.FirstName}");

        /// <summary>
        /// Returns a list of anonymous types.
        /// </summary>
        /// <param name="customers">list of customers.</param>
        /// <returns></returns>
        public dynamic GetNamesAndEmail(List<Customer> customers)
        {
            var query = customers.Select(c => new
            {
                FullName = $"{c.LastName}, {c.FirstName}",
                c.EmailAddress
            });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.FullName} : {item.EmailAddress}");
            }

            return query;
        }

        /// <summary>
        /// Retrieves and empty list of Customer type.
        /// </summary>
        /// <returns></returns>
        public List<Customer> RetrieveEmptyList() => Enumerable.Repeat(new Customer(), 5).ToList();

        /// <summary>
        /// Function will sort a list of type Customer by LastName and then FirstName.
        /// </summary>
        /// <param name="customerList"> list to sort.</param>
        /// <returns>sorted listed by name.</returns>
        public IEnumerable<Customer> SortByName(IEnumerable<Customer> customerList) => customerList
                                                                                .OrderBy(c => c.LastName)
                                                                                .ThenBy(c => c.FirstName);
        /// <summary>
        /// Sort a list of type Customer by Last Name and First Name and then Reverse.
        /// </summary>
        /// <param name="customerList">List to sort</param>
        /// <returns>Sorted List.</returns>
        public IEnumerable<Customer> SortByNameInReverse(List<Customer> customerList) => SortByName(customerList)
                                                                                        .Reverse();

        /// <summary>
        /// Sort a list of Customer type by CustomerTypeId
        /// </summary>
        /// <param name="customerList">list to sort</param>
        /// <returns>sorted list.</returns>
        public IEnumerable<Customer> SortByCustomerType(List<Customer> customerList) => customerList
                                                                                        .OrderByDescending(c => c.CustomerTypeId.HasValue)
                                                                                        .ThenBy(c => c.CustomerTypeId);

        /// <summary>
        /// returns list of customer names and customerTypes.
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="customerTypes"></param>
        /// <returns></returns>
        public dynamic GetNamesAndType(List<Customer> customers, List<CustomerType> customerTypes)
        {
            var query = customers.Join(customerTypes,
                                    c => c.CustomerTypeId,
                                    ct => ct.CustomerTypeId,
                                    (c, ct) => new
                                    {
                                        Name = $"{c.LastName}, {c.FirstName}",
                                        CustomerTypeName = ct.TypeName
                                    });

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Name} : {item.CustomerTypeName}");
            }

            return query.ToList();
        }

        /// <summary>
        /// Returns a list of customers with overdue invoices.
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public IEnumerable<Customer> GetOverdueCustomers(List<Customer> customers)
        {
            var query = customers
                        .SelectMany(c => c.InvoiceList
                                        .Where(i => i.IsPaid ?? false == false),
                                        (c, i) => c).Distinct();

            return query;
        }
    }
}
