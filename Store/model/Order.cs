using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class Order
    {
        private int id;
        private int customer_id;
        private int ammount;
        private DateTime created_at;
        private bool delivered;
        private double price;
        private bool sent;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public int Customer_ID { get => this.customer_id; set => this.customer_id = value; }
        public int Ammount { get => this.ammount; set => this.ammount = value; }
        public DateTime Created_At { get => this.created_at; set => this.created_at = value; }
        public bool Delivered { get => this.delivered; set => this.delivered = value; }
        public double Price { get => this.price; set => this.price = value; }
        public bool Sent { get => this.sent; set => this.sent = value; }
        #endregion

        public Order(int id, int customer_id, int ammount, DateTime created_at, bool delivered, double price, bool sent)
        {
            this.id = id;
            this.customer_id = customer_id;
            this.ammount = ammount;
            this.created_at = created_at;
            this.delivered = delivered;
            this.price = price;
            this.sent = sent;
        }

        public override string ToString()
        {
            return "Order ID: " + id.ToString() + "\nCustomer ID: " + customer_id.ToString() + "\nPrice: " + price.ToString() + "\nAmmount : "
            + ammount.ToString() + "\nDelivered: " + delivered.ToString() + "\nDate: " + created_at.ToString("f");
        }
        public override bool Equals(object obj)
        {
            Order other = (Order)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
