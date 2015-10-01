using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                string regular = sr.ReadLine();
                string match = Regex.Match(regular, @"(_+\w*(_*|\d*|\w*)*) | (_*\w+(_*|\d*|\w*)*)").Value;
                label1.Text = match;
                //Example, how to read a file character by character
                /*while (sr.Peek() > 10){
                    Console.WriteLine((char)sr.Read());
                }*/
                //MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }
    }
}
