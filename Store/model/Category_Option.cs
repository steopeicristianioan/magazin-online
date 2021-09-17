using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class Category_Option
    {
        private int id;
        private int category_id;
        private int option_id;

        //getters && setters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public int Category_ID { get => this.category_id; set => this.category_id = value; }
        public int Option_ID { get => this.option_id; set => this.option_id = value; }
        #endregion

        public Category_Option(int id, int category_id, int option_id)
        {
            this.id = id;
            this.category_id = category_id;
            this.option_id = option_id;
        }

        public override string ToString()
        {
            return "ID: " + id.ToString() + "\nCategory ID: " + category_id.ToString() +
                "\nOption ID: " + option_id.ToString();
        }
        public override bool Equals(object obj)
        {
            Category_Option other = (Category_Option)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
