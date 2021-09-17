using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class OrderDetail
    {
        private int id;
        private int product_id;
        private int order_id;
        private double price;
        private string selected_options;
        private int quantity;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public int Product_ID { get => this.product_id; set => this.product_id = value; }
        public int Order_ID { get => this.order_id; set => this.order_id = value; }
        public double Price { get => this.price; set => this.price = value; }
        public string Selected_Options { get => this.selected_options; set => this.selected_options = value; }
        public int Quantity { get => this.quantity; set => this.quantity = value; }
        #endregion

        public OrderDetail(int id, int product_id, int order_id, double price, string selected_options, int quantity)
        {
            this.id = id;
            this.product_id = product_id;
            this.order_id = order_id;
            this.price = price;
            this.selected_options = selected_options;
            this.quantity = quantity;
        }

        public override string ToString()
        {
            string[] words = selected_options.Split(',');
            string options = string.Empty;
            foreach (string optionID in words)
                options += "\n\tOption #" + optionID;
            return "Order detail ID: " + id.ToString() + "\nProduct ID: " + product_id.ToString() + "\nOrder ID: " + order_id.ToString()
            + "\nPrice: " + price.ToString() + "\nOptions: " + options;
        }
        public override bool Equals(object obj)
        {
            OrderDetail other = (OrderDetail)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
