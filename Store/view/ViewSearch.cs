using Store.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Forms;
using generics.NETFramework;
using Store.repository;

namespace Store.view
{
    class ViewSearch : View
    {
        private StandardSearch standardSearch;
        private MainForm mainForm;
        public MainForm MainForm { get => this.mainForm; set => this.mainForm = value; }
        private Category category;
        public Category Category { get => this.category; }
        private PriceSelector priceSelector;
        private OptionRepository optionRepository;
        private ChainedHashtable<string, List<Option>> hashtable;
        private Product_OptionRepository product_OptionRepository;
        private OrderDetailRepository orderDetailRepository;

        private Label cName;
        private List<FilterBox> filterBoxes;
        private FlowLayoutPanel flowAside;
        private IconButton search;

        private CategoryBox categoryBox;
        private List<Product> products;

        public ViewSearch(StandardSearch search, MainForm mainForm, Category category,
            OptionRepository optionRepository, CategoryBox categoryBox,
            Product_OptionRepository product_OptionRepository,
            OrderDetailRepository orderDetailRepository)
        {
            this.standardSearch = search;
            this.mainForm = mainForm;
            this.category = category;
            this.Size = mainForm.Size;
            this.optionRepository = optionRepository;
            this.categoryBox = categoryBox;
            products = categoryBox.Products;
            this.hashtable = optionRepository.groupByIdentifiers(category.ID, new Category_OptionRepository());
            this.product_OptionRepository = product_OptionRepository;
            this.orderDetailRepository = orderDetailRepository;

            setMain();
            setAside();
            setHeader();

            loadAside();
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
            aside.Location = new Point(0, 75);
            this.aside.BackColor = main.BackColor;
        }
        protected override void setMain()
        {
            main.Size = new Size(this.Width - 350, this.Height - 75);
            main.Location = new Point(350, 75);
            this.main.BackColor = ColorTranslator.FromHtml("#164A41");
        }

        private void loadAside()
        {
            loadName();
            loadFlowAside();
            loadPriceSelector();
            loadFilters();
            loadSearch();
        }
        private void loadName()
        {
            cName = new Label();
            cName.Parent = aside;
            cName.Size = new Size(aside.Width - 20, 50);
            cName.Location = new Point(10, 15);
            cName.TextAlign = ContentAlignment.MiddleCenter;
            cName.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            cName.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            cName.Text = category.Name;
            cName.BorderStyle = BorderStyle.FixedSingle;
        }
        private void loadPriceSelector()
        {
            priceSelector = new PriceSelector(flowAside);
            priceSelector.Margin = new Padding((aside.Width - priceSelector.Width) / 2, 0, 0, 5);
            priceSelector.Load();
            priceSelector.MinPrice.Text = standardSearch.MinPrice.ToString();
            priceSelector.MaxPrice.Text = standardSearch.MaxPrice.ToString();
        }
        private void loadFlowAside()
        {
            flowAside = new FlowLayoutPanel();
            flowAside.Parent = aside;
            flowAside.Size = new Size(aside.Width + 50, aside.Height - 75);
            flowAside.Location = new Point(0, 75);
            flowAside.BorderStyle = BorderStyle.None;
        }
        private void loadFilters()
        {
            int y = 100 + priceSelector.Location.Y, i = 0;
            filterBoxes = new List<FilterBox>();
            foreach(string identifier in staticMethods.StaticMethods.identifiers)
            {
                List<Option> options = hashtable.get(identifier);
                if(options.Count != 0)
                {
                    FilterBox box = new FilterBox(flowAside, identifier, options[0].Type, options, i);
                    box.Margin = new Padding((aside.Width - box.Width) / 2, 0, 0, 10);
                    box.Load();
                    y += box.Height + 5;
                    if (y > flowAside.Width)
                        flowAside.AutoScroll = true;
                    filterBoxes.Add(box);
                }
                i++;
            }


        }
        private void loadSearch()
        {
            search = new IconButton();
            search.Parent = flowAside;
            search.Size = new Size(200, 50);
            search.Margin = new Padding((aside.Width - 200) / 2, 5, 0, 0);
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

            //dummy;
            Label dummy = new Label();
            dummy.Parent = flowAside;
            dummy.Size = new Size(1, 1);
            dummy.Margin = new Padding(0, 100, 0, 0);
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
            foreach(FilterBox box in filterBoxes)
                if(box.Type == 1 && !box.ComboOK)
                {
                    MessageBox.Show("Filter " + box.FilterName + " is invalid", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            loadNewList();
        }
        private void loadMain()
        {
            this.categoryBox.Parent = main;
            categoryBox.Location = new Point((main.Width - categoryBox.Width) / 2, (main.Height - categoryBox.Height) / 2);
            this.categoryBox.Load();
        }
        private void loadNewList()
        {
            main.Controls.Clear();

            List<Product> newProducts = new List<Product>();
            foreach(Product product in products)
            {
                if (checkFilters(product))
                    newProducts.Add(product);
            }

            OrderRepository orderRepository = categoryBox.OrderRepository;
            CategoryBox newBox = new CategoryBox(mainForm, main, category, newProducts, orderRepository, orderDetailRepository, mainForm.id);
            newBox.Location = new Point((main.Width - newBox.Width) / 2, (main.Height - newBox.Height) / 2);
            newBox.Load();
            categoryBox = newBox;
        }
        private bool checkFilters(Product product)
        {
            if (!(product.Price >= double.Parse(priceSelector.MinPrice.Text) &&
                product.Price <= double.Parse(priceSelector.MaxPrice.Text)))
                return false;
            foreach(FilterBox filterBox in filterBoxes)
            {
                if (filterBox.Type == 1 && !handleType1(product, filterBox))
                    return false;
                else if (filterBox.Type == 2 && !handleType2(product, filterBox))
                    return false;
            }
            return true;
        }
        private bool handleType1(Product product, FilterBox box)
        {
            List<Product_Option> product_Options = product_OptionRepository.getProductOptionsByProductId(product.ID);
            List<Option> options = optionRepository.getFromProduct_OptionList(product_Options);
            int startID = box.StartID, endID = box.EndID, ct = 0;
            //MessageBox.Show(startID.ToString() + endID.ToString());
            foreach (Option option in options)
                if (option.Name.Contains(box.FilterName))
                    if (option.ID > startID && option.ID < endID)
                        return true;
            return false;
        }
        private bool handleType2(Product product, FilterBox box)
        {
            if (box.Selected == 0)
                return true;
            List<Product_Option> product_Options = product_OptionRepository.getProductOptionsByProductId(product.ID);
            List<Option> options = optionRepository.getFromProduct_OptionList(product_Options);
            foreach (Option option in options)
                if (option.Name.Contains(box.FilterName))
                    if (box.IDS[option.ID] != 0)
                        return true;
            return false;
        }
    }
}
