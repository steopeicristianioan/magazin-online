using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Drawing;
using Store.model;
using Store.repository;
using generics.NETFramework;
using Store.staticMethods;

namespace Store.view
{
    class ViewOrderDetail : View
    {
        private MainForm mainForm;
        public MainForm MAINFORM { get => this.mainForm; set => this.mainForm = value; }
        private OrderDetail detail;
        private OrderDetailRepository repository = new OrderDetailRepository();
        private bool loaded;
        private int y;
        private List<OptionBox> boxes;
        private OrderRepository orderRepository;
        public OrderRepository OrderRepository { get => this.orderRepository; set => this.orderRepository = value; }

        private int customer_id;
        public int Customer_ID { get => this.customer_id; set => this.customer_id = value; }
        private bool added = false;
        private bool fromCart = false;
        public bool FromCart { get => this.fromCart; set => this.fromCart = value; }
        private double start_Price;

        private ProductCard card;
        private List<Label> options;
        private Label lblOptions;
        private Label lblInfo;
        private IconButton addToCart;
        private IconButton back;

        private Panel quantity;
        private Label currentQuantity;
        private IconPictureBox plusQuantity;
        private IconPictureBox minusQuantity;
        private Label quantityMessage;
        private IconButton save;
        private QuantityModifier modifier;
        private IconButton remove;

        public ViewOrderDetail(MainForm mainForm, int orderDetailID, Size size, bool loaded) : base()
        {
            this.mainForm = mainForm;
            this.detail = repository.getById(orderDetailID);
            this.Size = size;
            this.loaded = loaded;
            boxes = new List<OptionBox>();

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
        }

