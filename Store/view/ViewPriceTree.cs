using Store.model;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using generics.NETFramework;

namespace Store.view
{
    class ViewPriceTree : View
    {
        private MainForm mainForm;
        public MainForm MainForm { get => this.mainForm; }
        private PriceTree tree;
        private TreeNode<Product> root;
        private OrderDetailRepository orderDetailRepository;
        public ViewPriceTree(MainForm mainForm, Control parent, 
            OrderDetailRepository orderDetailRepository, Size size)
        {
            this.mainForm = mainForm;
            this.Parent = parent;
            this.orderDetailRepository = orderDetailRepository;
            this.Size = size;

            tree = new PriceTree();
            root = tree.Tree.Root;

            setHeader();
            setAside();
            setMain();
        }

        protected override void setHeader()
        {
            header.Size = new Size(0, 0);
            header.Location = new Point(0, 0);
        }
        protected override void setMain()
        {
            main.Size = this.Size;
            main.Location = new Point(0, 0);
            main.BackColor = Color.Red;
        }
        protected override void setAside()
        {
            aside.Size = new Size(0, 0);
            aside.Location = new Point(0, 0);
        }

        private void loadTree(TreeNode<Product> root, ref int x, int y)
        {
            if (root == null)
                return;
            x += 5;
            loadTree(root.Right, ref x, y);
            for (int i = 5; i < x; i++)
            {
                y += 10;
                ProductCard card = new ProductCard(false, mainForm, orderDetailRepository,
                    Main, root.Data, new Size(50, 50));
                card.Location = new Point(x, y);
                card.load();
                card.Price.Text = root.Data.Price.ToString() + " $";
            }
            loadTree(root.Left, ref x, y);
        }
    }
}
