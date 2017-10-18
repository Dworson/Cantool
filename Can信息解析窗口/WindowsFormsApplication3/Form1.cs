using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        private ImageList imageList1;

        public Form1()
        {
            InitializeComponent();
        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listView1.SmallImageList = this.imageList1; //将listView的图标集与imageList1绑定
            ColumnHeader ch = new ColumnHeader();

            ch.Text = "列标题1";   //设置列标题

            ch.Width = 120;    //设置列宽度

            ch.TextAlign = HorizontalAlignment.Left;   //设置列的对齐方式

            this.listView1.Columns.Add(ch);    //将列头添加到ListView控件。
            
        }

    }
}
