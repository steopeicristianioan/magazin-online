using FontAwesome.Sharp;
using Store.model;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.view
{
    class ViewProductsByName : View
    {
        private MainForm mainForm;
        public MainForm MainForm { get => this.mainForm; }
        private List<Product> products;
        private OrderDetailRepository orderDetailRepository;
        private OrderRepository orderRepository;
        private ViewProduct view;

        public Panel before;

        private IconButton back;

        public ViewProductsByName(MainForm mainForm, List<Product> products, Size size,
            OrderDetailRepository orderDetailRepository, OrderRepository orderRepository,
            ViewProduct view)
        {
            this.products = products;
            this.mainForm = mainForm;
            this.Size = size;
            this.orderDetailRepository = orderDetailRepository;
            this.orderRepository = orderRepository;
            this.view = view;

            setMain();
            setAside();
            setHeader();
        }
        public void load()
        {
            loadBack();
            loadProducts();
        }
        protected override void setHeader()
        {
            header.Size = new Size(0,0);
            header.Location = new Point(0, 0);
            this.header.BackColor = main.BackColor;
        }
        protected override void setAside()
        {
            aside.Size = new Size(0,0);
            aside.Location = new Point(0, 0);
            this.aside.BackColor = main.BackColor;
        }
        protected override void setMain()
        {
            main.Size = new Size(this.Width, this.Height);
            main.Location = new Point(0,0);
            this.main.BackColor = ColorTranslator.FromHtml("#164A41");
            //main.AutoScroll = true;

        }

        private void loadBack()
        {
            back = new IconButton();
            back.Parent = main;
            back.Size = new Size(50, 50);
            back.IconChar = IconChar.AngleLeft;
            back.IconColor = ColorTranslator.FromHtml("#F1B24A");
            back.BackColor = Color.Transparent;
            back.FlatStyle = FlatStyle.Flat;
            back.FlatAppearance.BorderSize = 0;
            back.FlatAppearance.MouseDownBackColor = back.FlatAppearance.MouseOverBackColor = Color.Transparent;
            back.TabStop = false;
            back.FlatAppearance.BorderColor = main.BackColor;

            back.MouseHover += new EventHandler(this.back_Hover);
            back.MouseLeave += new EventHandler(this.back_Leave);
            back.Click += new EventHandler(this.back_Click);
        }
        private void back_Hover(object sender, EventArgs e)
        {
            back.IconColor = Color.White;
        }
        private void back_Leave(object sender, EventArgs e)
        {
            back.IconColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
            this.view.Main.Visible = true;
        }

        private void loadProducts()
        {
            int x = 50, y = 50, ct = 0;
            bool flag = false;
            foreach(Product product in products)
            {
                ct++;
                ProductCard card = new ProductCard(true, mainForm, orderDetailRepository, main, product,
                    new Size(250, 250));
                card.OrderRepository = orderRepository;
                card.Customer_ID = 1;
                card.setLocation(x, y);
                card.load();
                card.Price.Text = product.Price + " $";
                x += 255;
                if(ct % 3 == 0)
                {
                    x = 50;
                    y += 255;
                }
                if (y > main.Height)
                    main.AutoScroll = true; flag = true ;
            }
            if (flag)
            {
                Label dummy = new Label();
                dummy.Size = new Size(1, 50);
                dummy.Parent = main;
                dummy.Location = new Point(x, y);
            }
        }
    }
}
