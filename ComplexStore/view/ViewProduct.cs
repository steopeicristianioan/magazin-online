using ComplexStore.model;
using ComplexStore.repository;
using FontAwesome.Sharp;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexStore.view
{
    class ViewProduct : View
    {
        private ProductRepository productRepository;
        private Product_CategoryRepository product_CategoryRepository;
        private CategoryRepository categoryRepository;
        private MainForm mainForm;
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }

        private int y = 100;

        private FlowLayoutPanel flowMain;
        private Label message;
        private TextBox searchBar;
        private IconPictureBox searchBarButton;

        public ViewProduct(MainForm mainForm, Size size) : base()
        {
            this.mainForm = mainForm;
            productRepository = new ProductRepository();
            product_CategoryRepository = new Product_CategoryRepository();
            categoryRepository = new CategoryRepository();

            this.Size = size;
            

            setMain();
            setAside();
            setHeader();

            loadMain();
            loadAside();

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
            ChainedHashtable<Category, List<Product>> hashtable = product_CategoryRepository.groupByCategories(categoryRepository);
            List<Category> allCategories = categoryRepository.AllCategories;
            foreach (Category category in allCategories)
            {
                CategoryBox box = new CategoryBox(mainForm, flowMain, category, hashtable.get(category));
                box.Load();
            }

            //dummy label;
            Label dummy = new Label();
            dummy.Parent = flowMain;
            dummy.Size = new Size(30, 50);
        }

        private void loadAside()
        {
            loadSearchBar();
            loadSearchBarButton();
        }
        private void loadSearchBar()
        {
            searchBar = new TextBox();
            searchBar.Parent = aside;
            searchBar.Width = aside.Width - 100;
            searchBar.Location = new Point(30, 25);
            searchBar.BackColor = aside.BackColor;
            searchBar.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            searchBar.ForeColor = message.ForeColor;
            searchBar.Text = "Search...";
            searchBar.TabStop = false;
            searchBar.TextAlign = HorizontalAlignment.Center;

            aside.Paint += new PaintEventHandler(drawSearchBarLine);
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
            searchBarButton.Parent = aside;
            searchBarButton.Size = new Size(40, 40);
            searchBarButton.Location = new Point(searchBar.Location.X + searchBar.Width + 5, 18);
            searchBarButton.IconChar = IconChar.Search;
            searchBarButton.IconColor = message.ForeColor;
            searchBarButton.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
