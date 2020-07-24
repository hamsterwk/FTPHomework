using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
namespace FTPExplorer
{
/// < summary >
/// Summary description for FTPConnectDlg.
/// < /summary >
public class FTPConnectDlg : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tbFTPServer;
        private System.Windows.Forms.TextBox m_tbUserID;
        private System.Windows.Forms.TextBox m_tbPassword;
        private System.Windows.Forms.Button m_bnOK;
        private System.Windows.Forms.Button m_bnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.IContainer components;
        public FTPProperties m_FTPProperties;
        public string FTPServer
        {
            get { return m_tbFTPServer.Text; }
        }
        public string UserID
        {
            get { return m_tbUserID.Text; }
        }
        public string Password
        {
            get { return m_tbPassword.Text; }
        }
        public FTPConnectDlg(FTPProperties l_ftpProperties)
        {
//
// Required for Windows Form Designer support
//
InitializeComponent() ;
//
// TODO: Add any constructor code after InitializeComponent call
//
m_FTPProperties = l_ftpProperties;
            m_tbFTPServer.Text = m_FTPProperties.FTPServer;
            m_tbUserID.Text = m_FTPProperties.UserID;
            m_tbPassword.Text = m_FTPProperties.Password;
        }
        private void OnClickOK(object sender, System.EventArgs e)
        {
            string l_strServer = m_tbFTPServer.Text;
            l_strServer.Trim();
            if (l_strServer.Length == 0)
            {
                m_tbFTPServer.Focus();
                return;
            }
            string l_strUserID = m_tbUserID.Text;
            l_strUserID.Trim();
            if (l_strUserID.Length == 0)
            {
                m_tbUserID.Focus();
                return;
            }
            string l_strPassword = m_tbPassword.Text;
            if (l_strPassword.Length == 0)
            {
                m_tbPassword.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void OnClickCancel(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void OnClickConfigure(object sender, System.EventArgs e)
        {
            FTPConfigureDlg configureDlg = new FTPConfigureDlg(true, m_FTPProperties);
            configureDlg.ShowDialog();
        }
    }
}