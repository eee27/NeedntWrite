using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeedntWrite
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 lForm1 = (Form1)this.Owner;//把Form2的父窗口指针赋给lForm1
            lForm1.ReturnValue = Convert.ToInt32(textBox1.Text);
            Close();
        }
    }
}
