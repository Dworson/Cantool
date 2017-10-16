using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace sf
{
    public partial class Form3 : Form
    {
        private class Line
        {
           // private DrawImg dImg = new DrawImg();
            public string Str;
            public Color ForeColor;

            public Line(string str, Color color)
            {
                Str = str;
                ForeColor = color;
            }
        };

        ArrayList lines = new ArrayList();
        public Form3()
        {
            InitializeComponent();
        }

        private void zGraph1_Load(object sender, EventArgs e)
        {

        }
    }
}
