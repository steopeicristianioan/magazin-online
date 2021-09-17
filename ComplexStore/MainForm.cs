using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComplexStore.view;
using FontAwesome.Sharp;

namespace ComplexStore
{
    public partial class MainForm : Form
    {
        private Stack<Form> childs;
        private Form currentChild;
        private Panel main;
        private Panel aside;
        private Panel header;

        public Panel Header { get => this.header; }
        public Panel Main { get => this.main; }
        public Panel Aside { get => this.aside; }
        public Size asideSize;
        public Size mainSize;
        public Size headerSize;

        public MainForm()
        {
            InitializeComponent();
            this.MinimumSize = this.MaximumSize = new Size(1278,678);
            childs = new Stack<Form>();
            headerSize = new Size(this.Width, 75);
            mainSize = new Size(this.Width - 350, this.Height - 75);
            asideSize = new Size(350, this.Height - 75);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            actions();
            this.CenterToScreen();
        }
        private void actions()
        {
            //setCommonPanels();
            //this.Controls.Clear();
            openChild(new ViewProduct(this, this.Size), this);
        }

        private void setCommonPanels()
        {
            this.header = new Panel();
            this.main = new Panel();
            this.aside = new Panel();

            this.header.Parent = this.main.Parent = this.aside.Parent = this;
            this.header.BorderStyle = this.main.BorderStyle = this.aside.BorderStyle = BorderStyle.FixedSingle;

            setHeader();
            setMain();
            setAside();
        }
        private void setHeader()
        {
            header.Size = new Size(this.Width, 75);
            headerSize = header.Size;
            header.Location = new Point(0, 0);
        }
        private void setMain()
        {
            main.Size = new Size(this.Width - 350, this.Height - 75);
            mainSize = main.Size;
            main.Location = new Point(350, 75);
        }
        private void setAside()
        {
            aside.Size = new Size(350, this.Height - 75);
            asideSize = aside.Size;
            aside.Location = new Point(0, 75);
        }

        public void openChild(Form child, Control parent)
        {
            if (currentChild != null) currentChild.Close();
            childs.Push(child);
            currentChild = child;
            child.Parent = parent;
            child.Show();
        }
    }
}
