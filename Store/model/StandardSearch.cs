using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class StandardSearch
    {
        private double minPirce;
        private double maxPrice;

        public double MinPrice { get => this.minPirce; set => this.minPirce = value; }
        public double MaxPrice { get => this.maxPrice; set => this.maxPrice = value; }


        public StandardSearch(double min, double max)
        {
            minPirce = min;
            maxPrice = max;
        }


    }
}
