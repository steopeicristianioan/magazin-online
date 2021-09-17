using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.model;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void testGetAllCustomers()
        {
            CustomerRepository repository = new CustomerRepository();
            List<Customer> allCustomers = repository.AllCustomers;
            Console.WriteLine(allCustomers[0]);
            Assert.AreEqual(new Customer(1, "", "", "", "", "", "", ""), allCustomers[0]);
            Assert.AreEqual(allCustomers.Count, 2);
            Assert.AreEqual(allCustomers[allCustomers.Count-1].ID, 2);
        }
    }
}
