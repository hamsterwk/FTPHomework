using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FTPExplorer
{
    public partial class InputForm : Form
    {
        public InputForm(string Hint="",string warning="")
        {
            InitializeComponent();
            Text = Hint;
            label1.Text = warning;
        }
        private string response;

        public string Response { get => response; set => response = value; }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tmp = textBox1.Text;
            if (Regex.IsMatch(tmp, @"(\\|\||:|"" |\?|<|>)"))
            {
                label1.Visible = true;
                return;
            }
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Visible == true) return;
            response = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
