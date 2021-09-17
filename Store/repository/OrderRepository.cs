using Store.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.repository
{
    public class OrderRepository : Repository
    {
        private List<Order> allOrders;
        public List<Order> AllOrders { get => this.allOrders; }
        public int LastID;
        public OrderRepository() 
        {
            allOrders = getAllOrders();
            LastID = allOrders[allOrders.Count - 1].ID;
        }

        public List<Order> getAllOrders()
        {
            string sql = "select * from orders";
            allOrders = db.LoadData<Order, dynamic>(sql, new { }, connection);
            LastID = allOrders[allOrders.Count - 1].ID;
            return allOrders;
        }
        public Order getlastUnfinishedOrder(int customer_id)
        {
            foreach (Order order in allOrders)
                if (order.Customer_ID == customer_id && order.Sent == false)
                    return order;
            return null;
        }
        public List<Order> getOrdersByClinetID(int id)
        {
            List<Order> res = new List<Order>();
            foreach (Order order in allOrders)
                if (order.Customer_ID == id)
                    res.Add(order);
            return res;
        }
        public void addOrder(int customer_id)
        {
            int amm = 1;
            bool st1 = false, st2 = false;
            DateTime time = DateTime.Now;
            double price = 0;
            string sql = "insert into orders(customer_id,ammount,created_at,delivered,price,sent)" +
                "values(@customer_id,@amm,@time,@st1,@price,@st2)";
            db.SaveData(sql, new {customer_id, amm, time, st1, price, st2}, connection);
        }
        public void modifyAmmount(int id, int dif)
        {
            string sql = "update orders set ammount = ammount + @dif where id = @id";
            db.SaveData(sql, new { id, dif}, connection);
        }
        public void modifyPrice(int id, double newPrice)
        {
            string sql = "update orders set price = @newPrice where id = @id";
            db.SaveData(sql, new { id, newPrice }, connection);
        }
        public Order getById(int id)
        {
            foreach (Order order in allOrders)
                if (order.ID == id)
                    return order;
            return null;
        }
        public void deleteOrder(int id)
        {
            string sql = "delete from orders where id = @id";
            db.SaveData(sql, new { id }, connection);
        }
    }
}
