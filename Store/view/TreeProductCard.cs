using Store.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.view
{
    class TreeProductCard : Panel
    {
        private Product product;

        public TreeProductCard(Control parent, Product product)
        {
            this.Parent = parent;
            this.product = product;

            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(150, 150);
        }
    }
}
