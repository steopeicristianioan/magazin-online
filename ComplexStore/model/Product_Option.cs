using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.model
{
    class Product_Option
    {
        private int id;
        private int option_id;
        private int product_id;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public int Option_ID { get => this.option_id; set => this.option_id = value; }
        public int Product_ID { get => this.product_id; set => this.product_id = value; }
        #endregion

        public Product_Option(int id, int option_id, int product_id)
        {
            this.id = id;
            this.option_id = option_id;
            this.product_id = product_id;
        }

        public override string ToString()
        {
            return "Product option ID: " + id.ToString() + "\nOption ID: " + option_id.ToString() + "\nProduct ID: " + product_id.ToString();
        }
        public override bool Equals(object obj)
        {
            Product_Option other = (Product_Option)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
