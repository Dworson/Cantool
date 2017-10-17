﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;
using System.Drawing;

namespace sf
{
    public partial class Form3 : Form
    {
        ArrayList lines = new ArrayList();

        public CommPort.EventHandler OnStatusChanged { get; private set; }
        public CommPort.EventHandler OnDataReceived { get; private set; }

        public Form3()
        {
            InitializeComponent();
            
        }
        #region **输入数据**
        public List<float> x1 = new List<float>();
        public List<float> y1 = new List<float>();
        public List<float> x2 = new List<float>();
        public List<float> y2 = new List<float>();
        public List<float> x3 = new List<float>();
        public List<float> y3 = new List<float>();
        public List<float> x4 = new List<float>();
        public List<float> y4 = new List<float>();
        #endregion

        private void zGraph1_Load(object sender, EventArgs e)
        {
            CommPort com = CommPort.Instance;
            com.StatusChanged += OnStatusChanged;
            com.DataReceived += OnDataReceived;
            com.Open();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ///串口采样显示[周期k]
            button1.Enabled = false;
            this.Focus();
            textBox1.Text = "";
            int current;
            if (int.TryParse(textBox1.Text.ToString(), out current))
            {
                if (current > 100 && current < 300)
                {
                    timerDraw.Interval = current;
                }
                else
                {
                    textBox1.Text = "2m";
                }
            }
            else
            {
                textBox1.Text = "2m";
            }
            x1.Clear();
            y1.Clear();
            x2.Clear();
            y2.Clear();
            x3.Clear();
            y3.Clear();
            x4.Clear();
            y4.Clear();
            zGraph1.f_ClearAllPix();
            zGraph1.f_reXY();
            //zGraph1.f_LoadOnePix(ref x1, ref y1, System.Drawing.Color.Red, 2);
            //zGraph1.f_AddPix(ref x2, ref y2, System.Drawing.Color.Blue, 3);
            //zGraph1.f_AddPix(ref x3, ref y3, System.Drawing.Color.FromArgb(0, 128, 192), 2);
            zGraph1.f_AddPix(ref x4, ref y4, System.Drawing.Color.Yellow, 3);

            f_timerDrawStart(); //开始TIMER
            //更新按钮显示，表示为正在采样
        }
        private int timerDrawI = 0;
        private void timerDraw_Tick(object sender, EventArgs e)
        {
            ///TIME增加数据
            x1.Add(timerDrawI);
            y1.Add(timerDrawI % 100);
            x2.Add(timerDrawI);
            y2.Add((float)Math.Sin(timerDrawI / 10f) * 200);
            x3.Add(timerDrawI);
            y3.Add(50);
            x4.Add(timerDrawI);
            y4.Add((float)Math.Sin(timerDrawI / 10) * 200);
            timerDrawI++;
            zGraph1.f_Refresh();
            //更新按钮显示，表示为正在采样
            button1.Text += ".";
            if (button1.Text.Length > 22)
            {
                button1.Text = "串口采样正在采样.";
            }
        }
        private void f_timerDrawStart()
        {
            timerDrawI = 0;
            timerDraw.Start();
            textBox1.ReadOnly = true;
            textBox1.ReadOnly = true;
            button1.Enabled = false;
        }
        private void f_timerDrawStop()
        {
            timerDraw.Stop();
            textBox1.ReadOnly = false;
            textBox1.ReadOnly = false;
            button1.Enabled = true;
            button1.Text = "串口采样";
            button1.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
