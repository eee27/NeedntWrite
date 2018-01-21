using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Chloe.SQLite;
using Chloe.Core;
using Chloe.Infrastructure;
using Chloe;

namespace NeedntWrite
{

    public partial class Form1 : Form
    {
        int priceOrder = 0;
        int returnValue=0;
        public int ReturnValue { get => returnValue; set => returnValue = value; }

        #region sqlHelper

        public static SQLiteContext SqlHelper()
        {
            string connString = @"Data Source = itemInfo.db";
            SQLiteContext context = new SQLiteContext(new SQLiteConnectionFactory(connString));
            return context;
        }
        #endregion sqlHelper


        #region FormAndButtonAction #1

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(this, null);
            button9_Click(this, null);
        }

        //查找
        private void button1_Click(object sender, EventArgs e)
        {
            string searchStr = textBox1.Text;

            SQLiteContext context = SqlHelper();
            IQuery<Item_Info> q = context.Query<Item_Info>();
            List<Item_Info> resultList = q.Where(a => a.DEC_ITEM_ID > 0).Where(a => a.CHR_ITEM_NAME.Contains(searchStr)).Where(a => a.IS_DELETE == 0).ToList();

            skinDataGridView1.DataSource = resultList.Select(a => new { ID = a.DEC_ITEM_ID, 商品名称 = a.CHR_ITEM_NAME, 规格 = a.CHR_ITEM_SIZE, 单价 = a.REL_ITEM_PRICE }).ToList();
        }

        //排序
        private void button10_Click(object sender, EventArgs e)
        {
            string searchStr = textBox1.Text;

            SQLiteContext context = SqlHelper();
            IQuery<Item_Info> q = context.Query<Item_Info>();
            List<Item_Info> resultList = new List<Item_Info>();
            if (priceOrder == 0)
            {
                 resultList = q.Where(a => a.DEC_ITEM_ID > 0).Where(a => a.CHR_ITEM_NAME.Contains(searchStr)).OrderBy(a => a.REL_ITEM_PRICE).ToList();
                priceOrder+=1;
            }
            else if(priceOrder==1){
                 resultList = q.Where(a => a.DEC_ITEM_ID > 0).Where(a => a.CHR_ITEM_NAME.Contains(searchStr)).OrderByDesc(a=>a.REL_ITEM_PRICE).ToList();
                priceOrder += 1;
            }else if(priceOrder ==2)
            {
                resultList = q.Where(a => a.DEC_ITEM_ID > 0).Where(a => a.CHR_ITEM_NAME.Contains(searchStr)).OrderBy(a=>a.DEC_ITEM_ID).ToList();
                priceOrder =0;
            }

            skinDataGridView1.DataSource = resultList.Select(a => new { ID = a.DEC_ITEM_ID, 商品名称 = a.CHR_ITEM_NAME, 规格 = a.CHR_ITEM_SIZE, 单价 = a.REL_ITEM_PRICE }).ToList();
        }

        //添加
        private void button11_Click(object sender, EventArgs e)
        {
            int selectSize= skinDataGridView1.SelectedRows.Count;
            if (selectSize == 0)
            {
                MessageBox.Show("未选中任何行!选中后再添加!");
            }
            else {
                DataGridViewRow selectRow = skinDataGridView1.CurrentRow;

                Form2 form2 = new Form2();
                form2.Owner = this;
                form2.StartPosition = FormStartPosition.CenterScreen;
                
                
                form2.label2.Text = selectRow.Cells[1].Value.ToString();
                form2.label4.Text = selectRow.Cells[2].Value.ToString();
                form2.label6.Text = selectRow.Cells[3].Value.ToString();
                form2.ShowDialog();

                if (returnValue == 0) { return; }
                else
                {
                    int index = skinDataGridView2.Rows.Add();
                    DataGridViewRow row = skinDataGridView2.Rows[index];


                    row.Cells["ID"].Value = Convert.ToInt32(selectRow.Cells[0].Value.ToString());
                    row.Cells["商品名称"].Value = selectRow.Cells[1].Value.ToString();
                    row.Cells["规格"].Value = selectRow.Cells[2].Value.ToString();
                    row.Cells["单价"].Value = Convert.ToDecimal(selectRow.Cells[3].Value.ToString());
                    row.Cells["数量"].Value = returnValue;
                    row.Cells["总价"].Value = Convert.ToDecimal(selectRow.Cells[3].Value.ToString()) * Convert.ToDecimal(returnValue);

                    CalculateAllItemPrice();
                }
            }
        }













        #endregion FormAndButtonAction #1


        #region  FormAndButtonAction #2
        #endregion  FormAndButtonAction #2

        #region  FormAndButtonAction #3
        #endregion  FormAndButtonAction #3

        #region  FormAndButtonAction #4
        //查找
        private void button9_Click(object sender, EventArgs e)
        {
            string searchStr = textBox2.Text;

            SQLiteContext context = SqlHelper();
            IQuery<Item_Info> q = context.Query<Item_Info>();
            List<Item_Info> resultList = q.Where(a => a.DEC_ITEM_ID > 0).Where(a => a.CHR_ITEM_NAME.Contains(searchStr)).Where(a => a.IS_DELETE == 0).ToList();

            skinDataGridView3.DataSource = resultList.Select(a => new { ID = a.DEC_ITEM_ID, 商品名称 = a.CHR_ITEM_NAME, 规格 = a.CHR_ITEM_SIZE, 单价 = a.REL_ITEM_PRICE,添加时间=a.CHR_CRE_TIME }).ToList();
        }
        //添加
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.StartPosition = FormStartPosition.CenterScreen;
            form3.ShowDialog();

            button9_Click(this, null);
        }
        //更新
        private void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectRow = skinDataGridView3.SelectedRows[0];
            MessageBox.Show(selectRow.Index.ToString());
            Form4 form4 = new Form4();
            form4.itemId = (int)selectRow.Cells[0].Value;
            form4.textBox1.Text = (string)selectRow.Cells[1].Value;
            form4.textBox2.Text = (string)selectRow.Cells[2].Value;
            form4.textBox3.Text = selectRow.Cells[3].Value.ToString();

            form4.StartPosition = FormStartPosition.CenterScreen;
            form4.ShowDialog();

            button9_Click(this, null);
        }
        //删除
        private void button8_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectRow = skinDataGridView3.SelectedRows[0];
           DialogResult result=  MessageBox.Show("确实要删除记录吗?","确认",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                SQLiteContext context = SqlHelper();
                IQuery<Item_Info> q = context.Query<Item_Info>();
                int id = (int)selectRow.Cells[0].Value;
                Item_Info item = q.Where(a => a.DEC_ITEM_ID==id).FirstOrDefault();

                item.IS_DELETE = 1;
                item.CHR_DEL_TIME = DateTime.Now.ToString();
                context.Update(item);
                button9_Click(this, null);
            }
            else { return; }
        }


        #endregion  FormAndButtonAction #4



        /*-------------------------------------------------*/
        #region CommonFunc

        private void CalculateAllItemPrice()
        {
            decimal allItemPriceSum = 0;
            if (skinDataGridView2.Rows.Count > 1)
            {
                foreach (DataGridViewRow row in skinDataGridView2.Rows)
                {
                    allItemPriceSum += Convert.ToDecimal(row.Cells[5].Value);
                }
            }
            label2.Text = allItemPriceSum.ToString();
        }








        #endregion CommonFunc

        
    }



    //sql连接共用
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public SQLiteConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(this._connString);
            return conn;
        }
    }

    
}
