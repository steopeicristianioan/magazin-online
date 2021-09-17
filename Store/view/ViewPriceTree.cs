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
using FontAwesome.Sharp;

namespace Store.view
{
    class ViewPriceTree : View
    {
        private MainForm mainForm;
        public MainForm MainForm { get => this.mainForm; }
        private PriceTree tree;
        private TreeNode<Product> root;
        private OrderDetailRepository orderDetailRepository;
        private IconButton button;
        public ViewPriceTree(MainForm mainForm, Control parent, 
            OrderDetailRepository orderDetailRepository, Size size)
        {
            button = new IconButton();

            this.mainForm = mainForm;
            this.Parent = parent;
            this.orderDetailRepository = orderDetailRepository;
            this.Size = size;

            tree = new PriceTree();
            root = tree.Tree.Root;

            setHeader();
            setAside();
            setMain();

            int startX = 10;
            loadTree(root, ref startX, 0);
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
            main.AutoScroll = true;
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
            y += 10;
            loadTree(root.Left, ref x, y);
            x += 100;
            int newY = 0;
            for (int i = 10; i < y; i++)
                newY += 16;
            TreeProductCard card = new TreeProductCard(main, root.Data);
            card.Location = new Point(x, newY);
            //card.load();
            //card.Price.Text = root.Data.Price.ToString() + " $";
            loadTree(root.Right, ref x, y);
        }
    }
}
