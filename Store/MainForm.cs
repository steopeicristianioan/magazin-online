using Store.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class MainForm : Form
    {
        private Stack<Form> childs;
        public Form currentChild;
        private Panel main;
        private Panel aside;
        private Panel header;

        public int id;

        public MainForm()
        {
            InitializeComponent();
            //this.MinimumSize = this.MaximumSize = new Size(1278, 678);
            childs = new Stack<Form>();
            this.Size = new Size(600, 500);
            this.Text = "";
            this.Icon = null;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            actions();
            this.CenterToScreen();
        }
        private void actions()
        {
            openChild(new ViewLogin(this, this.Size), this);
        }


        public void openChild(Form child, Control parent)
        {
            this.CenterToScreen();
            if (currentChild != null)
                currentChild.Visible = false;
                //currentChild.Close();
            childs.Push(child);
            currentChild = child;
            currentChild.Parent = parent;
            currentChild.Show();
        }
        public void closeChild(Control parent)
        {
            currentChild.Close();
            childs.Pop();
            currentChild = childs.Peek();
            currentChild.Parent = parent;
            currentChild.Show();
        }
    }
}
