using ComplexStore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ComplexStore.repository;

namespace ComplexStore.view
{
    class ProductCard : Panel
    {
        private Product product;
        private MainForm mainForm;
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }
        private OrderDetailRepository orderDetailRepository;
        private OrderRepository orderRepository;
        private bool click;

        private PictureBox img;
        private Label price;
        private Label name;
        private Font font = new Font("Segoe UI", 10, FontStyle.Bold);

        public Label Price { get => this.price; set => this.price = value; }
        public Label LBLName { get => this.name; set => this.name = value; }

        public ProductCard(bool click, MainForm mainForm, OrderDetailRepository orderDetailRepository, 
            Panel parent, Product product, Size size)
        {
            this.product = product;
            this.Parent = parent;
            this.Size = size;
            this.BackColor = ColorTranslator.FromHtml("#9DC88D");
            this.mainForm = mainForm;
            this.orderDetailRepository = orderDetailRepository;
            this.click = click;
            //this.orderRepository = orderRepository;
        }
        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public void load()
        {
            setPictureBox();
            setCommonNamePrice();
            if (click)
            {
                this.Click += new EventHandler(card_Click);
                img.Click += new EventHandler(card_Click);
                price.Click += new EventHandler(card_Click);
                name.Click += new EventHandler(card_Click);
            }
        }
        private void setPictureBox()
        {
            img = new PictureBox();
            img.Parent = this;
            img.Location = new Point(this.Width/2 - 75, 10);
            img.Size = new Size(150,150);
            img.SizeMode = PictureBoxSizeMode.StretchImage;
            img.Image = product.Image;
        }
        private void setCommonNamePrice()
        {
            name = new Label();
            price = new Label();

            name.Parent = price.Parent = this;
            name.TextAlign = price.TextAlign = ContentAlignment.MiddleCenter;
            name.Font = price.Font = font;
            name.Size = price.Size = new Size(200, 25);
            name.BackColor = price.BackColor = this.BackColor;
            name.ForeColor = price.ForeColor = ColorTranslator.FromHtml("#164A41");

            setName();
            setPrice();
        }
        private void setName()
        {
            name.Location = new Point(this.Width / 2 - 100, 170);
            name.Text = product.Name;
            int size = 10;
            while ((product.Name.Length + 5) * size > 200)
                size--;
            name.Font = new Font("Segoe UI", size, FontStyle.Bold);
        }
        private void setPrice()
        {
            price.Location = new Point(name.Location.X, 200);
        }

        private void card_Click(object sender, EventArgs e)
        {

            orderDetailRepository.add(product.ID, 1, product.Price, "");
            orderDetailRepository.getAllOrderDetails();
            //MessageBox.Show(mainForm.Main.Size.ToString());
            mainForm.openChild(new ViewOrderDetail(mainForm, orderDetailRepository.LastID, mainForm.Size, false), mainForm);
        }
    }
}
