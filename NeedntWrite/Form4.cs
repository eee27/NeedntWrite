using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chloe.SQLite;

namespace NeedntWrite
{
    public partial class Form4 : Form
    {
        public int itemId;
        string oldName;
        string oldSize;
        string oldPrice;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            oldName = textBox1.Text;
            oldSize = textBox2.Text;
            oldPrice = textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item_Info itemInfo = new Item_Info();
            itemInfo.DEC_ITEM_ID = itemId;
            itemInfo.CHR_ITEM_NAME = textBox1.Text;
            itemInfo.CHR_ITEM_SIZE = textBox2.Text;
            itemInfo.REL_ITEM_PRICE = Convert.ToDouble(textBox3.Text);
            itemInfo.CHR_APP_TIME = DateTime.Now.ToString();

            SQLiteContext context = Form1.SqlHelper();
            context.Update(itemInfo);

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = oldName;
            textBox2.Text = oldSize;
            textBox3.Text = oldPrice;
        }
    }
}
