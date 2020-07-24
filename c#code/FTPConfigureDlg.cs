using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
namespace FTPExplorer
{
    public class FTPConfigureDlg : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button m_bnOK;
        private System.Windows.Forms.Button m_bnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox m_cBServerOS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbFTPPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown m_nUDSendTimeOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown m_nUDRecvTimeOut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton m_rBASCIIMode;
        private System.Windows.Forms.RadioButton m_rBBinaryMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.IContainer components;
        public FTPProperties m_FTPProperties;
        public int FTPPort
        {
            get
            {
                try
                {
                    int l_iPort = int.Parse(m_tbFTPPort.Text);
                    return l_iPort;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public int SendTimeOut
        {
            get
            {
                try
                {
                    return (int)m_nUDSendTimeOut.Value;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public int RecvTimeOut
        {
            get
            {
                try
                {
                    return (int)m_nUDRecvTimeOut.Value;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public FTPConfigureDlg(bool l_bStatus, FTPProperties l_FTPProperties)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            m_FTPProperties = l_FTPProperties;
            if (l_bStatus == true)
            {
                m_tbFTPPort.Enabled = true;
                m_cBServerOS.SelectedIndex = 2;
            }
            else
            {
                m_tbFTPPort.Enabled = false;
                m_cBServerOS.Enabled = false;
            }
            m_rBASCIIMode.Checked = true;
            m_rBBinaryMode.Enabled = false;
            m_nUDSendTimeOut.Enabled = true;
            m_nUDRecvTimeOut.Enabled = true;
            m_tbFTPPort.Text = m_FTPProperties.Port.ToString();
            m_cBServerOS.SelectedIndex = m_FTPProperties.ServerTypeIndex;
            m_nUDSendTimeOut.Value = m_FTPProperties.SendTimeOut;
            m_nUDRecvTimeOut.Value = m_FTPProperties.RecvTimeOut;
        }
        private void OnClickOK(object sender, System.EventArgs e)
        {
            try
            {
                int l_iPort = int.Parse(m_tbFTPPort.Text);
                if (l_iPort < 1)
                {
                    throw (new Exception("Invalid Port number entered. Enter a positive value"));
                }
                m_FTPProperties.ServerType = m_cBServerOS.SelectedText;
                m_FTPProperties.Port = l_iPort;
                m_FTPProperties.SendTimeOut = (int)m_nUDSendTimeOut.Value;
                m_FTPProperties.RecvTimeOut = (int)m_nUDRecvTimeOut.Value;
                if (m_rBASCIIMode.Checked == true)
                {
                    m_FTPProperties.TransferMode = 'A';
                }
                else
                {
                    m_FTPProperties.TransferMode = 'B';
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, " Server Configuration-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_tbFTPPort.Focus();
            }
        }
        private void OnClickCancel(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
