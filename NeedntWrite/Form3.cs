using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chloe;
using Chloe.SQLite;

namespace NeedntWrite
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length > 0 && textBox2.Text.Trim().Length > 0 && textBox3.Text.Trim().Length > 0)
            {
                string itemName = textBox1.Text;
                string itemSize = textBox2.Text;
                double itemPrice = Convert.ToDouble(textBox3.Text);
                string currTime = DateTime.Now.ToString();

                SQLiteContext context = Form1.SqlHelper();

                Item_Info itemInfo = new Item_Info();
                itemInfo.CHR_ITEM_NAME = itemName;
                itemInfo.CHR_ITEM_SIZE = itemSize;
                itemInfo.REL_ITEM_PRICE = itemPrice;
                itemInfo.CHR_CRE_TIME = currTime;
                context.Insert(itemInfo);
                Close();
            }
            else {
                MessageBox.Show("所有输入都不能为空!重新输入!");
            }
        }
    }
}
