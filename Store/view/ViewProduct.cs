using Store.model;
using Store.repository;
using FontAwesome.Sharp;
using generics.NETFramework;
using Store;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.view
{
    class ViewProduct : View
    {
        private ProductRepository productRepository;
        private Product_CategoryRepository product_CategoryRepository;
        private CategoryRepository categoryRepository;
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;
        private MainForm mainForm;
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }
        private ChainedHashtable<Category, List<Product>> hashtable;

        //private int y = 100;

        private FlowLayoutPanel flowMain;
        private Label message;
        private Panel temp;

        private TextBox searchBar;
        private IconPictureBox searchBarButton;
        private FlowLayoutPanel flowAside;
        private AllCategoryBox allCategoryBox;
        private PriceSelector priceSelector;
        private IconButton search;

        private IconButton cart;

        public ViewProduct(MainForm mainForm, Size size) : base()
        {
            this.mainForm = mainForm;
            productRepository = new ProductRepository();
            product_CategoryRepository = new Product_CategoryRepository();
            categoryRepository = new CategoryRepository();
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();

            this.Size = size;
            

            setMain();
            setAside();
            setHeader();

            loadMain();
            loadAside();
            loadHeader();
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
            aside.Location = new Point(0, 75);
            this.aside.BackColor = main.BackColor;
        }
        protected override void setMain()
        {
            main.Size = new Size(this.Width - 350, this.Height - 75);
            main.Location = new Point(350, 75);
            this.main.BackColor = ColorTranslator.FromHtml("#164A41");
            //main.AutoScroll = true;

            flowMain = new FlowLayoutPanel();
            flowMain.Parent = main;
            flowMain.Location = new Point(0, 110);
            flowMain.Size = new Size(main.Width, main.Height - 110);
            flowMain.AutoScroll = true;
            flowMain.Margin = new Padding(1);
            flowMain.Padding = new Padding(10, 20, 0, 0);
        }

        private void loadMain()
        {
            loadTemp();
            loadTitle();
            loadHome();
        }
        private void loadTitle()
        {
            message = new Label();
            message.Parent = main;
            message.Size = new Size(500, 100);
            message.Location = new Point(main.Width / 2 - 250, 5);
            message.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.Text = "START YOUR SHOPPING AND ENJOY!";
            message.Font = new Font("Segoe UI", 15, FontStyle.Bold);
        }
        private void loadHome()
        {
            hashtable = product_CategoryRepository.groupByCategories(categoryRepository);
            List<Category> allCategories = categoryRepository.AllCategories;
            foreach (Category category in allCategories)
            {
                CategoryBox box = new CategoryBox(mainForm, flowMain, category, hashtable.get(category), 
                    orderRepository, orderDetailRepository, mainForm.id);
                box.Load();
            }

            //dummy label;
            Label dummy = new Label();
            dummy.Parent = flowMain;
            dummy.Size = new Size(30, 50);
        }

        private void loadAside()
        {
            //loadSearchBar();
            //loadSearchBarButton();

            loadFlowAside();
            loadSearch();
        }
        private void loadFlowAside()
        {
            flowAside = new FlowLayoutPanel();
            flowAside.Parent = aside;
            flowAside.Location = new Point(0, 15);
            flowAside.Size = new Size(aside.Width, aside.Height);

            loadAllCategoryBox();
            loadPriceInterval();
        }
        private void loadAllCategoryBox()
        {
            allCategoryBox = new AllCategoryBox(flowAside, categoryRepository);
            allCategoryBox.Load();
            allCategoryBox.Margin = new Padding((flowAside.Width - 300) / 2, 0, 0, 0);
        }
        private void loadPriceInterval()
        {
            priceSelector = new PriceSelector(flowAside);
            priceSelector.Margin = new Padding(allCategoryBox.Margin.Left, 10, 0, 0);
            priceSelector.Load();
        }
        private void loadSearch()
        {
            search = new IconButton();
            search.Parent = flowAside;
            search.Size = new Size(200, 50);
            search.Margin = new Padding((flowAside.Width - 200) / 2, 10, 0, 0);
            search.FlatStyle = FlatStyle.Flat;
            search.FlatAppearance.BorderSize = 0;
            search.FlatAppearance.BorderColor = flowAside.BackColor;
            //search.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#F1B24A");
            search.FlatAppearance.MouseDownBackColor = search.FlatAppearance.MouseOverBackColor = Color.Transparent;
            search.IconColor = search.ForeColor = ColorTranslator.FromHtml("#FFFFff");
            search.TextImageRelation = TextImageRelation.ImageBeforeText;
            search.IconChar = IconChar.Searchengin;
            search.Text = "Search using these filters";
            search.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            search.MouseHover += new EventHandler(this.search_Hover);
            search.MouseLeave += new EventHandler(this.search_Leave);
            search.Click += new EventHandler(this.search_Click);
        }
        private void search_Hover(object sende, EventArgs e)
        {
            search.IconColor = search.ForeColor = ColorTranslator.FromHtml("#9DC88D");
        }
        private void search_Leave(object sender, EventArgs e)
        {
            search.IconColor = search.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }
        private void search_Click(object sender, EventArgs e)
        {
            if (!checkMainFilters())
            {
                MessageBox.Show("Make sure your filters are correct!", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int index = allCategoryBox.Categories.IndexOf(allCategoryBox.Checked);
            Category aux = new Category(index + 13, categoryRepository.AllCategories[index].Name);
            StandardSearch search = new StandardSearch(double.Parse(priceSelector.MinPrice.Text),
                double.Parse(priceSelector.MaxPrice.Text));
            List<Product> filtered = productRepository.getFilteredByCategory(hashtable, aux, search);
            CategoryBox category = new CategoryBox(mainForm, flowMain, aux, filtered, orderRepository, orderDetailRepository, mainForm.id);
            ViewSearch viewSearch = new ViewSearch(search, mainForm, aux, new OptionRepository(), category,
                new Product_OptionRepository(), orderDetailRepository);
            mainForm.openChild(viewSearch, mainForm);
            //category.Load();
        }
        private bool checkMainFilters()
        {
            if (allCategoryBox.Checked == null)
                return false;
            bool min = double.TryParse(priceSelector.MinPrice.Text, out double minim);
            bool max = double.TryParse(priceSelector.MaxPrice.Text, out double maxim);
            if (!min || !max || (minim > maxim))
                return false;
            return true;
        }

        private void loadHeader()
        {
            loadSearchBar();
            loadSearchBarButton();
            loadCart();
        }
        private void loadSearchBar()
        {
            searchBar = new TextBox();
            searchBar.Parent = header;
            searchBar.Width = 250;
            searchBar.Location = new Point((header.Width - 250) / 2, 25);
            searchBar.BackColor = aside.BackColor;
            searchBar.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            searchBar.ForeColor = message.ForeColor;
            searchBar.Text = "Search...";
            searchBar.TabStop = false;
            searchBar.TextAlign = HorizontalAlignment.Center;

            header.Paint += new PaintEventHandler(drawSearchBarLine);
            searchBar.Click += new EventHandler(this.searchBar_Click);
        }
        private void drawSearchBarLine(object sender, PaintEventArgs e)
        {
            searchBar.BorderStyle = BorderStyle.None;
            Pen p = new Pen(message.ForeColor);
            Graphics g = e.Graphics;
            int variance = 5;
            g.DrawRectangle(p, new Rectangle(searchBar.Location.X - variance, searchBar.Location.Y - variance, searchBar.Width + variance, searchBar.Height + variance));
        }
        private void loadSearchBarButton()
        {
            searchBarButton = new IconPictureBox();
            searchBarButton.Parent = header;
            searchBarButton.Size = new Size(40, 40);
            searchBarButton.Location = new Point(searchBar.Location.X + searchBar.Width + 5, 18);
            searchBarButton.IconChar = IconChar.Search;
            searchBarButton.IconColor = message.ForeColor;
            searchBarButton.SizeMode = PictureBoxSizeMode.StretchImage;

            searchBarButton.Click += new EventHandler(this.searchBarButton_Click);
        }
        private void loadCart()
        {
            cart = new IconButton();
            cart.Parent = header;
            cart.TabStop = false;
            cart.Size = new Size(100, 71);
            cart.Location = new Point(header.Width - 300, 2);
            cart.IconChar = IconChar.ShoppingCart;
            cart.IconColor = Color.White;
            cart.FlatStyle = FlatStyle.Flat;
            cart.FlatAppearance.BorderSize = 0;
            cart.FlatAppearance.MouseDownBackColor = cart.FlatAppearance.MouseOverBackColor = Color.Transparent;
            cart.FlatAppearance.BorderColor = header.BackColor;

            cart.MouseHover += new EventHandler(cart_Hover);
            cart.MouseLeave += new EventHandler(cart_Leave);
            cart.Click += new EventHandler(cart_Click);
        }
        private void cart_Hover(object sender, EventArgs e)
        {
            cart.IconColor = message.ForeColor;
        }
        private void cart_Leave(object sender, EventArgs e)
        {
            cart.IconColor = Color.White;
        }
        private void cart_Click(object sender, EventArgs e)
        {
            ViewCart viewCart = new ViewCart(mainForm.id, orderRepository, new OrderDetailRepository(), mainForm.Size, productRepository);
            viewCart.MainForm = mainForm;
            mainForm.openChild(viewCart, mainForm);
        }
        private void searchBar_Click(object sender, EventArgs e)
        {
            searchBar.Text = "";
        }
        private void searchBarButton_Click(object sender, EventArgs e)
        {
            loadNewList();
        }
        private List<Product> getBySearchBar()
        {
            List<Product> products = new List<Product>();
            List<int> ids = new List<int>();
            foreach(Category category in categoryRepository.AllCategories)
            {
                List<Product> products1 = hashtable.get(category);
                foreach (Product product in products1)
                {
                    if ((product.Name.Contains(searchBar.Text) || product.Description.Contains(searchBar.Text))
                        && (searchBar.Text != "") && (!products.Contains(product)))
                    {
                        products.Add(product);
                        ids.Add(product.ID);
                    }
                }
            }
            return products;
        }
        private void loadNewList()
        {
            main.Visible = false;

            List<Product> products = getBySearchBar();

            if(temp.Controls != null) temp.Controls.Clear();

           

            ViewProductsByName view = new ViewProductsByName(mainForm, products, main.Size,
                orderDetailRepository, orderRepository, this);
            view.Parent = temp;
            view.load();
            view.Show();
        }
        private void loadTemp()
        {
            temp = new Panel();
            temp.Parent = this;
            temp.Size = main.Size;
            temp.Location = main.Location;
        }
    }
}
