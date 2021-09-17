using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Store.model;

namespace Store.view
{
    class FilterBox : Panel
    {
        private string filterName;
        public string FilterName { get => this.filterName; }
        private int type;
        public int Type { get => this.type; }
        private int identifierIndex;
        private List<Option> options;

        private Label name;
        private IconButton show;
        private ComboBox from;
        private ComboBox to;
        private List<CheckBox> boxes;
        //private List<int> optionIDS;
        //public List<int> OptionsIDS { get => this.optionIDS; }
        private int[] ids;
        public int[] IDS { get => this.ids; }

        public int StartID = -1;
        public int EndID = -1;
        public int Selected = 0;
        public bool ComboOK = true;

        public FilterBox(Control parent, string name, int type, List<Option> options, int index)
        {
            this.Parent = parent;
            filterName = name;
            this.type = type;
            this.options = options;
            this.identifierIndex = index;
            if(type == 1) ids = new int[options.Count];
            else ids = new int[200];
            StartID = options[0].ID;
            EndID = options[options.Count - 1].ID;

            this.Size = new Size(300, 150);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MaximumSize = Size;
            this.MinimumSize = new Size(300, 35);
            //this.AutoScroll = true;

            this.Paint += new PaintEventHandler(this.colorBorder);
        }
        public void Load()
        {
            if (type == 1)
            {
                loadMessages();
                loadComboBoxes();
                this.Paint += new PaintEventHandler(this.drawComboBoxBorders);
            }
            else loadBoxes();
            loadName();
            loadShow();
        }

        private void loadName()
        {
            name = new Label();
            name.Parent = this;
            name.Size = new Size(150, 30);
            name.Location = new Point(1, 1);
            name.TextAlign = ContentAlignment.MiddleCenter;
            name.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            name.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            name.Text = filterName;
        }
        private void loadShow()
        {
            show = new IconButton();
            show.TabStop = false;
            show.Parent = this;
            show.Size = new Size(30, 30);
            show.IconChar = IconChar.SortUp;
            show.Location = new Point(name.Width + 5, 1);
            show.IconColor = ColorTranslator.FromHtml("#F1b24A");
            show.FlatStyle = FlatStyle.Flat;
            show.FlatAppearance.BorderSize = 0;
            show.ImageAlign = ContentAlignment.TopCenter;
            show.FlatAppearance.BorderColor = this.BackColor;
            show.FlatAppearance.MouseDownBackColor = show.FlatAppearance.MouseOverBackColor = Color.Transparent;
            show.Click += new EventHandler(this.show_Click);
        }
        private void show_Click(object sender, EventArgs e)
        {
            if (show.IconChar == IconChar.SortUp)
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
        private void colorBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                ColorTranslator.FromHtml("#F1B24A"),
                ButtonBorderStyle.Solid);
        }

        private void loadMessages()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(3, 40);
            label.Size = new Size(this.Width - 6, 25);
            label.Text = "     From:                    To: ";
            label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            label.ForeColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void loadComboBoxes()
        {
            from = new ComboBox();
            to = new ComboBox();

            from.Parent = to.Parent = this;
            from.Width = to.Width = (this.Width - 25) / 2;
            from.ForeColor = to.ForeColor = ColorTranslator.FromHtml("#F1B24A");
            from.Font = to.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            from.BackColor = to.BackColor = this.BackColor;
            from.TabStop = to.TabStop = false;
            from.DropDownStyle = to.DropDownStyle = ComboBoxStyle.DropDownList;

            from.Location = new Point(2, 70);
            to.Location = new Point(this.Width - to.Width, 70);

            from.Text = options[1].Name.Remove(options[1].Name.IndexOf(filterName), filterName.Length);
            to.Text = options[options.Count - 1].Name.Remove(options[options.Count - 1].Name.IndexOf(filterName), filterName.Length);

            from.SelectedIndexChanged += delegate (object sender2, EventArgs e2) { item_Click(sender2, e2, 1); };
            to.SelectedIndexChanged += delegate (object sender2, EventArgs e2) { item_Click(sender2, e2, 2); };

            int i = 0;
            foreach (Option option in options)
            {
                string name = option.Name.Remove(option.Name.IndexOf(filterName), filterName.Length);
                from.Items.Add(name);
                to.Items.Add(name);
                ids[i++] = option.ID;
            }
        }
        private void drawComboBoxBorders(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(ColorTranslator.FromHtml("#9DC88D"), 3);
            Rectangle border1 = new Rectangle(0, 68, from.Width + 3, from.Height + 3);
            Rectangle border2 = new Rectangle(to.Location.X - 2, 68, from.Width + 3, from.Height + 3);
            e.Graphics.DrawRectangle(pen, border1);
            e.Graphics.DrawRectangle(pen, border2);
        }
        private void item_Click(object sender, EventArgs e, int FromOrTo)
        {
            if (FromOrTo == 1)
            {
                StartID = ids[from.SelectedIndex];
            }
            else EndID = ids[to.SelectedIndex];
            if (StartID > EndID)
                ComboOK = false;
            else ComboOK = true;
        }
        private void loadBoxes()
        {
            boxes = new List<CheckBox>();
            int x = 3, y = 40, ct = 0;
            foreach(Option option in options)
            {
                CheckBox box = new CheckBox();
                box.Parent = this;
                box.Width = (this.Width - 25) / 2;
                box.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                box.ForeColor = ColorTranslator.FromHtml("#9DC88D");
                int start = option.Name.IndexOf(filterName);
                box.Text = option.Name.Remove(start, filterName.Length);
                box.BackColor = this.BackColor;
                if (ct % 2 == 0) {
                    box.Location = new Point(x, y);
                }
                else
                {
                    box.Location = new Point(this.Width - box.Width - 5, y);
                    y += box.Height + 2;
                }
                ct++;
                int auxID = option.ID;
                box.CheckedChanged += delegate (object sender2, EventArgs e2) { checkBox_Click(sender2, e2, auxID, auxID); };
                boxes.Add(box);
            }
        }
        private void checkBox_Click(object sender, EventArgs e, int position, int id)
        {
            CheckBox box = (CheckBox)sender;
            if (box.Checked)
            {
                Selected++;
                //MessageBox.Show(id.ToString());
                ids[position] = id;
            }
            else
            {
                ids[position] = 0;
                Selected--;
            }
        }
    }

}
