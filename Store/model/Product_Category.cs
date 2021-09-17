using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class Product_Category
    {
        private int id;
        private int category_id;
        private int product_id;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public int Category_ID { get => this.category_id; set => this.category_id = value; }
        public int Product_ID { get => this.product_id; set => this.product_id = value; }
        #endregion

        public Product_Category(int id, int category_id, int product_id)
        {
            this.id = id;
            this.category_id = category_id;
            this.product_id = product_id;
        }

        public override string ToString()
        {
            return "Product category ID: " + id.ToString() + "\nCategory ID: " + category_id.ToString() + "\nProduct ID: " + product_id.ToString();
        }
        public override bool Equals(object obj)
        {
            Product_Category other = (Product_Category)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
