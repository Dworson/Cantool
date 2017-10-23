using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sf
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Time.Text != null)
            {
                //实例化创建对象item  
                ListViewItem item = new ListViewItem();
                //向listView控件的项中添加第一个元素ID  
                item = listView1.Items.Add(Time.Text.Trim()); //个人认为这句类似于数据库的添加主键元素,为了标明到底是哪一行,为后续操作做铺垫  

            }

        }
    }
}
