using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Store.view
{
    abstract class View : Form
    {
        protected Panel header;
        protected Panel main;
        protected Panel aside;

        public Panel Header { get => this.header; set => this.header = value; }
        public Panel Main { get => this.main; set => this.main = value; }
        public Panel Aside { get => this.aside; set => this.aside = value; }

        public View()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopLevel = false;

            setCommonProperties();
        }

        private void setCommonProperties()
        {
            header = new Panel();
            main = new Panel();
            aside = new Panel();

            header.Parent = main.Parent = aside.Parent = this;
            header.BorderStyle = main.BorderStyle = aside.BorderStyle = BorderStyle.FixedSingle;
        }

        protected abstract void setHeader();
        protected abstract void setMain();
        protected abstract void setAside();
    }
}
