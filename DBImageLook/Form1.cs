using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace DBImageLook
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private byte[] HexStringToBytes(string hex)
        {
            byte[] data = new byte[hex.Length /2];
            int j = 0;
            for (int i = 0; i < hex.Length; i+=2)
            {
               data[j] = Convert.ToByte(hex.Substring(i, 2), 16);
               ++j;
            }
            return data;
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(tbRawData.Text, "^(0x)?[a-f0-9]+$", RegexOptions.IgnoreCase)) { 
                var hex = tbRawData.Text.Substring(2);
                if (hex.Length % 2 == 1) hex = hex.Substring(0, hex.Length - 1);

                Bitmap bmp = new Bitmap(new MemoryStream(HexStringToBytes(hex)));
                pictureBox1.Image = bmp;
            } else {
                MessageBox.Show("Invalid Image Data, must be hexadecimal string, optionally prepended with 0x");
            }
        }

        private void tbRawData_TextChanged(object sender, EventArgs e)
        {
            linkLabel1.Visible = (tbRawData.Text.Length > 0);
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tbRawData.Text = "";
            linkLabel1.Visible = false;
        }
    }
}
