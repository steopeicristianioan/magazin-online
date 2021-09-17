using ComplexStore.model;
using DataAccessLibrary.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    public class CustomerRepository : Repository
    {
        public CustomerRepository() { }

        public List<Customer> allCustomers()
        {
            string sqlCommand = "select * from customers";
            return db.LoadData<Customer, dynamic>(sqlCommand, new { }, connection);
        }
    }
}
