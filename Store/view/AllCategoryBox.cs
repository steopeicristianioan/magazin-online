using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Store.model;
using Store.repository;

namespace Store.view
{
    class AllCategoryBox : Panel
    {
        private List<RadioButton> categories;
        public List<RadioButton> Categories { get => this.categories; set => this.categories = value; }
        private CategoryRepository categoryRepository;
        private List<Category> all;
        public RadioButton Checked;

        private IconButton show;
        private Label title;
        private Timer timer;

        public AllCategoryBox(Control parent, CategoryRepository categoryRepository)
        {
            this.Parent = parent;
            categories = new List<RadioButton>();
            this.categoryRepository = categoryRepository;
            all = categoryRepository.AllCategories;
            this.Size = new Size(300, 350);
            this.MaximumSize = Size;
            this.MinimumSize = new Size(this.Width, 35);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Paint += new PaintEventHandler(this.colorBorder);
        }

        public void Load()
        {
            loadTitle();
            loadCheckBoxes();
            loadTimer();
            loadShow();
        }
        private void loadTitle()
        {
            title = new Label();
            title.Parent = this;
            title.Text = "Select category";
            title.Size = new Size(175, 30);
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
        private void loadCheckBoxes()
        {
            int x = 10, y = 35;
            foreach(Category category in all)
            {
                RadioButton box = new RadioButton();
                box.Text = category.Name;
                box.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                box.Parent = this;
                box.Location = new Point(x, y);
                box.Width = (this.Width - 20);
                box.Height = 31;
                box.ForeColor = ColorTranslator.FromHtml("#9DC88D");
                box.Click += new EventHandler(this.radioButton_Check);
                y += box.Height + 2;
                categories.Add(box);
            }
        }
        private void loadTimer()
        {
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
        }
        private void show_Click(object sender, EventArgs e)
        {
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if(show.IconChar == IconChar.SortUp)
            {
                while (Size != MinimumSize)
                {
                    this.Height -= 1;
                }
                show.IconChar = IconChar.SortDown;
                show.ImageAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                while (Size != MaximumSize)
                {
                    this.Height += 10;
                }
                show.IconChar = IconChar.SortUp;
                show.ImageAlign = ContentAlignment.TopCenter;
            }
            timer.Stop();
        }
        private void colorBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                ColorTranslator.FromHtml("#F1B24A"),
                ButtonBorderStyle.Solid);
        }
        private void radioButton_Check(object sender, EventArgs e)
        {
            Checked = (RadioButton)sender;
        }
    }
}