        public void loadMain()
        {
            ProductRepository productRepository = new ProductRepository();
            Product product = productRepository.getById(this.detail.Product_ID);

            card = new ProductCard(false, mainForm, repository, main, product, new Size(250,250));
            card.setLocation(main.Width/2 - 125, 25);
            card.load();
            if (loaded)
                card.Price.Text = detail.Price + " $";
            else card.Price.Text = product.Price + " $";

            if (fromCart)
            {
                loadIfFromCart();
                return;
            }
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
            loadBack();
            loadQuantity();
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
            string[] identifiers = StaticMethods.identifiers;
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
                    boxes.Add(box);
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
            lblInfo.Location = new Point(main.Width - 335, 0);
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
            addToCart.Click += new EventHandler(addToCart_Click);
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
        private void addToCart_Click(object sender, EventArgs e)
        {
            added = true;
            constructPrice();
            constructOptions();
            repository.modifyQuantity(detail.ID, int.Parse(currentQuantity.Text));
            orderRepository.modifyAmmount(detail.Order_ID, int.Parse(currentQuantity.Text) - 1);
        }
        private void constructPrice()
        {
            double price = card.Product.Price;
            foreach(OptionBox box in boxes)
            {
                double temp = box.Options[box.Current_Index].Price_Increase;
                price += temp;
            }
            repository.modifyPrice(detail.ID, price * int.Parse(currentQuantity.Text));
            updateOrder(price * int.Parse(currentQuantity.Text));
        }
        private void constructOptions()
        {
            string options = "";
            foreach(OptionBox box in boxes)
            {
                options += box.Options[box.Current_Index].ID.ToString() + ",";
            }
            options = options.Remove(options.Length - 1, 1);
            repository.modifyOptions(detail.ID, options);
        }
        private void updateOrder(double add)
        {
            double price = orderRepository.getById(detail.Order_ID).Price;
            orderRepository.modifyPrice(detail.Order_ID, price + add);
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
            if (!added && !fromCart)
            {
                repository.deleteOrderDetail(detail.ID);
                orderRepository.modifyAmmount(detail.Order_ID, -1);
            }
            if (mainForm.currentChild.GetType().Equals(typeof(ViewCart))) 
            {
                mainForm.currentChild.Refresh();
            }
        }
        private void loadQuantity()
        {
            quantity = new Panel();
            quantity.Parent = main;
            quantity.Size = new Size(150, 75);
            quantity.Location = new Point(card.Location.X - 160, card.Location.Y + (card.Height - 75));
            //quantity.BorderStyle = BorderStyle.FixedSingle;

            if(!loaded)
                loadQuantityPlusMinus();
            loadCurrentQuantity();
            loadQuantityMessage();
        }
        private void loadQuantityPlusMinus()
        {
            plusQuantity = new IconPictureBox();
            minusQuantity = new IconPictureBox();

            plusQuantity.Parent = minusQuantity.Parent = quantity;
            plusQuantity.Size = minusQuantity.Size = new Size(25,25);
            //plusQuantity.BorderStyle = minusQuantity.BorderStyle = BorderStyle.FixedSingle;
            plusQuantity.SizeMode = PictureBoxSizeMode.StretchImage;
            plusQuantity.IconColor = minusQuantity.IconColor = ColorTranslator.FromHtml("#F1B24A");

            plusQuantity.Location = new Point(100, 40);
            minusQuantity.Location = new Point(15, 40);

            plusQuantity.IconChar = IconChar.Plus;
            minusQuantity.IconChar = IconChar.Minus;

            plusQuantity.Click += new EventHandler(plus_Click);
            minusQuantity.Click += new EventHandler(minus_Click);
        }
        private void loadCurrentQuantity()
        {
            currentQuantity = new Label();
            currentQuantity.Parent = quantity;
            currentQuantity.Size = new Size(60, 35);
            currentQuantity.Location = new Point(40, 35);
            currentQuantity.ForeColor = plusQuantity.IconColor;
            currentQuantity.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            if (!loaded)
                currentQuantity.Text = "1";
            else currentQuantity.Text = detail.Quantity.ToString();
            currentQuantity.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadQuantityMessage()
        {
            quantityMessage = new Label();
            quantityMessage.Parent = quantity;
            quantityMessage.Location = new Point(0, 0);
            quantityMessage.Size = new Size(quantity.Width, 40);
            quantityMessage.Font = currentQuantity.Font;
            quantityMessage.Text = "Quantity";
            quantityMessage.ForeColor = card.BackColor;
            quantityMessage.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void plus_Click(object sender, EventArgs e)
        {
            currentQuantity.Text = (int.Parse(currentQuantity.Text) + 1).ToString();
        }
        private void minus_Click(object sender, EventArgs e)
        {
            if(int.Parse(currentQuantity.Text) > 1)
                currentQuantity.Text = (int.Parse(currentQuantity.Text) - 1).ToString();
        }

        private void loadIfFromCart()
        {
            card.Price.Text = (detail.Price / detail.Quantity).ToString() + "$";
            start_Price = double.Parse(card.Price.Text.Substring(0, card.Price.Text.Length - 2));
            modifier = new QuantityModifier(main, new Size(100, 50), detail.Order_ID,
                detail.ID, repository, orderRepository, null);
            modifier.s = 0;
            modifier.Location = new Point(card.Location.X - 105, card.Location.Y + (card.Height - 100));
            //modifier.BorderStyle = BorderStyle.FixedSingle;
            modifier.Detail = detail;
            modifier.Load();
            loadPossibleOptionsFromCart();
            loadSave();
            loadInfo();
            loadRemove();
            loadBack();
        }
        private int getCurrentIndexOfOption(int option_ID, List<Option> options)
        {
            for (int i = 0; i < options.Count; i++)
                if (options[i].ID == option_ID)
                    return i;
            return -1;
        }
        private void loadPossibleOptionsFromCart()
        {
            y = 290;
            int x = main.Width / 2 - 250, ct = 0, auxH1 = y, auxH2 = y;
            Product_OptionRepository product_OptionRepository = new Product_OptionRepository();
            ChainedHashtable<string, List<Option>> hashtable = product_OptionRepository.groupByIdentifiers(detail.Product_ID);
            string[] identifiers = StaticMethods.identifiers;
            foreach (string identifier in identifiers)
            {
                List<Option> options = hashtable.get(identifier);
                if (options != null)
                {
                    string[] op = detail.Selected_Options.Split(',');
                    int res = -1;
                    for(int i = 0; i<op.Length; i++)
                    {
                        int id = int.Parse(op[i]);
                        int index = getCurrentIndexOfOption(id, options);
                        if(index != -1)
                        {
                            res = index;
                            break;
                        }
                    }
                    ct++;
                    OptionBox box = new OptionBox(main, options, identifier, card.Price);
                    box.Current_Index = res;
                    box.Start_Index = res;
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
                    boxes.Add(box);
                }
            }
            //dummy label
            Label lbl = new Label();
            lbl.Parent = main;
            lbl.Size = new Size(50, 150);
            lbl.Location = new Point(50, y);
            lbl.BorderStyle = BorderStyle.None;
        }
        private void loadSave()
        {
            save = new IconButton();
            save.Parent = main;
            save.Size = new Size(200, 50);
            save.Location = new Point(card.Location.X + card.Width + 5, card.Location.Y + (card.Height - 50));
            save.FlatStyle = FlatStyle.Flat;
            save.FlatAppearance.BorderSize = 0;
            save.Text = "Save changes";
            save.IconChar = IconChar.CloudDownloadAlt;
            save.ForeColor = save.IconColor = Color.White;
            save.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            save.TextImageRelation = TextImageRelation.ImageBeforeText;
            save.FlatAppearance.MouseDownBackColor = save.FlatAppearance.MouseOverBackColor = Color.Transparent;

            save.MouseHover += new EventHandler(this.save_Hover);
            save.MouseLeave += new EventHandler(this.save_Leave);
            save.Click += new EventHandler(this.save_Click);
        }
        private void save_Hover(object sender, EventArgs e)
        {
            save.ForeColor = save.IconColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void save_Leave(object sender, EventArgs e)
        {
            save.ForeColor = save.IconColor = Color.White;
        }
        private void save_Click(object sender, EventArgs e)
        {
            constructOptions();
            constructPriceFromCart();
        }
        private void constructPriceFromCart()
        {
            double price = card.Product.Price;
            foreach (OptionBox box in boxes)
            {
                double temp = box.Options[box.Current_Index].Price_Increase;
                price += temp;
            }
            repository.modifyPrice(detail.ID, price * int.Parse(modifier.Current.Text));
            double actual_price = double.Parse(card.Price.Text.Substring(0, card.Price.Text.Length - 2));
            updateOrder((actual_price - start_Price) * int.Parse(modifier.Current.Text));
        }
        private void loadRemove()
        {
            remove = new IconButton();
            remove.Parent = main;
            remove.Size = new Size(150, 50);
            remove.Location = new Point(card.Location.X - 155, card.Location.Y + card.Height - 50);
            remove.Text = "Remove from cart";
            remove.IconChar = IconChar.Trash;
            remove.IconColor = remove.ForeColor = ColorTranslator.FromHtml("#DE354C");
            remove.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            remove.FlatStyle = FlatStyle.Flat;
            remove.TextImageRelation = TextImageRelation.ImageBeforeText;
            remove.FlatAppearance.BorderSize = 0;
            remove.FlatAppearance.MouseDownBackColor = remove.FlatAppearance.MouseOverBackColor = Color.Transparent;

            remove.Click += new EventHandler(this.remove_Click);
        }
        private void remove_Click(object sender, EventArgs e)
        {
            double price = detail.Price;
            Order order = orderRepository.getById(detail.Order_ID);
            repository.deleteOrderDetail(detail.ID);
            repository.getAllOrderDetails();
            if (order.Ammount == detail.Quantity)
            {
                orderRepository.deleteOrder(detail.Order_ID);
                orderRepository.getAllOrders();
                //if (box != null)
                    //box.View.Refresh();
            }
            else
            {
                orderRepository.modifyAmmount(detail.Order_ID, detail.Quantity * (-1));
                orderRepository.modifyPrice(detail.Order_ID, orderRepository.getById(detail.Order_ID).Price - price);
                orderRepository.getAllOrders();
                //if (box != null) box.View.Refresh();
            }
            ViewCart viewCart = new ViewCart(1, orderRepository, repository, mainForm.Size, new ProductRepository());
            viewCart.MainForm = mainForm;
            mainForm.openChild(viewCart, mainForm);
        }
    }
}
