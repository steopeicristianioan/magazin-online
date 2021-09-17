using ComplexStore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    class OrderRepository : Repository
    {
        public OrderRepository() { }

        public List<Order> allOrders()
        {
            string sql = "select * from orders";
            return db.LoadData<Order, dynamic>(sql, new { }, connection);
        }
        public Order getCart(int customerID)
        {
            string sql = "select * from where customer_id = @customerID and sent = 0";
            List<Order> temp = db.LoadData<Order, dynamic>(sql, new { }, connection);
            if (temp == null)
                return null;
            return temp[0];
        }
    }
}
