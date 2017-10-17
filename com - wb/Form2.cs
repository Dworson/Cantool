using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace sf
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            CommPort com = CommPort.Instance;

            int found = 0;
            string[] portList = com.GetAvailablePorts();
            for (int i=0; i<portList.Length; ++i)
            {
                string name = portList[i];
                comboBox1.Items.Add(name);
                if (name == Settings.Port.PortName)
                    found = i;
            }
            if (portList.Length > 0)
                comboBox1.SelectedIndex = found;

            Int32[] baudRates = {
                100,300,600,1200,2400,4800,9600,14400,19200,
                38400,56000,57600,115200,128000,256000,0
            };
            found = 0;
            for (int i=0; baudRates[i] != 0; ++i)
            {
                comboBox2.Items.Add(baudRates[i].ToString());
                if (baudRates[i] == Settings.Port.BaudRate)
                    found = i;
            }
            comboBox2.SelectedIndex = 12;

            comboBox3.Items.Add("5");
            comboBox3.Items.Add("6");
            comboBox3.Items.Add("7");
            comboBox3.Items.Add("8");
            comboBox3.SelectedIndex = Settings.Port.DataBits - 5;

            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                comboBox4.Items.Add(s);
            }
            comboBox4.SelectedIndex = (int)Settings.Port.Parity;

            //停止位选择项
            comboBox5.Items.Add("0");
            comboBox5.Items.Add("1");
            comboBox5.Items.Add("1.5");
            comboBox5.Items.Add("2");
            comboBox5.SelectedIndex = 1;//默认为1

            foreach (string s in Enum.GetNames(typeof(Handshake)))
            {
                comboBox6.Items.Add(s);
            }
            comboBox6.SelectedIndex = (int)Settings.Port.Handshake;

            switch (Settings.Option.AppendToSend)
            {
                case Settings.Option.AppendType.AppendNothing:
                    radioButton1.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendCR:
                    radioButton2.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendLF:
                    radioButton3.Checked = true;
                    break;
                case Settings.Option.AppendType.AppendCRLF:
                    radioButton4.Checked = true;
                    break;
            }

            checkBox1.Checked = Settings.Option.HexOutput;
            checkBox2.Checked = Settings.Option.MonoFont;
            checkBox3.Checked = Settings.Option.LocalEcho;
            checkBox4.Checked = Settings.Option.StayOnTop;
			checkBox5.Checked = Settings.Option.FilterUseCase;

			textBox1.Text = Settings.Option.LogFileName;
		}

		// OK
		private void button1_Click(object sender, EventArgs e)
		{
			Settings.Port.PortName = comboBox1.Text;
			Settings.Port.BaudRate = Int32.Parse(comboBox2.Text);
			Settings.Port.DataBits = comboBox3.SelectedIndex + 5;
			Settings.Port.Parity = (Parity)comboBox4.SelectedIndex;
			Settings.Port.StopBits = (StopBits)comboBox5.SelectedIndex;
			Settings.Port.Handshake = (Handshake)comboBox6.SelectedIndex;

			if (radioButton2.Checked)
				Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCR;
			else if (radioButton3.Checked)
				Settings.Option.AppendToSend = Settings.Option.AppendType.AppendLF;
			else if (radioButton4.Checked)
				Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCRLF;
			else
				Settings.Option.AppendToSend = Settings.Option.AppendType.AppendNothing;

			Settings.Option.HexOutput = checkBox1.Checked;
			Settings.Option.MonoFont = checkBox2.Checked;
			Settings.Option.LocalEcho = checkBox3.Checked;
			Settings.Option.StayOnTop = checkBox4.Checked;
			Settings.Option.FilterUseCase = checkBox5.Checked;

			Settings.Option.LogFileName = textBox1.Text;

			CommPort com = CommPort.Instance;
			com.Open();

			Settings.Write();

			Close();
		}

		// Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

		private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog1 = new OpenFileDialog();

            fileDialog1.Title = "Open config";
            fileDialog1.Filter = "config files (*.txt)|*.txt|All files (*.*)|*.*";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;

            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
				//声明一个文件流
                FileStream fs = new FileStream(fileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                //创建读取器
                StreamReader sr = new StreamReader(fs);

                comboBox1.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox2.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox3.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox4.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox5.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox5.SelectedIndex = Convert.ToInt32(sr.ReadLine());

                radioButton1.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton2.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton3.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton4.Checked = Convert.ToBoolean(sr.ReadLine());

                checkBox1.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox2.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox3.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox4.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox5.Checked = Convert.ToBoolean(sr.ReadLine());

                sr.Close();
                fs.Close();

                Settings.Port.PortName = comboBox1.Text;
                Settings.Port.BaudRate = Int32.Parse(comboBox2.Text);
                Settings.Port.DataBits = comboBox3.SelectedIndex + 5;
                Settings.Port.Parity = (Parity)comboBox4.SelectedIndex;
                Settings.Port.StopBits = (StopBits)comboBox5.SelectedIndex;
                Settings.Port.Handshake = (Handshake)comboBox6.SelectedIndex;

                if (radioButton2.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCR;
                else if (radioButton3.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendLF;
                else if (radioButton4.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCRLF;
                else
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendNothing;

                Settings.Option.HexOutput = checkBox1.Checked;
                Settings.Option.MonoFont = checkBox2.Checked;
                Settings.Option.LocalEcho = checkBox3.Checked;
                Settings.Option.StayOnTop = checkBox4.Checked;
                Settings.Option.FilterUseCase = checkBox5.Checked;

                CommPort com = CommPort.Instance;
                com.Open();

                Settings.Write();
			}
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //保存设定内容
            int portIndex = comboBox1.SelectedIndex;
            int baudIndex = comboBox2.SelectedIndex;
            int dataIndex = comboBox3.SelectedIndex;
            int parityIndex = comboBox4.SelectedIndex;
            int stopIndex = comboBox5.SelectedIndex;
            int hardwareIndex = comboBox6.SelectedIndex;

            bool nothingchecked = radioButton1.Checked;
            bool CRchecked = radioButton2.Checked;
            bool LRchecked = radioButton3.Checked;
            bool CRLFchecked = radioButton4.Checked;

            bool hexchecked = checkBox1.Checked;
            bool Monospacedchecked= checkBox2.Checked;
            bool localchecked = checkBox3.Checked;
            bool staychecked = checkBox4.Checked;
            bool filterchecked = checkBox5.Checked;

            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\config.txt", false);
            sw.WriteLine(portIndex);
            sw.WriteLine(baudIndex);
            sw.WriteLine(dataIndex);
            sw.WriteLine(parityIndex);
            sw.WriteLine(stopIndex);
            sw.WriteLine(hardwareIndex);

            sw.WriteLine(nothingchecked);
            sw.WriteLine(CRchecked);
            sw.WriteLine(LRchecked);
            sw.WriteLine(CRLFchecked);

            sw.WriteLine(hexchecked);
            sw.WriteLine(Monospacedchecked);
            sw.WriteLine(localchecked);
            sw.WriteLine(staychecked);
            sw.WriteLine(filterchecked);

            sw.Close();
            MessageBox.Show("保存成功！", "Save"); 
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string setting = Application.StartupPath + "\\config.txt";
            if (File.Exists(setting))
            {
                //读取设定文件
                StreamReader sr = new StreamReader(setting);
                comboBox1.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox2.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox3.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox4.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox5.SelectedIndex = Convert.ToInt32(sr.ReadLine());
                comboBox5.SelectedIndex = Convert.ToInt32(sr.ReadLine());

                radioButton1.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton2.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton3.Checked = Convert.ToBoolean(sr.ReadLine());
                radioButton4.Checked = Convert.ToBoolean(sr.ReadLine());

                checkBox1.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox2.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox3.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox4.Checked = Convert.ToBoolean(sr.ReadLine());
                checkBox5.Checked = Convert.ToBoolean(sr.ReadLine());

                sr.Close();

                Settings.Port.PortName = comboBox1.Text;
                Settings.Port.BaudRate = Int32.Parse(comboBox2.Text);
                Settings.Port.DataBits = comboBox3.SelectedIndex + 5;
                Settings.Port.Parity = (Parity)comboBox4.SelectedIndex;
                Settings.Port.StopBits = (StopBits)comboBox5.SelectedIndex;
                Settings.Port.Handshake = (Handshake)comboBox6.SelectedIndex;

                if (radioButton2.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCR;
                else if (radioButton3.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendLF;
                else if (radioButton4.Checked)
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendCRLF;
                else
                    Settings.Option.AppendToSend = Settings.Option.AppendType.AppendNothing;

                Settings.Option.HexOutput = checkBox1.Checked;
                Settings.Option.MonoFont = checkBox2.Checked;
                Settings.Option.LocalEcho = checkBox3.Checked;
                Settings.Option.StayOnTop = checkBox4.Checked;
                Settings.Option.FilterUseCase = checkBox5.Checked;      

                CommPort com = CommPort.Instance;
                com.Open();

                Settings.Write();
            }
        }
    }
}