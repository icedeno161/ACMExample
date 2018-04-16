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
            Customer foundCustomer = null;

            foundCustomer = customerList.FirstOrDefault(c =>
                                c.CustomerId == customerId);

            return foundCustomer;

        }

        public List<Customer> Retrieve()
        {
            List<Customer> custList = new List<Customer>
                    {new Customer()
                          { CustomerId = 1,
                            FirstName="Frodo",
                            LastName = "Baggins",
                            EmailAddress = "fb@hob.me",
                            CustomerTypeId=1},
                    new Customer()
                          { CustomerId = 2,
                            FirstName="Bilbo",
                            LastName = "Baggins",
                            EmailAddress = "bb@hob.me",
                            CustomerTypeId=null},
                    new Customer()
                          { CustomerId = 3,
                            FirstName="Samwise",
                            LastName = "Gamgee",
                            EmailAddress = "sg@hob.me",
                            CustomerTypeId=1},
                    new Customer()
                          { CustomerId = 4,
                            FirstName="Rosie",
                            LastName = "Cotton",
                            EmailAddress = "rc@hob.me",
                            CustomerTypeId=2}};
            return custList;
        }

        /// <summary>
        /// Returns just the names of the user in a Customer type list.
        /// </summary>
        /// <param name="customers">customer list.</param>
        /// <returns></returns>
        public IEnumerable<string> GetNames(List<Customer> customers) => customers.Select(c => $"{c.LastName}, {c.FirstName}");

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
        public IEnumerable<Customer> SortByName(List<Customer> customerList) => customerList
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
    }
}
