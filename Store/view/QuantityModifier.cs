using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.model;
using Store.repository;
using FontAwesome.Sharp;
using System.Drawing;

namespace Store.view
{
    class QuantityModifier : Panel
    {
        private OrderDetailBox box;
        public OrderDetailBox Box { get => this.box; set => this.box = value; }
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;
        private int orderID;
        private int orderDetailID;
        public int s;
        private OrderDetail detail;
        public OrderDetail Detail { get => this.detail; set => this.detail = value; }

        private IconButton plus;
        private IconButton minus;
        private Label current;
        public Label Current { get => this.current; set => this.current = value; }

        public QuantityModifier(Control parent, Size size, int orderID, int orderDetailID,
            OrderDetailRepository orderDetailRepository, OrderRepository orderRepository, OrderDetailBox box)
        {
            this.Parent = parent;
            this.Size = size;
            this.orderID = orderID;
            this.orderDetailID = orderDetailID;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.box = box;

            //this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void Load()
        {
            loadPlusMinus();
            loadCurrent();
        }

        private void loadPlusMinus()
        {
            plus = new IconButton();
            minus = new IconButton();

            plus.Parent = minus.Parent = this;
            plus.FlatStyle = minus.FlatStyle = FlatStyle.Flat;
            plus.FlatAppearance.BorderSize = minus.FlatAppearance.BorderSize = 0;
            plus.Size = minus.Size = new Size(25, 25);
            plus.IconColor = minus.IconColor = ColorTranslator.FromHtml("#F1B24A");
            plus.IconSize = minus.IconSize = 20;
            plus.FlatAppearance.MouseDownBackColor = plus.FlatAppearance.MouseOverBackColor =
                minus.FlatAppearance.MouseDownBackColor = minus.FlatAppearance.MouseOverBackColor =
                Color.Transparent;
            plus.TabStop = minus.TabStop = false;

            plus.IconChar = IconChar.Plus;
            minus.IconChar = IconChar.Minus;

            plus.Location = new Point(this.Width - 25, (this.Height - 25) / 2);
            minus.Location = new Point(0, (this.Height - 25) / 2);

            plus.Click += new EventHandler(plus_Click);
            minus.Click += new EventHandler(this.minus_Click);

            plus.MouseHover += new EventHandler(this.button_Hover);
            minus.MouseHover += new EventHandler(this.button_Hover);

            plus.MouseLeave += new EventHandler(this.button_Leave);
            minus.MouseLeave += new EventHandler(this.button_Leave);
        }
        private void loadCurrent()
        {
            current = new Label();
            current.Parent = this;
            current.Size = new Size(this.Width - 50, this.Height - 5);
            current.Location = new Point(25, 0);
            current.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            current.TextAlign = ContentAlignment.MiddleCenter;
            current.Text = detail.Quantity.ToString();
            current.ForeColor = plus.IconColor;
        }

        private void minus_Click(object sender, EventArgs e)
        {
            orderDetailRepository.getAllOrderDetails();
            orderRepository.getAllOrders();

            if(int.Parse(current.Text) > 1)
            {
                double difference = detail.Price / detail.Quantity;
                orderDetailRepository.modifyPrice(orderDetailID, detail.Price - difference);
                orderDetailRepository.modifyQuantity(orderDetailID, detail.Quantity - 1);
                orderRepository.modifyAmmount(orderID, -1);
                orderRepository.modifyPrice(orderID, orderRepository.getById(orderID).Price - difference);

                orderDetailRepository.getAllOrderDetails();
                orderRepository.getAllOrders();

                current.Text = (int.Parse(current.Text) - 1).ToString();
            }
            else if (s == 1)
            {
                double price = detail.Price;
                Order order = orderRepository.getById(orderID);
                orderDetailRepository.deleteOrderDetail(detail.ID);
                orderDetailRepository.getAllOrderDetails();
                if(order.Ammount == 1)
                {
                    orderRepository.deleteOrder(orderID);
                    orderRepository.getAllOrders();
                    if(box != null) box.View.Refresh();
                }
                else
                {
                    orderRepository.modifyAmmount(orderID, -1);
                    orderRepository.modifyPrice(orderID, orderRepository.getById(orderID).Price - price);
                    orderRepository.getAllOrders();
                    if(box != null) box.View.Refresh();
                }
            }
        }
        private void plus_Click(object sender, EventArgs e)
        {
            orderDetailRepository.getAllOrderDetails();
            orderRepository.getAllOrders();

            double difference = detail.Price / detail.Quantity;
            orderDetailRepository.modifyPrice(orderDetailID, detail.Price + difference);
            orderDetailRepository.modifyQuantity(orderDetailID, detail.Quantity + 1);
            orderRepository.modifyAmmount(orderID, + 1);
            orderRepository.modifyPrice(orderID, orderRepository.getById(orderID).Price + difference);

            orderDetailRepository.getAllOrderDetails();
            orderRepository.getAllOrders();

            current.Text = (int.Parse(current.Text) + 1).ToString();
        }
        private void button_Hover(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.IconColor = Color.White;
        }
        private void button_Leave(object sender, EventArgs e)
        {
            IconButton button = (IconButton)sender;
            button.IconColor = ColorTranslator.FromHtml("#F1B24A");
        }
    }
}
