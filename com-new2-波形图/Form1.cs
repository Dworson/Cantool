using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace sf
{
    
    public partial class Form1 : Form
    {
        Series series = new Series("Spline");
        /// <summary>
        /// ����������и����ַ�������ɫ���ࡣ
		/// </summary>
		public class Line
        {
            DBHelper dbHelper = new DBHelper();
            public string Str;
            public Color ForeColor;


            public Line(string str, Color color)
            {
                Str = str;
                ForeColor = color;
            }
        };

        ArrayList lines = new ArrayList();

        Font origFont;
        Font monoFont;
        static int sum = 0;



        public Form1()
        {
            InitializeComponent();

            //���ݿ������ַ���,CanMessage
            string sql1 = string.Format("select MessageName from CanMessage");
            DBHelper db1 = new DBHelper();
            //�ؼ���.DataSource=���ݼ�.���ݱ�
            comboBox3.DataSource = db1.GetDataSet(sql1).Tables[0];
            comboBox3.DisplayMember = "MessageName";
            comboBox3.ValueMember = "MessageName";




            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer2.FixedPanel = FixedPanel.Panel2;

            AcceptButton = button5; //Send
            CancelButton = button4; //Close
            button8.Click += new EventHandler(button8_Click);
            button9.Click += new EventHandler(button9_Click);

            outputList_Initialize();

            Settings.Read();
            TopMost = Settings.Option.StayOnTop;

            // �ñ�ʹ�ö�������
            origFont = Font;
            FontFamily ff = new FontFamily("Courier New");
            monoFont = new Font(ff, 8, FontStyle.Regular);
            Font = Settings.Option.MonoFont ? monoFont : origFont;

            CommPort com = CommPort.Instance;
            com.StatusChanged += OnStatusChanged;
            com.DataReceived += OnDataReceived;
            chart1.Series.Clear();
            series.ChartType = SeriesChartType.Spline;
            chart1.Series.Add(series);
            Control.CheckForIllegalCrossThreadCalls = false;
            com.Open();
        }

        // ����ر�ʱ�رչ����̡߳�
        protected override void OnClosed(EventArgs e)
        {
            CommPort com = CommPort.Instance;
            com.Close();

            base.OnClosed(e);
        }

        /// <summary>
        /// ����ַ�������־�ļ�
        /// </summary>
        /// <param name="stringOut">string to output</param>
        public void logFile_writeLine(string stringOut)
        {
            if (Settings.Option.LogFileName != "")
            {
                Stream myStream = File.Open(Settings.Option.LogFileName,
                    FileMode.Append, FileAccess.Write, FileShare.Read);
                if (myStream != null)
                {
                    StreamWriter myWriter = new StreamWriter(myStream, Encoding.UTF8);
                    myWriter.WriteLine(stringOut);
                    myWriter.Close();
                }
            }
        }

        #region Output window

        string filterString = "";
        bool scrolling = true;
        Color receivedColor = Color.Green;
        Color sentColor = Color.Blue;

        /// <summary>
        ///������ڵ������Ĳ˵�
        /// </summary>
        ContextMenu popUpMenu;

        /// <summary>
        /// ���������Ƿ�ƥ���ַ���
        /// </summary>
        /// <param name="s">string to check</param>
        /// <returns>���ƥ�������Ϊtrue</returns>
        bool outputList_ApplyFilter(String s)
        {
            if (filterString == "")
            {
                return true;
            }
            else if (s == "")
            {
                return false;
            }
            else if (Settings.Option.FilterUseCase)
            {
                return (s.IndexOf(filterString) != -1);
            }
            else
            {
                string upperString = s.ToUpper();
                string upperFilter = filterString.ToUpper();
                return (upperString.IndexOf(upperFilter) != -1);
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        void outputList_ClearAll()
        {
            lines.Clear();
            partialLine = null;

            outputList.Items.Clear();
        }

        /// <summary>
        /// ˢ�������
        /// </summary>
        void outputList_Refresh()
        {
            outputList.BeginUpdate();
            outputList.Items.Clear();
            foreach (Line line in lines)
            {
                if (outputList_ApplyFilter(line.Str))
                {
                    outputList.Items.Add(line);
                }
            }
            outputList.EndUpdate();
            outputList_Scroll();
        }

        internal Line outputList_Add(string str, Color color)
        {
            Line newLine = new Line(str, color);
            lines.Add(newLine);

            if (outputList_ApplyFilter(newLine.Str))
            {
                outputList.Items.Add(newLine);
                outputList_Scroll();
            }

            return newLine;
        }

        /// <summary>
        /// ������������е�һ��
        /// </summary>
        /// <param name="line">line to update</param>
        void outputList_Update(Line line)
        {
            // ����Ӧ����ӵ������
            if (outputList_ApplyFilter(line.Str))
            {
                // ���Ѿ���ʾ������
                bool found = false;
                for (int i = 0; i < outputList.Items.Count; ++i)
                {
                    int index = (outputList.Items.Count - 1) - i;
                    if (line == outputList.Items[index])
                    {
                        // is item visible?
                        int itemsPerPage = (int)(outputList.Height / outputList.ItemHeight);
                        if (index >= outputList.TopIndex &&
                            index < (outputList.TopIndex + itemsPerPage))
                        {
                            // is there a way to refresh just one line
                            // without redrawing the entire listbox?
                            // changing the item value has no effect
                            outputList.Refresh();
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    // not found, so add it
                    outputList.Items.Add(line);
                }
            }
        }

        /// <summary>
        /// ��ʼ�������
        /// </summary>
        private void outputList_Initialize()
        {
            // ������Ϊ�б����ƣ��������ǿ��������ɫ
            outputList.DrawMode = DrawMode.OwnerDrawFixed;
            outputList.DrawItem += new DrawItemEventHandler(outputList_DrawItem);
            outputList.ClearSelected();

            // ����outputList�����Ĳ˵�
            popUpMenu = new ContextMenu();
            popUpMenu.MenuItems.Add("&Copy", new EventHandler(outputList_Copy));
            popUpMenu.MenuItems[0].Visible = true;
            popUpMenu.MenuItems[0].Enabled = false;
            popUpMenu.MenuItems[0].Shortcut = Shortcut.CtrlC;
            popUpMenu.MenuItems[0].ShowShortcut = true;
            popUpMenu.MenuItems.Add("Copy All", new EventHandler(outputList_CopyAll));
            popUpMenu.MenuItems[1].Visible = true;
            popUpMenu.MenuItems.Add("Select &All", new EventHandler(outputList_SelectAll));
            popUpMenu.MenuItems[2].Visible = true;
            popUpMenu.MenuItems[2].Shortcut = Shortcut.CtrlA;
            popUpMenu.MenuItems[2].ShowShortcut = true;
            popUpMenu.MenuItems.Add("Clear Selected", new EventHandler(outputList_ClearSelected));
            popUpMenu.MenuItems[3].Visible = true;
            outputList.ContextMenu = popUpMenu;
        }

        /// <summary>
        /// ����������л�����ɫ
        /// </summary>
        void outputList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0 && e.Index < outputList.Items.Count)
            {
                Line line = (Line)outputList.Items[e.Index];

                // ���ѡ��ʹ�ı���ɫ�ɶ�
                Color color = line.ForeColor;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    color = Color.Black;    // ʹ��ɶ�

                }

                e.Graphics.DrawString(line.Str, e.Font, new SolidBrush(color),
                    e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// ������������ڵĵײ�
        /// </summary>
        void outputList_Scroll()
        {
            if (scrolling)
            {
                int itemsPerPage = (int)(outputList.Height / outputList.ItemHeight);
                outputList.TopIndex = outputList.Items.Count - itemsPerPage;
            }
        }

        /// <summary>
        /// ���������������/���ø���ѡ��
        /// </summary>
        private void outputList_SelectedIndexChanged(object sender, EventArgs e)
        {
            popUpMenu.MenuItems[0].Enabled = (outputList.SelectedItems.Count > 0);
          
            string datanum = CommPort.dataNum;
            dataDecodeing();
            
            //string Stard = datanum.Substring(0, 1);
            //string id = datanum.Substring(1, 3);
            //string dlc = datanum.Substring(4, 1);
            //string data = datanum.Substring(5, 16);
            //System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(phy.);
            //listViewItem1.SubItems.Add(CommPort.dataNum.Substring(1, 3));
            //listViewItem1.SubItems.Add(CommPort.dataNum.Substring(4, 1));
            //listViewItem1.SubItems.Add(CommPort.dataNum.Substring(5, 20));
            //listViewItem1.SubItems.Add(CommPort.dataNum.Substring(6, 1));

            //listView1.Items.Add(listViewItem1);
        }

        /// <summary>
        /// ����������е�ѡ���Ƶ�������
        /// </summary>
        private void outputList_Copy(object sender, EventArgs e)
        {
            int iCount = outputList.SelectedItems.Count;
            if (iCount > 0)
            {
                String[] source = new String[iCount];
                for (int i = 0; i < iCount; ++i)
                {
                    source[i] = ((Line)outputList.SelectedItems[i]).Str;
                }

                String dest = String.Join("\r\n", source);
                Clipboard.SetText(dest);
            }
        }

        /// <summary>
        /// ������������е�������
        /// </summary>
        private void outputList_CopyAll(object sender, EventArgs e)
        {
            int iCount = outputList.Items.Count;
            if (iCount > 0)
            {
                String[] source = new String[iCount];
                for (int i = 0; i < iCount; ++i)
                {
                    source[i] = ((Line)outputList.Items[i]).Str;
                }

                String dest = String.Join("\r\n", source);
                Clipboard.SetText(dest);
            }
        }

        /// <summary>
        /// ѡ����������е�������
        /// </summary>
        private void outputList_SelectAll(object sender, EventArgs e)
        {
            outputList.BeginUpdate();
            for (int i = 0; i < outputList.Items.Count; ++i)
            {
                outputList.SetSelected(i, true);
            }
            outputList.EndUpdate();
        }

        /// <summary>
        /// �����ѡ�����
        /// </summary>
        private void outputList_ClearSelected(object sender, EventArgs e)
        {
            outputList.ClearSelected();
            outputList.SelectedItem = -1;
        }

        #endregion

        #region Event handling - data received and status changed

        /// <summary>
        /// ͨ��ת�����ɴ�ӡ���ַ���׼��������ַ�����
        /// </summary>
        /// <param name="StringIn">input string to prepare.</param>
        /// <returns>output string.</returns>
        private String PrepareData(String StringIn)
        {
            // The names of the first 32 characters
            string[] charNames = { "NUL", "SOH", "STX", "ETX", "EOT",
                "ENQ", "ACK", "BEL", "BS", "TAB", "LF", "VT", "FF", "CR", "SO", "SI",
                "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB",
                "ESC", "FS", "GS", "RS", "US", "Space"};

            string StringOut = "";

            foreach (char c in StringIn)
            {
                if (Settings.Option.HexOutput)
                {
                    StringOut = StringOut + String.Format("{0:X2} ", (int)c);
                }
                else if (c < 32 && c != 9)
                {
                    StringOut = StringOut + "<" + charNames[c] + ">";

                    //Uglier "Termite" style
                    //StringOut = StringOut + String.Format("[{0:X2}]", (int)c);
                }
                else
                {
                    StringOut = StringOut + c;
                }
            }
            return StringOut;
        }

        /// <summary>
        /// AddData�����Ĳ����С�
        /// </summary>
        private Line partialLine = null;

        public object SymbolType { get; private set; }

        /// <summary>
        /// ��������ӵ������
        /// </summary>
        /// <param name="StringIn"></param>
        /// <returns></returns>
        private Line AddData(String StringIn)
        {
            String StringOut = PrepareData(StringIn);

            // if we have a partial line, add to it.
            if (partialLine != null)
            {
                // tack it on
                partialLine.Str = partialLine.Str + StringOut;
                outputList_Update(partialLine);
                return partialLine;
            }

            return outputList_Add(StringOut, receivedColor);
        }

        // ί������Invoke
        internal delegate void StringDelegate(string data);

        /// <summary>
        ///  �������Դ��ж˿ڵ����ݽ����¼���
		/// </summary>
		/// <param name="data">��������</param>
		public void OnDataReceived(string dataIn)
        {
            //Handle multi-threading
            if (InvokeRequired)
            {
                Invoke(new StringDelegate(OnDataReceived), new object[] { dataIn });
                return;
            }

            // ��ͣ�����Լӿ�������
            bool saveScrolling = scrolling;
            scrolling = false;

            // ������Ǽ�⵽һ������ֹ����������ӵ����
            int index;
            while (dataIn.Length > 0 &&
                ((index = dataIn.IndexOf("\r")) != -1 ||
                (index = dataIn.IndexOf("\n")) != -1))
            {
                String StringIn = dataIn.Substring(0, index);
                dataIn = dataIn.Remove(0, index + 1);

                logFile_writeLine(AddData(StringIn).Str);
                partialLine = null; // ��ֹ������
            }

            // ���������ʣ������ݣ���Ӳ�����

            if (dataIn.Length > 0)
            {
                partialLine = AddData(dataIn);
            }

            // restore scrolling
            scrolling = saveScrolling;
            outputList_Scroll();
        }

        /// <summary>
        /// ��������״̬
        /// </summary>
        public void OnStatusChanged(string status)
        {
            // ������߳�

            if (InvokeRequired)
            {
                Invoke(new StringDelegate(OnStatusChanged), new object[] { status });
                return;
            }

            textBox1.Text = status;
        }

        #endregion

        #region User interaction

        /// <summary>
        /// �л�����״̬
        /// </summary>
        private void textBox1_Click(object sender, MouseEventArgs e)
        {
            CommPort com = CommPort.Instance;
            if (com.IsOpen)
            {
                com.Close();
            }
            else
            {
                com.Open();
            }
            outputList.Focus();

        }

        /// <summary>
        /// �ı��˲���
        /// </summary>
        //      private void textBox2_TextChanged(object sender, EventArgs e)
        //      {
        //          filterString = textBox2.Text;
        //	outputList_Refresh();
        //}

        /// <summary>
        /// ��ʾ���öԻ���
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            TopMost = false;

            Form2 form2 = new Form2();
            form2.ShowDialog();

            TopMost = Settings.Option.StayOnTop;
            Font = Settings.Option.MonoFont ? monoFont : origFont;
        }

        /// <summary>
        /// ����������
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            outputList_ClearAll();
        }
        /// <summary>
        /// ��ʾ��������Ϣ
        /// </summary>
        

        /// <summary>
        /// �ر����
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ��������� 0-9 a-f A-F, Ȼ�󷵻�ʮ����������ֵ ?
        /// </summary>
        private static int GetHexDigit(char c)
        {
            if ('0' <= c && c <= '9') return (c - '0');
            if ('a' <= c && c <= 'f') return (c - 'a') + 10;
            if ('A' <= c && c <= 'F') return (c - 'A') + 10;
            return 0;
        }

        /// <summary>
        ///  ConvertEscapeSequences�����Ľ���״̬
        /// </summary>
        public enum Expecting : byte
        {
            ANY = 1,
            ESCAPED_CHAR,
            HEX_1ST_DIGIT,
            HEX_2ND_DIGIT
        };

        /// <summary>
        /// ת������
        /// </summary>
        public string ConvertEscapeSequences(string s)
        {
            Expecting expecting = Expecting.ANY;

            int hexNum = 0;
            string outs = "";
            foreach (char c in s)
            {
                switch (expecting)
                {
                    case Expecting.ANY:
                        if (c == '\\')
                            expecting = Expecting.ESCAPED_CHAR;
                        else
                            outs += c;
                        break;
                    case Expecting.ESCAPED_CHAR:
                        if (c == 'x')
                        {
                            expecting = Expecting.HEX_1ST_DIGIT;
                        }
                        else
                        {
                            char c2 = c;
                            switch (c)
                            {
                                case 'n': c2 = '\n'; break;
                                case 'r': c2 = '\r'; break;
                                case 't': c2 = '\t'; break;
                            }
                            outs += c2;
                            expecting = Expecting.ANY;
                        }
                        break;
                    case Expecting.HEX_1ST_DIGIT:
                        hexNum = GetHexDigit(c) * 16;
                        expecting = Expecting.HEX_2ND_DIGIT;
                        break;
                    case Expecting.HEX_2ND_DIGIT:
                        hexNum += GetHexDigit(c);
                        outs += (char)hexNum;
                        expecting = Expecting.ANY;
                        break;
                }
            }
            return outs;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            string command = comboBox1.Text;
            comboBox1.Items.Add(comboBox1.Text);
            comboBox1.Text = "";
            comboBox1.Focus();

            if (command.Length > 0)
            {
                command = ConvertEscapeSequences(command);

                CommPort com = CommPort.Instance;
                com.Send(command);

                if (Settings.Option.LocalEcho)
                {
                    outputList_Add(command + "\r", sentColor);
                }
            }
        }


        //����app��װ�÷��͵���Ϣ�����ݽ�����
        private void dataEncoding(string mes)
        {
            string a = mes.Substring(0, 1);
            if (a == "T")
            {

            }
            else if (a == "t")
            {
                string b = mes.Substring(0, 4);
                string c = b.Remove(0, 1);

            }
        }

        //16�����ַ���ת2�����ַ���
        static string HexString2BinString(string hexString)
        {
            string result = string.Empty;
            int v = Convert.ToInt32(hexString, 16);
            int v2 = int.Parse(Convert.ToString(v, 2));
            //��λ��0
            result += string.Format("{0:d4} ", v2);
            return result;
        }

        //��cantoolװ�÷�������Ϣ�����ݽ���
        public void dataDecodeing()
        {
            string datanum = CommPort.dataNum;

            //��ȡ��һλ
            string st = datanum.Substring(0, 1);
            //��ʼ��id��data
            string strid = string.Empty;
            string strdata = string.Empty;
            if (st == "T" && datanum.Length==26)
            {
                //T��ͷ��8λid
                strid = datanum.Substring(1, 8);
                strdata = datanum.Substring(10, 16);      
            }
            else
            {
                //��ȡ16����id
                strid = datanum.Substring(1, 3);
                //��ȡ16����data
                strdata = datanum.Substring(5, 16);
            }          
            //ʮ������idתʮ��������
            int id = int.Parse(strid, NumberStyles.HexNumber);
            //��id��ȡ�����signal��name
            ArrayList siglist = new ArrayList();
            siglist = DBHelper.Getsigname(id);
             
            //�Ƚ�data�����ַ�������
            string[] temp = new string[16];
            for (int i = 0; i < 16; i++)
            {
                temp[i] = HexString2BinString(strdata[i].ToString());
                //Console.WriteLine(temp[i]);
            }
            string[] temp2 = new string[8];
            for(int i = 0; i < 8; i++)
            {
                temp2[i] = temp[i*2].Substring(0, 4) + temp[i*2+1];
            } 
            int[,] bindata = new int[8, 8];
            //�ϳ�8*8 int����
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                        bindata[i, j] = (int)temp2[i][j]-48;
                }
            }
           //��ÿ���źŽ��д���
            foreach (string name in siglist)
            {
                //��ȡ��signal����ʼλ
                int start = DBHelper.Getsigstart(name);
                //ת����ʵ����ʼλ��
                if(start>=0 && start <= 7)
                {
                    start = 7 - start;
                }else if(start>=8 && start <= 15)
                {
                    start = 15 - start + 8;
                }else if (start >= 16 && start <= 23)
                {
                    start = 23 - start + 16;
                }else if (start >= 24 && start <= 31)
                {
                    start = 31 - start + 24;
                }else if (start >= 32 && start <= 39)
                {
                    start = 39 - start + 32;
                }else if (start >= 40 && start <= 47)
                {
                    start = 47 - start + 40;
                }else if (start >= 48 && start <= 55)
                {
                    start = 55 - start + 48;
                }else if (start >= 56 && start <= 63)
                {
                    start = 63 - start + 56;
                }
                //��ȡ��ʼλ��Ӧ�±�
                int i = start / 8;
                int j = start - 8 * i;

                //��ȡ��signal�ĳ���
                int len = DBHelper.Getsiglength(name);

                //�ж�1+����0+
                int seq = DBHelper.Getsigseq(name);
                //��ʼ��can�ź�Ϊ��
                string can = string.Empty;
                if (seq == 0)
                {
                    //0+��ȡcan�ź�
                    while (true)
                    {
                        can += bindata[i, j].ToString();
                        if (j == 7)
                        {
                            j = 0;
                            i++;
                        }
                        else
                        {
                            j++;
                        }
                        len--;
                        if (len == 0) break;
                    }
                }
                else
                {
                    //1+��ȡcan�ź�
                    while (true)
                    {
                        can = bindata[i, j].ToString() + can;
                        if (j == 0)
                        {
                            j = 7;
                            i++;
                        }
                        else
                        {
                            j--;
                        }
                        len--;
                        if (len == 0) break;
                    }
                }
                //can�ź�תΪ10����
                long cansig = Convert.ToInt64(can,2);

                //��ȡA,Bֵ��phy = can�ź� * A + B
                double A = DBHelper.GetsiglA(name);
                double B = DBHelper.GetsiglB(name);
                double phy = cansig * A + B;
                //��ȡ����ֵ��λ
                string unit = DBHelper.Getsiglunit(name);
                //MessageBox.Show(phy.ToString());
                //��ӵ�listview
                System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(id.ToString());
                listViewItem1.SubItems.Add(name);
                listViewItem1.SubItems.Add(can);
                listViewItem1.SubItems.Add(phy.ToString()+unit);
                listView1.Items.Add(listViewItem1);
                series.Points.AddY(phy);
            }
           

        }

        /// <summary>
        /// ��˿ڷ����ļ�
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.RestoreDirectory = false;
            dialog.Title = "Select a file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String text = System.IO.File.ReadAllText(dialog.FileName);

                CommPort com = CommPort.Instance;
                com.Send(text);

                if (Settings.Option.LocalEcho)
                {
                    outputList_Add("SendFile " + dialog.FileName + "," +
                        text.Length.ToString() + " byte(s)\n", sentColor);
                }
            }
        }

        /// <summary>
        /// toggle scrolling
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            scrolling = !scrolling;
            outputList_Scroll();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            TopMost = false;

            Form3 form3 = new Form3();
            form3.ShowDialog();

            TopMost = Settings.Option.StayOnTop;
            Font = Settings.Option.MonoFont ? monoFont : origFont;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TopMost = false;

            Form4 form4 = new Form4();
            form4.ShowDialog();

            TopMost = Settings.Option.StayOnTop;
            Font = Settings.Option.MonoFont ? monoFont : origFont;
        }



        #endregion


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void axiXYPlotX1_OnClick(object sender, EventArgs e)
        {

        }


        //Message���ݰ�



        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text1 = comboBox3.SelectedValue.ToString();


            string sql2 = string.Format("select * from CanSignal where ID=(select ID from CanMessage where MessageName = '{0}');", text1);
            DBHelper db2 = new DBHelper();
            //�ؼ���.DataSource=���ݼ�.���ݱ�
            comboBox2.DataSource = db2.GetDataSet(sql2).Tables[0];
            comboBox2.DisplayMember = "SignalName";
            comboBox2.ValueMember = "SignalName";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text1 = comboBox2.SelectedValue.ToString();

            //��data���������ʾӦ�������ֵ�ķ�Χ
            string sql1 = string.Format("select C from CanSignal where SignalName= '{0}';", text1);
            string sql2 = string.Format("select D from CanSignal where SignalName= '{0}';", text1);
            DBHelper db2 = new DBHelper();
            //����ֵ�����ҷ�Χ
            float c = db2.SelectCInMessage(sql1);
            float d = db2.SelectCInMessage(sql2);
            richTextBox1.Text = "��Χ��" + c.ToString() + '-' + d.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        //����ֵת��ΪCan�ź�
        public int PsyToCanMessage(int psy, string text)
        {
            int can;
            string sql = string.Format("select B from CanSignal where SignalName= '{0}';", text);
            string sql1 = string.Format("select A from CanSignal where SignalName= '{0}';", text);


            DBHelper db = new DBHelper();
            float b = System.Convert.ToSingle(db.SelectCInMessage(sql));
            float a = System.Convert.ToSingle(db.SelectCInMessage(sql1));

            can = (int)((psy - b) / a);
            return can;
        }

        //������д�����鲢ת��Ϊ16���ƣ����������ִ���ʽ,�˴�Ϊ0+
        private string BtoH(string b, string text)
        {
            string[,] Array = new string[8, 8]; //ע���ά���������
            int can;
            string sql = string.Format("select C from CanSignal where SignalName= '{0}';", text);
            string sql1 = string.Format("select D from CanSignal where SignalName= '{0}';", text);


            DBHelper db = new DBHelper();
            int c = System.Convert.ToInt32(db.SelectCInMessage(sql));
            int d = System.Convert.ToInt32(db.SelectCInMessage(sql1));

            //����Ϊ����Ĵ洢,
            //�Զ���0
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Array[i, j] = "0";
                }
            //����λ�滻
            //��ʼ������λ��,�ַ�����λ��0������д�뻹���ɸ�λд,����Ӧ�û�ȡ���һλ�����ƵĲ���λ�ã����Ų�
            //string str = b.ToString();
            string str = b;
            int l = b.Length;
            int k = c + d;
            int m = k % 8;
            int n = k / 8;
            int q = c / 8;
            int p = c % 8;

            //ֻ�ǲ�����Чλ��

            for (int j = 0; j <= m && l > 0; j++)
            {
                Array[n, j] = str.Substring(l - 1, 1);
                l = l - 1;
            }
            n = n - 1;
            if ((n - q) > 0)
            {
                for (int i = n - 1; n > q; i++)
                {
                    for (int j = 0; j <= 8 && l > 0; j++)
                    {
                        if (l > 0)
                        {
                            Array[i, j] = str.Substring(l - 1, 1);
                            l = l - 1;
                        }
                        //else
                        //{
                        //    Array[i, j] = "0";
                        //}
                    }
                    n = n - 1;
                }

            }
            else if (n == q)
            {
                for (int j = 0; j <= p && l > 0; j++)
                {
                    Array[n, j] = str.Substring(l - 1, 1);
                    l = l - 1;
                }
            }
            //�����鴢��Ϊһ���ַ���
            //string count = "";
            int count = 2;
            //int sum ;
            int sumfinal;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    int a = System.Convert.ToInt32(Array[i, j]);
                    sum = sum * 2 + a;
                    //sumfinal = sum;
                    //count = count+ Array[i, j] ;
                }
            //long a = long.Parse(count);
            //long v = Convert.ToInt64(count);
            //������ôֱ��ת��ϵͳ����֪������ַ����Ǳ�ʾ���Ƕ�����
            string sumlast = Convert.ToString(sum, 16);
            return sumlast;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string cmd1 = comboBox3.Text;

            string sql2 = string.Format("select ID from CanMessage where MessageName = '{0}';", cmd1);
            DBHelper db2 = new DBHelper();

            int id = db2.SelectIdInMessage(sql2);
            string iD = id.ToString();

            //����Signal����Ϣȷ�����͵����ݵ�λ��

            string cmd2 = comboBox2.Text;
            string sql3 = string.Format("select ID from CanMessage where MessageName = '{0}';", cmd1);



            string cmd3 = domainUpDown1.Text;

            //�����Ƶ�ת��output
            int cmd4 = System.Convert.ToInt32(richTextBox1.Text);
            int mid = PsyToCanMessage(cmd4, cmd2);
            string output = Convert.ToString(mid, 2);
            output = BtoH(output, cmd2).ToString();


            //int signal1 = System.Convert.ToInt32(cmd4);
            //int signal2 = Convert.ToString(Convert.ToInt32(cmd4, 2), 2);
            //int signal2 = System.Convert.ToString(cmd4, 2);

            string cmd5 = textBox3.Text;

            string cmd = 't' + iD + cmd3 + output + cmd5;

            //comboBox1.Items.Add(comboBox1.Text);
            //comboBox1.Text = "";
            //comboBox1.Focus();

            if (cmd.Length > 0)
            {
                //cmd1 = form1.ConvertEscapeSequences(cmd1);

                CommPort com = CommPort.Instance;
                com.Send(cmd);

                if (Settings.Option.LocalEcho)
                {
                    outputList_Add(cmd + "\r", sentColor);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

       
    }
}
