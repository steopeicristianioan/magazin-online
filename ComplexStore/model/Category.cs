using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.model
{
    class Category
    {
        private int id;
        private string name;

        //setters && getters
        #region
        public int ID { get => this.id; set => this.id = value; }
        public string Name { get => this.name; set => this.name = value; }
        #endregion

        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return "Category ID: " + id.ToString() + "\nName: " + name;
        }
        public override bool Equals(object obj)
        {
            Category other = (Category)obj;
            return id == other.id;
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
