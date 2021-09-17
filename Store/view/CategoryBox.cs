using Store.model;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;

namespace Store.view
{
    class CategoryBox : Panel
    {
        private List<Product> products;
        public List<Product> Products { get => this.products; set => this.products = value; }
        private Category category;
        private MainForm mainForm;
        private OrderDetailRepository orderDetailRepository;
        private OrderRepository orderRepository;
        public OrderRepository OrderRepository { get => this.orderRepository; }
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }
        private int id;



        private int currentIndex = 0;
        private int x = 5;
        private int loadAtOnce = 2;

        private Label title;
        private List<ProductCard> cards;
        private IconButton show;
        private Panel pnlProducts;
        private IconButton loadMore;

        public CategoryBox(MainForm mainForm, Control parent, Category category, 
            List<Product> products, OrderRepository orderRepository, 
            OrderDetailRepository orderDetailRepository, int id)
        {
            this.mainForm = mainForm;
            this.Parent = parent;
            this.category = category;
            this.products = products;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.id = id;

            cards = new List<ProductCard>();
            

            this.Size = new Size(parent.Width - 50, 350);
            this.MinimumSize = new Size(this.Width, 35);
            this.MaximumSize = this.Size;
        }
        public void SetLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public void Load()
        {
            loadTitle();
            loadShow();
            loadPnlProducts();
            loadSection();
            if (products.Count > loadAtOnce)
                loadMoreButton();
        }
        private void loadTitle()
        {
            title = new Label();
            title.Parent = this;
            title.Text = category.Name;
            title.Size = new Size(200, 30);
            title.Location = new Point(1, 1);
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            title.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadShow()
        {
            show = new IconButton();
            show.TabStop = false;
            show.Parent = this;
            show.Size = new Size(30, 30);
            show.IconChar = IconChar.SortUp;
            show.Location = new Point(title.Width + 5, 1);
            show.IconColor = ColorTranslator.FromHtml("#F1b24A");
            show.FlatStyle = FlatStyle.Flat;
            show.FlatAppearance.BorderSize = 0;
            show.ImageAlign = ContentAlignment.TopCenter;
            show.FlatAppearance.MouseDownBackColor = show.FlatAppearance.MouseOverBackColor = Color.Transparent;

            show.Click += new EventHandler(this.show_Click);
        }
        private void loadPnlProducts()
        {
            pnlProducts = new Panel();
            pnlProducts.Parent = this;
            pnlProducts.Location = new Point(1, 35);
            pnlProducts.Size = new Size(this.Width - 2, this.Height - 45);
            pnlProducts.AutoScroll = true;
        }
        private void show_Click(object sender, EventArgs e)
        {
            if(show.IconChar == IconChar.SortUp)
            {
                this.Size = this.MinimumSize;
                show.IconChar = IconChar.SortDown;
                show.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                this.Size = this.MaximumSize;
                show.IconChar = IconChar.SortUp;
                show.ImageAlign = ContentAlignment.TopCenter;
            }
        }
        private void loadSection()
        {
            int ct = 0;
            for (int i = currentIndex; i < currentIndex + loadAtOnce && i<products.Count ; i++)
            {
                ProductCard temp = new ProductCard(true,mainForm, orderDetailRepository, pnlProducts, products[i], new Size(250, 250));
                temp.OrderRepository = orderRepository;
                temp.Customer_ID = this.id;
                temp.setLocation(x, (pnlProducts.Height - 250) / 2 - 10);
                temp.load();
                temp.Price.Text = products[i].Price + " $";
                x += 255;
                int aux = i;
                cards.Add(temp);
                ct++;
            }
            currentIndex += ct;
        }
        private void loadMoreButton()
        {
            loadMore = new IconButton();
            loadMore.Parent = pnlProducts;
            loadMore.TabStop = false;
            loadMore.Size = new Size(125, 125);
            loadMore.Location = new Point(x, (pnlProducts.Height - 125) / 2 - 10);
            loadMore.IconChar = IconChar.Spinner;
            loadMore.ImageAlign = ContentAlignment.TopCenter;
            loadMore.Text = "Load more products";
            loadMore.ForeColor = loadMore.IconColor = Color.White;
            loadMore.BackColor = Color.Transparent;
            loadMore.TextAlign = ContentAlignment.BottomCenter;
            loadMore.FlatStyle = FlatStyle.Flat;
            loadMore.FlatAppearance.BorderSize = 0;
            loadMore.FlatAppearance.MouseDownBackColor = loadMore.FlatAppearance.MouseOverBackColor =  Color.Transparent;
            loadMore.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Bold);
            loadMore.IconSize = 75;

            loadMore.Click += new EventHandler(loadMore_Click);
            loadMore.MouseHover += new EventHandler(loadMore_Hover);
            loadMore.MouseLeave += new EventHandler(loadMore_Leave);
        }
        private void loadMore_Click(object sender, EventArgs e)
        {
            loadMore.Visible = false;
            loadSection();
            if(!(currentIndex == products.Count))
            {
                loadMore.Location = new Point(x, loadMore.Location.Y);
                loadMore.Visible = true;
            }
        }
        private void loadMore_Hover(object sende, EventArgs e)
        {
            loadMore.ForeColor = loadMore.IconColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void loadMore_Leave(object sender, EventArgs e)
        {
            loadMore.ForeColor = loadMore.IconColor = Color.White;
        }
        public override void Refresh()
        {
            //MessageBox.Show(products.Count.ToString());
            pnlProducts.Controls.Clear();
            currentIndex = 0;
            loadSection();
            if (products.Count > loadAtOnce)
                loadMore.Visible = true;
            else loadMore.Visible = false;
        }

    }
}
