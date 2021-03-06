﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace sf
{
    public partial class Form4 : Form
    {
        private DataTable _dtEmployees;
        public Form4()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this._dtEmployees = this.CreateDataTable();

            //调用BuildTree方法构建TreeView。
            tree.BuildTree(
                this._dtEmployees, this.treeView1,
                true, "LastName", "EmployeeID", "ReportsTo");
        }

        string[] id = new string[100];
        string[,] signal = new string[100, 1000];
        int m = 0, n = 0;
        public DataTable CreateDataTable()
        {
            FileStream fsRead = new FileStream("F:\\", FileMode.Open);//此处需要添加显示的文件
            StreamReader reader = new StreamReader(fsRead);
            string str;
            do
            {
                str = reader.ReadLine();
                if (str == null)
                    break;
                int i, j;
                if (str[0].Equals('B'))
                {
                    i = str.IndexOf("BO_ ");   //索引为0
                    j = str.IndexOf(" ", 4, 9);//索引为7
                    id[m] = str.Substring(i + 4, j - i - 4);
                    // MessageBox.Show(id[m]);
                    m++;
                    n = 0;

                }
                if (str.Length > 1 && str[1].Equals('S'))
                {
                    i = str.IndexOf("_", 4, 9);//索引为8
                    j = str.IndexOf(":");
                    signal[m, n] = str.Substring(i + 1, j - i - 1);
                    //   MessageBox.Show(signal[m, n]);
                    n++;
                }
            }
            while (str != null);

            DataTable dataTable = new DataTable();

            // The value in this column will identify the TreeNode
            dataTable.Columns.Add("EmployeeID");

            // The value in this column will display on the TreeNode
            dataTable.Columns.Add("LastName");

            // The value in this column will identify its parentId 
            dataTable.Columns.Add("ReportsTo");

            // Fill the DataTable
            dataTable.Rows.Add(0, "read data", DBNull.Value);
            for (int i = 0; id[i] != null; i++)
            {
                dataTable.Rows.Add(i + 1, id[i], 0);
            }

            int p = m + 1;
            for (int i = 0; id[i] != null; i++)
            {
                for (int j = 0; signal[i + 1, j] != null; j++)
                {
                    dataTable.Rows.Add(p, signal[i + 1, j], i + 1);
                    p++;
                }

            }
            return dataTable;
        }
    }
}
