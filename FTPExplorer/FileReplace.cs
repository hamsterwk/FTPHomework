using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTPExplorer
{
    public partial class FileReplace : Form
    {
        public FileReplace(string fileName="")
        {
            InitializeComponent();
            labelFile.Text = fileName;
        }
        private int status;
        private bool flag;
        public int Status { get => status; set => status = value; }
        public bool Flag { get => flag; set => flag = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            Status = 1;
            flag = checkBox1.Checked;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Status = -1;
            flag = checkBox1.Checked;
            Close();
        }
    }
}
