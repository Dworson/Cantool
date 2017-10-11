using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sf
{
    public partial class Form3 : Form
    {
        private class Line
        {
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

        public Form3()
        {
            InitializeComponent();
            //AcceptButton = button5; //Send
            CancelButton = button4; //Close
            button4.Click += new EventHandler(button4_Click);
           //utputList_Initialize();

        }
        private void button2_Click(object sender, EventArgs e)
        {
          //outputList_ClearAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
