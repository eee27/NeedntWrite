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



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connString = @"Data Source = itemInfo.db";
            SQLiteContext context = new SQLiteContext(new SQLiteConnectionFactory(connString));
            IQuery<Item_Info> q = context.Query<Item_Info>();
            Item_Info result=q.Where(a => a.DEC_ITEM_ID == 1).FirstOrDefault();
            MessageBox.Show(result.CHR_ITEM_NAME);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }







    }




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
