using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfControlLibrary.Mode;

namespace WpfControlLibrary
{
    /// <summary>
    /// UserCl.xaml 的交互逻辑
    /// </summary>
    public partial class UserCl : UserControl
    {
        UseCMode useCMode = new UseCMode();
        public UserCl()
        {
            InitializeComponent();
            this.DataContext = useCMode;
        }      

        public void setValue(Double dou)
        {
            this.useCMode.MyProperty = dou;
        }
        public Double getValue()
        {
            return this.useCMode.MyProperty;
        }

        public void setMinNum(Double dou)
        {
            this.prx.Minimum = dou;
        }
        public Double getMinNum()
        {
            return this.prx.Minimum;
        }

        public void setMaxNum(Double dou)
        {
            this.prx.Maximum = dou;
        }
        public Double getMaxNum()
        {
            return this.prx.Maximum;
        }
    }
}
