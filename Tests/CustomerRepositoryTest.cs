using ComplexStore.model;
using ComplexStore.repository;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class CustomerRepositoryTest
    {
        [Fact]
        public void testGetAllCustomers()
        {
            CustomerRepository repository = new CustomerRepository();
            List<Customer> allCustomers = repository.allCustomers();

            Assert.Equal(new Customer(1, "", "", "", "", "", "", ""), allCustomers[0]);
        }
    }
}
