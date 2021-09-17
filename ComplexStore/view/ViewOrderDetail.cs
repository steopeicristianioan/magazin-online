using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Drawing;
using ComplexStore.model;
using ComplexStore.repository;
using generics.NETFramework;

namespace ComplexStore.view
{
    class ViewOrderDetail : View
    {
        private MainForm mainForm;
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }
        private OrderDetail detail;
        private OrderDetailRepository repository = new OrderDetailRepository();
        private bool loaded;
        private int y;

        private ProductCard card;
        private List<Label> options;
        private Label lblOptions;
        private Label lblInfo;
        private IconButton addToCart;

        public ViewOrderDetail(MainForm mainForm, int orderDetailID, Size size, bool loaded) : base()
        {
            this.mainForm = mainForm;
            this.detail = repository.getById(orderDetailID);
            this.Size = size;
            this.loaded = loaded;
            setMain();
            setHeader();
            setAside();
        }

        protected override void setHeader()
        {
            this.header.Location = new Point(0, 0);
            this.header.Size = new Size(this.Width, 75);
            this.header.BackColor = main.BackColor;
        }
        protected override void setAside()
        {
            this.aside.Location = new Point(0, 0);
            this.aside.Size = new Size(0,0);
            this.aside.BackColor = Color.Yellow;
        }
        protected override void setMain()
        {
            this.main.Location = new Point(0, 75);
            this.main.Size = new Size(this.Width, this.Height - 75);
            this.main.BackColor = ColorTranslator.FromHtml("#164A41");
            main.AutoScroll = true;
            

            loadMain();
        }

        private void loadMain()
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.getById(this.detail.Product_ID);

            card = new ProductCard(false, mainForm, repository, main, product, new Size(250,250));
            card.setLocation(main.Width/2 - 125, 25);
            card.load();
            if (loaded)
                card.Price.Text = detail.Price + " $";
            else card.Price.Text = product.Price + " $";

            if (loaded)
            {
                loadInfo();
                loadOptionsLabel();
                loadOptions();
            }
            else
            {
                loadPossibleOptions();
                loadAddToCart();
            }

        }
        private void loadOptionsLabel()
        {
            lblOptions = new Label();
            lblOptions.Parent = main;
            lblOptions.Size = new Size(410, 50);
            lblOptions.Location = new Point(main.Width/2-205, 340);
            lblOptions.Text = "These are the selected options";
            lblOptions.TextAlign = ContentAlignment.MiddleCenter;
            lblOptions.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblOptions.ForeColor = card.BackColor;

            IconPictureBox icon = new IconPictureBox();
            icon.Parent = lblOptions;
            icon.IconChar = IconChar.Cogs;
            icon.ForeColor = card.BackColor;
            icon.Size = new Size(50, 50);
            icon.Location = new Point(0, 0);

        }
        private void loadPossibleOptions()
        {
            y = 290;
            int x = main.Width/2 - 250, ct = 0, auxH1 = y, auxH2 = y;
            Product_OptionRepository product_OptionRepository = new Product_OptionRepository();
            ChainedHashtable<string, List<Option>> hashtable = product_OptionRepository.groupByIdentifiers(detail.Product_ID);
            string[] identifiers = staticMethods.StaticMethods.identifiers;
            foreach (string identifier in identifiers)
            {
                List<Option> options = hashtable.get(identifier);
                if (options != null)
                {
                    ct++;
                    OptionBox box = new OptionBox(main, options, identifier, card.Price);
                    box.Load();
                    if (ct % 2 == 1)
                    {
                        x = main.Width / 2 - 225;
                        if (ct == 1)
                            auxH1 += box.Height + 5;
                        else if (ct > 1)
                        {
                            y = auxH1;
                            auxH1 += box.Height + 5;
                        }
                    }
                    else
                    {
                        x += 250;
                        if (ct == 2)
                            auxH2 += box.Height + 5;
                        else if (ct > 2)
                        {
                            y = auxH2;
                            auxH2 += box.Height + 5;
                        }
                    }
                    box.SetLocation(x, y);
                }
            }
            //dummy label
            Label lbl = new Label();
            lbl.Parent = main;
            lbl.Size = new Size(50, 150);
            lbl.Location = new Point(50, y);
            lbl.BorderStyle = BorderStyle.None;
        }
        private void loadOptions()
        {
            int x = main.Width/2 - 205, y = 400, ct = 0;
            options = new List<Label>();
            OptionRepository optionRepository = new OptionRepository();
            List<Option> list = optionRepository.getOptionsFromOrderDetail(detail);
            foreach (Option option in list)
            {
                ct++;
                Label temp = new Label();
                temp.Parent = main;
                temp.Size = new Size(200, 50);
                temp.Text = option.Name;
                temp.TextAlign = ContentAlignment.MiddleCenter;
                temp.Location = new Point(x, y);
                temp.ForeColor = ColorTranslator.FromHtml("#F1B24A");
                temp.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                if (ct % 2 == 1)
                {
                    x += 210;
                }
                else
                {
                    x = main.Width/2 - 205;
                    y += 55;
                }
            }

            //dummy label
            Label lbl = new Label();
            lbl.Parent = main;
            lbl.Size = new Size(50, 45);
            lbl.Location = new Point(50, y);
            lbl.BorderStyle = BorderStyle.None;
        }
        private void loadInfo()
        {
            lblInfo = new Label();
            lblInfo.Parent = main;
            lblInfo.Size = new Size(300, 75);
            lblInfo.Location = new Point(main.Width - 320, 0);
            lblInfo.TextAlign = ContentAlignment.MiddleRight;
            lblInfo.Text = "Order detail # " + detail.ID + "\nFrom order # " + detail.Order_ID;
            lblInfo.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            lblInfo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }
        private void loadAddToCart()
        {
            addToCart = new IconButton();
            addToCart.Parent = main;
            addToCart.Size = new Size(200, 50);
            addToCart.Location = new Point(card.Location.X + card.Width + 10, card.Location.Y + card.Height - 50);
            addToCart.IconChar = IconChar.ShoppingCart;
            addToCart.IconColor = ColorTranslator.FromHtml("#F1B24A");
            addToCart.ImageAlign = ContentAlignment.MiddleLeft;
            addToCart.Text = "Add to cart";
            addToCart.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            addToCart.ForeColor = card.BackColor;
            addToCart.TextAlign = ContentAlignment.MiddleCenter;
            addToCart.TextImageRelation = TextImageRelation.ImageBeforeText;
            addToCart.FlatAppearance.BorderSize = 0;
            addToCart.FlatStyle = FlatStyle.Flat;
            addToCart.FlatAppearance.MouseDownBackColor = addToCart.FlatAppearance.MouseOverBackColor = Color.Transparent;

            addToCart.MouseHover += new EventHandler(addToCartHover);
            addToCart.MouseLeave += new EventHandler(addToCartLeave);
        }
        private void addToCartHover(object sender, EventArgs e)
        {
            Color aux = addToCart.IconColor;
            addToCart.IconColor = addToCart.ForeColor;
            addToCart.ForeColor = aux;
        }
        private void addToCartLeave(object sender, EventArgs e)
        {
            addToCart.IconColor = ColorTranslator.FromHtml("#F1B24A");
            addToCart.ForeColor = card.BackColor;
        }
    }
}
