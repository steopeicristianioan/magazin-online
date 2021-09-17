using Store.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class OrderDetailRepository : Repository
    {
        private List<OrderDetail> allOrderDetails;
        public List<OrderDetail> AllOrderDetails { get => this.allOrderDetails; }
        public int LastID;
        public OrderDetailRepository() 
        {
            getAllOrderDetails();
        }

        public void getAllOrderDetails()
        {
            string sqlScript = "select * from order_details";
            allOrderDetails = db.LoadData<OrderDetail, dynamic>(sqlScript, new { }, connection);
            LastID = allOrderDetails[allOrderDetails.Count - 1].ID;
        }
        public OrderDetail getById(int id)
        {
            foreach (OrderDetail detail in allOrderDetails)
                if (detail.ID == id)
                    return detail;
            return new OrderDetail(-1, 0, 0, 0, "", 0);
        }
        public void add(int product_id, int order_id, double price, string selected_options, int quantity)
        {
            string sql = "insert into order_details(product_id,order_id,price,selected_options,quantity) " +
                "values (@product_id,@order_id,@price,@selected_options,@quantity)";
            db.SaveData(sql, new { product_id, order_id, price, selected_options, quantity }, connection);
        }
        public void modifyPrice(int id, double newPrice)
        {
            string sql = "update order_details set price = @newPrice where id = @id";
            db.SaveData(sql, new { id, newPrice }, connection);
        }
        public void modifyOptions(int id, string options)
        {
            string sql = "update order_details set selected_options = @options where id = @id";
            db.SaveData(sql, new { id, options }, connection);
        }
        public void deleteOrderDetail(int id)
        {
            string sql = "delete from order_details where id = @id";
            db.SaveData(sql, new { id }, connection);
        }
        public void modifyQuantity(int id, int quantity)
        {
            string sql = "update order_details set quantity = @quantity where id = @id";
            db.SaveData(sql, new { id, quantity }, connection);
        }
        public List<OrderDetail> getCart(int orderID)
        {
            List<OrderDetail> cart = new List<OrderDetail>();
            foreach (OrderDetail detail in allOrderDetails)
                if (detail.Order_ID == orderID)
                    cart.Add(detail);
            return cart;
        }
    }
}
