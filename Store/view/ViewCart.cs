using Store.repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Store.model;

namespace Store.view
{
    class ViewCart : View
    {
        private MainForm mainForm;
        public MainForm MainForm { get => this.mainForm; set => this.mainForm = value; }
        private int customerID;
        public int CustomerID { get => this.customerID; set => this.customerID = value; }
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;
        private ProductRepository productRepository;
        private List<OrderDetail> cart;
        private Order lastOrder;

        private IconButton back;
        private FlowLayoutPanel details;

        public ViewCart(int customerID, OrderRepository orderRepository, 
            OrderDetailRepository orderDetailRepository, Size size,
            ProductRepository productRepository) : base()
        {
            this.Size = size;
            this.customerID = customerID;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;

            setMain();
            setHeader();
            setAside();

            loadMain();
        }

        protected override void setHeader()
        {
            header.Size = new Size(this.Width, 75);
            header.Location = new Point(0, 0);
            this.header.BackColor = main.BackColor;
        }
        protected override void setAside()
        {
            aside.Size = new Size(350, this.Height - 75);
            aside.Location = new Point(this.Width - 350, 75);
            this.aside.BackColor = main.BackColor;
            aside.BorderStyle = BorderStyle.FixedSingle;
        }
        protected override void setMain()
        {
            main.Size = new Size(this.Width - 350, this.Height - 75);
            main.Location = new Point(0, 75);
            this.main.BackColor = ColorTranslator.FromHtml("#164A41");
        }

        private void loadMain()
        {
            setDetailsPanel();
            loadDetailsPanel();
            loadBack();
        }
        private void setDetailsPanel()
        {
            details = new FlowLayoutPanel();
            details.Parent = this.main;
            details.Location = new Point(0, 75);
            details.Size = new Size(main.Width, main.Height);
            details.AutoScroll = true;
            details.Padding = new Padding(50, 0, 0, 0);
        }
        private void loadDetailsPanel()
        {
            lastOrder = orderRepository.getlastUnfinishedOrder(customerID);
            if (lastOrder != null)
            {
                cart = orderDetailRepository.getCart(lastOrder.ID);
                foreach(OrderDetail detail in cart)
                {
                    OrderDetailBox box = new OrderDetailBox(detail, orderDetailRepository,
                        new Size(details.Width - 100, 100), productRepository, orderRepository, details, this);
                    box.Load();
                }
            }
            else
            {
                IconPictureBox box = new IconPictureBox();
                box.Parent = details;
                box.Margin = new Padding((details.Width - 300) / 2, 0, 0, 0);
                box.Size = new Size(300, 300);
                box.IconChar = IconChar.ShoppingBasket;
                box.SizeMode = PictureBoxSizeMode.StretchImage;
                box.IconColor = ColorTranslator.FromHtml("#F1B24A");

                Label message = new Label();
                message.Parent = details;
                message.Margin = new Padding((details.Width - 650) / 2, -10, 0, 0);
                message.Size = new Size(650, 50);
                message.Location = new Point(details.Padding.Left, 310);
                message.Text = "Your cart is empty, go for some shopping!";
                message.Font = new Font("Segoe UI", 15, FontStyle.Bold);
                message.TextAlign = ContentAlignment.MiddleCenter;
                message.ForeColor = box.IconColor;

                IconButton button = new IconButton();
                button.Parent = details;
                button.Margin = new Padding((details.Width - 350) / 2, 50, 0, 0);
                button.Size = new Size(350, 50);
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderColor = button.ForeColor = ColorTranslator.FromHtml("#9DC88D");
                button.IconColor = ColorTranslator.FromHtml("#F1B24A");
                button.Text = "Take me to the home page";
                button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                button.IconChar = IconChar.Gifts;
                button.TextImageRelation = TextImageRelation.ImageBeforeText;
            }
        }
        public override void Refresh()
        {
            details.Controls.Clear();
            orderDetailRepository.getAllOrderDetails();
            orderRepository.getAllOrders();
            loadDetailsPanel();
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
            mainForm.closeChild(mainForm);
        }
    }
}
