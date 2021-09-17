using ComplexStore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ComplexStore.view
{
    class OptionBox : Panel
    {
        private List<Option> options;
        private string title;
        private int current_Index;
        private double def_price;

        private Label lblTitle;
        private List<Label> lblOptions;
        private Font font = new Font("Segoe UI", 8, FontStyle.Bold);
        private Label priceTextBox;

        public OptionBox(Panel parent, List<Option> options, string title, Label price)
        {
            this.Parent = parent;
            this.options = options;
            this.title = title;
            this.priceTextBox = price;
            def_price = double.Parse(price.Text.Substring(0, price.Text.Length - 2));
            this.Size = new Size(250, 100);
        }
        public void SetLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        public void Load()
        {
            loadTitle();
            loadOptions();
        }
        private void loadTitle()
        {
            lblTitle = new Label();
            lblTitle.Parent = this;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Size = new Size(100, 20);
            lblTitle.Font = font;
            lblTitle.Text = title;
            lblTitle.ForeColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void loadOptions()
        {
            lblOptions = new List<Label>();
            int x = 10, y = 30, ct = 1;
            foreach (Option op in options)
                op.Price_Increase -= options[0].Price_Increase;
            for(int i = 0; i<options.Count; i++)
            {
                Option option = options[i];
                Label temp = new Label();
                temp.Parent = this;
                temp.Location = new Point(x, y);
                temp.Size = new Size(option.Name.Length * 7, 20);
                temp.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                temp.Text = option.Name.Remove(option.Name.IndexOf(title), title.Length);
                temp.TextAlign = ContentAlignment.MiddleCenter;
                temp.BorderStyle = BorderStyle.FixedSingle;
                temp.ForeColor = ColorTranslator.FromHtml("#9DC88D");
                int aux = i;
                temp.Click += delegate (object sender2, EventArgs e2) { option_Click(sender2, e2, aux); };
                temp.MouseHover += delegate (object sender2, EventArgs e2) { option_Hover(sender2, e2, aux); };
                temp.MouseLeave += delegate (object sender2, EventArgs e2) { option_Leave(sender2, e2, aux); };
                lblOptions.Add(temp);
                if (i+1<options.Count && x + option.Name.Length * 7 + 5 + options[i+1].Name.Length * 6 + 7 > 200)
                {
                    x = 10;
                    y += 25;
                    ct++;
                }
                else x += option.Name.Length * 7 + 5;
            }
            this.Height = 30 + 30 * ct;
            current_Index = 0;
            lblOptions[current_Index].ForeColor = lblTitle.ForeColor;
        }
        private void option_Click(object sender, EventArgs e, int pos)
        {
            lblOptions[pos].Text = options[pos].Name.Remove(options[pos].Name.IndexOf(title), title.Length);
            for (int i = 0; i < options.Count; i++)
                if (i != pos)
                    lblOptions[i].ForeColor = ColorTranslator.FromHtml("#9DC88D");
            lblOptions[pos].ForeColor = lblTitle.ForeColor;
            double newPrice = double.Parse(priceTextBox.Text.Substring(0, priceTextBox.Text.Length - 2));
            if(pos != 0) newPrice += options[pos].Price_Increase - options[0].Price_Increase;
            if(current_Index != 0) newPrice -= options[current_Index].Price_Increase;
            current_Index = pos;
            priceTextBox.Text = newPrice.ToString() + " $";
        }
        private void option_Hover(object sender, EventArgs e, int pos)
        {
            if (lblOptions[pos].ForeColor != ColorTranslator.FromHtml("#F1B24A"))
            {
                string start = options[pos].Price_Increase >= options[current_Index].Price_Increase ? "+" : "-";
                lblOptions[pos].Text = start + Math.Abs(options[pos].Price_Increase - options[current_Index].Price_Increase) + "$";
                lblOptions[pos].ForeColor = Color.White;
            }
        }
        private void option_Leave(object sender, EventArgs e, int pos)
        {
            lblOptions[pos].Text = options[pos].Name.Remove(options[pos].Name.IndexOf(title), title.Length);
            lblOptions[pos].ForeColor = ColorTranslator.FromHtml("#9DC88D");
            if(pos == current_Index)
            {
                lblOptions[pos].ForeColor = lblTitle.ForeColor;
            }
        }
    }
}
