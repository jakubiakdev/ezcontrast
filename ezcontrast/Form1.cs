using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ezcontrast
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
        public Color backgroundcolor;
        public Color foregroundcolor;
        public double contrastratio;

        public Form1()
        {
            InitializeComponent();
        }

        public Color GetColorAt(Point location)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            {
                Point cursor = new Point();
                GetCursorPos(ref cursor);

                backgroundcolor = GetColorAt(cursor);
                this.textBox1.BackColor = GetColorAt(cursor);
                update_contrast();
            }
        }
        void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            {
                Point cursor = new Point();
                GetCursorPos(ref cursor);

                foregroundcolor = GetColorAt(cursor);
                this.textBox1.ForeColor = GetColorAt(cursor);
                update_contrast();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }
        private void update_contrast()
        {
            float bgR;
            float bgG;
            float bgB;
            float fgR;
            float fgG;
            float fgB;
            float bgRL;
            float fgRL;
            //relaive luminance
            if (backgroundcolor.R / 255.0 <= 0.03928) { bgR = (float)(backgroundcolor.R / 255.0 / 12.92); } else { bgR = (float)Math.Pow(((backgroundcolor.R / 255.0 + 0.055) / 1.055), 2.4); }
            if (backgroundcolor.G / 255.0 <= 0.03928) { bgG = (float)(backgroundcolor.R / 255.0 / 12.92); } else { bgG = (float)Math.Pow(((backgroundcolor.G / 255.0 + 0.055) / 1.055), 2.4); }
            if (backgroundcolor.B / 255.0 <= 0.03928) { bgB = (float)(backgroundcolor.R / 255.0 / 12.92); } else { bgB = (float)Math.Pow(((backgroundcolor.B / 255.0 + 0.055) / 1.055), 2.4); }
            bgRL = (float)(0.2126 * bgR + 0.7152 * bgG + 0.0722 * bgB);
            if (foregroundcolor.R / 255.0 <= 0.03928) { fgR = (float)(foregroundcolor.R / 255.0 / 12.92); } else { fgR = (float)Math.Pow(((foregroundcolor.R / 255.0 + 0.055) / 1.055), 2.4); }
            if (foregroundcolor.G / 255.0 <= 0.03928) { fgG = (float)(foregroundcolor.R / 255.0 / 12.92); } else { fgG = (float)Math.Pow(((foregroundcolor.G / 255.0 + 0.055) / 1.055), 2.4); }
            if (foregroundcolor.B / 255.0 <= 0.03928) { fgB = (float)(foregroundcolor.R / 255.0 / 12.92); } else { fgB = (float)Math.Pow(((foregroundcolor.B / 255.0 + 0.055) / 1.055), 2.4); }
            fgRL = (float)(0.2126 * fgR + 0.7152 * fgG + 0.0722 * fgB);

            if (fgRL > bgRL) { contrastratio = (fgRL + 0.05) / (bgRL + 0.05); }
            if (bgRL > fgRL) { contrastratio = (bgRL + 0.05) / (fgRL + 0.05); }
            if (bgRL == fgRL) { contrastratio = 1; }

            label1.Text = "The contrast is  " + contrastratio.ToString("0.##") + ":1";

            if (contrastratio > 4.5) { checkBox1.CheckState = CheckState.Checked; checkBox4.CheckState = CheckState.Checked; } else { checkBox1.CheckState = CheckState.Unchecked; checkBox4.CheckState = CheckState.Unchecked; }
            if (contrastratio > 3.1) { checkBox3.CheckState = CheckState.Checked; checkBox5.CheckState = CheckState.Checked; } else { checkBox3.CheckState = CheckState.Unchecked; checkBox5.CheckState = CheckState.Unchecked; }
            if (contrastratio > 7.1) { checkBox2.CheckState = CheckState.Checked; } else { checkBox2.CheckState = CheckState.Unchecked; }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = trackBar1.Value;
            timer2.Interval = trackBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            Form2 help = new Form2();
            // Show the help form
            help.Show();
        }
    }
}
