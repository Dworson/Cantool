using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace USB
{
    public partial class Form1 : Form
    {
        public const int WM_DEVICECHANGE = 0x219;
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019;
        public const int DBT_CONFIGCHANGED = 0x0018;
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int DBT_DEVICEREMOVEPENDING = 0x8003;
        public const int DBT_DEVICETYPESPECIFIC = 0x8005;
        public const int DBT_DEVNODES_CHANGED = 0x0007;
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                if (m.Msg == WM_DEVICECHANGE)
                {
                    switch (m.WParam.ToInt32())
                    {
                        case WM_DEVICECHANGE://
                            break;
                        case DBT_DEVICEARRIVAL://U盘插入
                            DriveInfo[] s = DriveInfo.GetDrives();
                            foreach (DriveInfo drive in s)
                            {
                                if (drive.DriveType == DriveType.Removable)
                                {
                                    richTextBox1.AppendText("U盘已插入，盘符为:" + drive.Name.ToString() + "\r\n");
                                    break;
                                }
                            }
                            break;
                        case DBT_CONFIGCHANGECANCELED:
                            MessageBox.Show("2");
                            break;
                        case DBT_CONFIGCHANGED:
                            MessageBox.Show("3");
                            break;
                        case DBT_CUSTOMEVENT:
                            MessageBox.Show("4");
                            break;
                        case DBT_DEVICEQUERYREMOVE:
                            MessageBox.Show("5");
                            break;
                        case DBT_DEVICEQUERYREMOVEFAILED:
                            MessageBox.Show("6");
                            break;
                        case DBT_DEVICEREMOVECOMPLETE: //U盘卸载
                            richTextBox1.AppendText("U盘已卸载，盘符为:");
                            break;
                        case DBT_DEVICEREMOVEPENDING:
                            MessageBox.Show("7");
                            break;
                        case DBT_DEVICETYPESPECIFIC:
                            MessageBox.Show("8");
                            break;
                        case DBT_DEVNODES_CHANGED://可用，设备变化时
                            MessageBox.Show("9");
                            break;
                        case DBT_QUERYCHANGECONFIG:
                            MessageBox.Show("10");
                            break;
                        case DBT_USERDEFINED:
                            MessageBox.Show("11");
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

    }
}