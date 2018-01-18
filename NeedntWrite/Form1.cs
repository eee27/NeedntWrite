using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace NeedntWrite
{
    public partial class Form1 : Form
    {
        SQLiteConnection sqlConn;
        SQLiteDataAdapter sqlAdapter;
        DataTable dataTable;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //连接数据库.
            string mDbPath = Application.StartupPath + "/itemInfo.db";
            //如果数据库不存在则自动创建.
            sqlConn = new SQLiteConnection("Data Source=" + mDbPath);
            //打开数据库文件.
            sqlConn.Open();
            //创建表[Test Table].
            // id        - Unique Counter - Key Field (Required in any table)
            // FirstName - Text
            // Age       - Integer
            using (SQLiteCommand sqlCmd = new SQLiteCommand
                ("CREATE TABLE IF NOT EXISTS [item_info] " +
                 "(id INTEGER PRIMARY KEY AUTOINCREMENT, 'FirstName' TEXT, 'Age' INTEGER);",
                 sqlConn))
            {
                sqlCmd.ExecuteNonQuery();
            }

            //获取数据库中表
            //表 "Tables"中字段 "TABLE_NAME" 包含所有表名信息
            using (DataTable dataTable = sqlConn.GetSchema("Tables")) 
                // "Tables"包含系统表详细信息
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dataTable.Rows[i].ItemArray[dataTable.Columns.IndexOf("TABLE_NAME")].ToString());
                }
                if (comboBox1.Items.Count > 0)
                {
                    comboBox1.SelectedIndex = 1; // 默认选中第一张表
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlAdapter = new SQLiteDataAdapter("SELECT * FROM [" + comboBox1.Text + "]", sqlConn);
            dataTable = new DataTable(); // Don't forget initialize!
            sqlAdapter.Fill(dataTable);

            // 绑定数据到DataGridView
            dataGridView1.DataSource = dataTable;
        }
    }
}
