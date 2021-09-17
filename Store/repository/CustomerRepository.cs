using Store.model;
using DataAccessLibrary.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class CustomerRepository : Repository
    {
        private List<Customer> allCustomers;
        public List<Customer> AllCustomers { get => this.allCustomers; }
        public CustomerRepository() 
        {
            allCustomers = getAllCustomers();
        }

        public List<Customer> getAllCustomers()
        {
            string sqlCommand = "select * from customers";
            return db.LoadData<Customer, dynamic>(sqlCommand, new { }, connection);
        }
        public Customer getByName(string firstName, string lastName)
        {
            foreach (Customer customer in allCustomers)
                if (customer.First_Name == firstName && customer.Last_Name == lastName)
                    return customer;
            return null;
        }
    }
}
