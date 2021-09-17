using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class Product : IComparable<Product>
    {
        private int id;
        private string name;
        private double price;
        private Image image;
        private int stock;
        private DateTime created_at;
        private string description;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }
        public double Price { get => this.price; set => this.price = value; }
        public int Stock { get => this.stock; set => this.stock = value; }
        public DateTime Created_At { get => this.created_at; set => this.created_at = value; }
        public string Description { get => this.description; set => this.description = value; }
        public Image Image { get => this.image; }
        public void setImage(byte[] image)
        {
            MemoryStream m = new MemoryStream(image);
            this.image = Image.FromStream(m);
        }
        #endregion

        public Product(int id, string name, double price, byte[] image, int stock, DateTime created_at, string description)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.stock = stock;
            this.created_at = created_at;
            this.description = description;
            if(image != null)
                setImage(image);
        }

        public override string ToString()
        {
            //return "Product ID: " + id.ToString() + "\nName: " + name + "\nPrice: " + price.ToString() + "\nStock: " + stock.ToString()
            //+ "\nDescription: " + description + "\nCreated at: " + created_at.ToString("f");
            return id.ToString();
        }
        public override bool Equals(object obj)
        {
            Product other = (Product)obj;
            return (this.id.Equals(other.id));
        }
        public override int GetHashCode()
        {
            return id;
        }

        public int CompareTo(Product product)
        {
            if (this.Price > product.price)
                return 1;
            else if (this.Price == product.price)
                return 0;
            else return -1;
        }
    }
}
