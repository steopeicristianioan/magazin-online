using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class Option
    {
        private int id;
        private string name;
        private int price_increase;
        private int type;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }
        public int Price_Increase { get => this.price_increase; set => this.price_increase = value; }
        public int Type { get => this.type; set => this.type = value; }
        #endregion
        
        public Option(int id, string name, int price_increase, int type)
        {
            this.id = id;
            this.name = name;
            this.price_increase = price_increase;
            this.type = type;
        }

        public override string ToString()
        {
            return "Option ID: " + id.ToString() + "\nName: " + name + "\nprice_increase on price: " + price_increase + " $";
        }
        public override bool Equals(object obj)
        {
            Option other = (Option)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
