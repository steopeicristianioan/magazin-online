using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Store.repository;
using Store.model;
using FontAwesome.Sharp;

namespace Store.view
{
    class OrderDetailBox : Panel
    {
        private ViewCart view;
        public ViewCart View { get => this.view; }
        private OrderDetail detail;
        private OrderDetailRepository orderDetailRepository;
        private ProductRepository productRepository;
        private OrderRepository orderRepository;
        private Product product;

        private PictureBox pic;
        private Label desc;
        private IconPictureBox logo;
        private QuantityModifier modifier;
        private IconButton expand;

        public OrderDetailBox(OrderDetail detail, OrderDetailRepository orderDetailRepository
            , Size size, ProductRepository productRepository, OrderRepository orderRepository
            , Control parent, ViewCart view)
        {
            this.detail = detail;
            this.orderDetailRepository = orderDetailRepository;
            this.productRepository = productRepository;
            this.Parent = parent;
            this.orderRepository = orderRepository;
            this.product = productRepository.getById(this.detail.Product_ID);
            this.view = view;

            this.Size = size;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void Load()
        {
            //loadLogo();
            loadPic();
            loadDesc();
            loadModifier();
            loadExpand();
        }

        private void loadLogo()
        {
            logo = new IconPictureBox();
            logo.Parent = this;
            logo.Size = new Size(50, 50);
            logo.Location = new Point(0, (this.Height - 50) / 2);
            logo.IconColor = ColorTranslator.FromHtml("#F1B24A");
            logo.IconChar = IconChar.Tag;
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void loadPic()
        {
            pic = new PictureBox();
            pic.Parent = this;
            pic.Size = new Size(100, this.Height - 10);
            pic.Location = new Point(15, 5);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = product.Image;
        }
        private void loadDesc()
        {
            desc = new Label();
            desc.Parent = this;
            desc.Location = new Point(120, 5);
            desc.Size = new Size(this.Width - 450, this.Height - 10);
            desc.ForeColor = ColorTranslator.FromHtml("#9DC88D");
            desc.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            desc.Text = product.Name + "\n" + (detail.Price / detail.Quantity).ToString() + " $";
            desc.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadModifier()
        {
            modifier = new QuantityModifier(this, new Size(100, this.Height), detail.Order_ID, detail.ID,
                orderDetailRepository, orderRepository, this);
            modifier.Detail = detail;
            modifier.Location = new Point(110 + desc.Width + 5, 0);
            modifier.Load();
        }
        private void loadExpand()
        {
            expand = new IconButton();
            expand.Parent = this;
            expand.Location = new Point(175 + desc.Width + modifier.Width, 5);
            expand.Size = new Size(150, this.Height - 10);
            expand.IconChar = IconChar.ExpandAlt;
            expand.ImageAlign = ContentAlignment.MiddleLeft;
            expand.TextImageRelation = TextImageRelation.ImageBeforeText;
            expand.Text = "See more";
            expand.ForeColor = expand.IconColor = ColorTranslator.FromHtml("#9DC88D");
            expand.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            expand.FlatStyle = FlatStyle.Flat;
            expand.FlatAppearance.BorderSize = 0;
            expand.FlatAppearance.MouseDownBackColor = expand.FlatAppearance.MouseOverBackColor = Color.Transparent;
            expand.TabStop = false;

            expand.MouseHover += new EventHandler(this.expand_Hover);
            expand.MouseLeave += new EventHandler(this.expand_Leave);
            expand.Click += new EventHandler(this.expand_Click);
        }
        private void expand_Hover(object sender, EventArgs e)
        {
            expand.IconColor = expand.ForeColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void expand_Leave(object sender, EventArgs e)
        {
            expand.IconColor = expand.ForeColor = ColorTranslator.FromHtml("#9DC88D");
        }
        private void expand_Click(object sender, EventArgs e)
        {
            ViewOrderDetail viewOrderDetail = new ViewOrderDetail(view.MainForm, detail.ID,
                view.MainForm.Size, true);
            viewOrderDetail.FromCart = true;
            viewOrderDetail.OrderRepository = orderRepository;
            viewOrderDetail.loadMain();
            view.MainForm.openChild(viewOrderDetail, view.MainForm);
        }
    }
}

