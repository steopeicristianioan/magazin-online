using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store.view
{
    class PriceSelector : Panel
    {
        private TextBox minPrice;
        private TextBox maxPrice;
        private Label message;

        public TextBox MinPrice { get => this.minPrice; set => this.minPrice = value; }
        public TextBox MaxPrice { get => this.maxPrice; set => this.maxPrice = value; }

        public PriceSelector(Control parent)
        {
            this.Parent = parent;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(300, 88);

            this.Paint += new PaintEventHandler(drawBorder);
        }

        public void Load()
        {
            loadMessage();
            loadInterval();
        }
        private void loadMessage()
        {
            message = new Label();
            message.Parent = this;
            message.Size = new Size(295, 30);
            message.Location = new Point(1, 1);
            message.Text = "Pick your price range! ($)";
            message.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.ForeColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void loadInterval()
        {
            minPrice = new TextBox();
            maxPrice = new TextBox();

            maxPrice.Parent = minPrice.Parent = this;
            minPrice.BorderStyle = maxPrice.BorderStyle = BorderStyle.None;
            minPrice.Size = maxPrice.Size = new Size(130, 50);
            minPrice.Font = maxPrice.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            minPrice.ForeColor = maxPrice.ForeColor = ColorTranslator.FromHtml("#9DC88D");
            minPrice.BackColor = maxPrice.BackColor = this.BackColor;
            minPrice.TabStop = maxPrice.TabStop = false;
            minPrice.TextAlign = maxPrice.TextAlign = HorizontalAlignment.Center;

            minPrice.Location = new Point(1, 50);
            maxPrice.Location = new Point(this.Width - 133, 50);

            minPrice.Text = "200";
            maxPrice.Text = "1000";

            minPrice.Click += new EventHandler(textBox_Click);
            maxPrice.Click += new EventHandler(textBox_Click);

            minPrice.MouseLeave += new EventHandler(textBox_Leave);
            maxPrice.MouseLeave += new EventHandler(textBox_Leave);

        }
        private void textBox_Click(object sender, EventArgs e)
        {
            if (sender.Equals(minPrice) && minPrice.Text == "From: ")
                minPrice.Text = "";
            else if (sender.Equals(maxPrice) && maxPrice.Text == "To: ")
                maxPrice.Text = "";
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            if (sender.Equals(minPrice) && minPrice.Text == "")
                minPrice.Text = "From: ";
            else if (sender.Equals(maxPrice) && maxPrice.Text == "")
                maxPrice.Text = "To: ";
        }
        private void drawBorder(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                ColorTranslator.FromHtml("#F1B24A"),
                ButtonBorderStyle.Solid);
        }
    }
}
