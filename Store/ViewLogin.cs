using FontAwesome.Sharp;
using Store.model;
using Store.repository;
using Store.view;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    class ViewLogin : Form
    {
        private CustomerRepository customerRepository = new CustomerRepository();
        private MainForm form;
        public ViewLogin(MainForm parent, Size size)
        {
            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.form = parent;
            this.Parent = parent;
            this.Size = size;
            this.BackColor = ColorTranslator.FromHtml("#164A41");

            Load();
        }

        private TextBox firstName;
        private TextBox password;
        private TextBox lastName;
        private IconButton confirm;
        private Label message;

        public void Load()
        {
            confirm = new IconButton();
            loadMessage();
            loadTextBoxes();
            loadButton();
        }

        private void loadMessage()
        {
            message = new Label();
            message.Parent = this;
            message.Size = new Size(400, 70);
            message.Location = new Point((this.Width - message.Width) / 2, 25);
            message.TextAlign = ContentAlignment.MiddleCenter;
            message.Text = "Insert your data and start shopping!";
            message.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            message.ForeColor = ColorTranslator.FromHtml("#F1B24A");
        }
        private void loadTextBoxes()
        {
            firstName = new TextBox();
            lastName = new TextBox();
            password = new TextBox();

            firstName.Parent = password.Parent = lastName.Parent = this;
            firstName.Width = password.Width = lastName.Width = 250;
            firstName.TextAlign = password.TextAlign = lastName.TextAlign = HorizontalAlignment.Center;
            firstName.Font = password.Font = lastName.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            firstName.BackColor = password.BackColor = lastName.BackColor = this.BackColor;
            firstName.ForeColor = password.ForeColor = lastName.ForeColor = message.ForeColor;
            firstName.BorderStyle = password.BorderStyle = lastName.BorderStyle = BorderStyle.FixedSingle;
            firstName.TabStop = password.TabStop = lastName.TabStop = false;

            firstName.Location = new Point((this.Width - firstName.Width) / 2, 150);
            lastName.Location = new Point((this.Width - firstName.Width) / 2, 170 + firstName.Height);
            password.Location = new Point((this.Width - firstName.Width) / 2, 190 + firstName.Height + lastName.Height);

            password.PasswordChar = '*';

            firstName.Text = "Cristian";//"First Name";
            lastName.Text = "Steopei";//"Last Name";
            password.Text = "cristiSef1";//"Password";

            firstName.Click += new EventHandler(this.textBox_Click);
            lastName.Click += new EventHandler(this.textBox_Click);
            password.Click += new EventHandler(this.textBox_Click);

            firstName.MouseLeave += new EventHandler(this.texBox_Leave);
            lastName.MouseLeave += new EventHandler(this.texBox_Leave);
            password.MouseLeave += new EventHandler(this.texBox_Leave);
        }
        private void textBox_Click(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Equals(firstName) && box.Text == "First Name")
                box.Text = "";
            else if (box.Equals(lastName) && box.Text == "Last Name")
                box.Text = "";
            else if (box.Text == "Password") 
                box.Text = "";
        }
        private void texBox_Leave(object sender, EventArgs e)
        {
            TextBox box = (TextBox)sender;
            if (box.Equals(firstName) && box.Text == "")
                box.Text = "First Name";
            else if (box.Equals(lastName) && box.Text == "")
                box.Text = "Last Name";
            else if (box.Text == "")
                box.Text = "Password";
        }

        private void loadButton()
        {
            confirm = new IconButton();
            confirm.Parent = this;
            confirm.Size = new Size(250, 100);
            confirm.Location = new Point((this.Width - confirm.Width) / 2, password.Location.Y+password.Height + 50);
            confirm.FlatStyle = FlatStyle.Flat;
            confirm.FlatAppearance.BorderSize = 0;
            confirm.TextAlign = ContentAlignment.MiddleCenter;
            confirm.ForeColor = confirm.IconColor = Color.White;
            confirm.Font = message.Font;
            confirm.FlatAppearance.MouseDownBackColor = confirm.FlatAppearance.MouseOverBackColor = Color.Transparent;
            confirm.IconChar = IconChar.DoorOpen;
            confirm.TextImageRelation = TextImageRelation.ImageBeforeText;
            confirm.Text = "Login";

            confirm.MouseHover += new EventHandler(this.button_Hover);
            confirm.MouseLeave += new EventHandler(this.button_Leave);
            confirm.Click += new EventHandler(this.button_Click);
        }
        private void button_Hover(object sender, EventArgs e)
        {
            confirm.ForeColor = confirm.IconColor = message.ForeColor;
        }
        private void button_Leave(object sender, EventArgs e)
        {
            confirm.ForeColor = confirm.IconColor = Color.White;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if(firstName.Text == "" || lastName.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please complete all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Customer customer = customerRepository.getByName(firstName.Text, lastName.Text);
            if(customer == null)
                MessageBox.Show("Make sure that your first name\nand last name are correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(customer.Password != password.Text)
                MessageBox.Show("Make sure your password is correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                form.id = customer.ID;
                form.MinimumSize = form.MaximumSize = form.Size = new Size(1278, 678);
                form.openChild(new ViewProduct(form, form.Size), form);
            }
        }
    }
}
