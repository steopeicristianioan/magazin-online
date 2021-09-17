using ComplexStore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    class OrderDetailRepository : Repository
    {
        private List<OrderDetail> allOrderDetails;
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
            return new OrderDetail(-1, 0, 0, 0, "");
        }
        public void add(int product_id, int order_id, double price, string selected_options)
        {
            string sql = "insert into order_details(product_id,order_id,price,selected_options) " +
                "values (@product_id,@order_id,@price,@selected_options)";
            db.SaveData(sql, new { product_id, order_id, price, selected_options }, connection);
        }
    }
}
