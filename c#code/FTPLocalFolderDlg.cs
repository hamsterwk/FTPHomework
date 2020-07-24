using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
namespace FTPExplorer
{
/// < summary >
/// Summary description for FTPLocalFolderDlg.
/// < /summary >
public class FTPLocalFolderDlg : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbLocalFolder;
        private System.Windows.Forms.Button m_bnBrowse;
        private System.Windows.Forms.Button m_bnOK;
        private System.Windows.Forms.Button m_bnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
        public FTPLocalFolderDlg(string l_strLocalFolder)
        {
//
// Required for Windows Form Designer support
//
InitializeComponent() ;
//
// TODO: Add any constructor code after InitializeComponent call
//
m_tbLocalFolder.Text = l_strLocalFolder;
        }
        public string LocalFolder
        {
            get { return m_tbLocalFolder.Text; }
        }
        private void OnClickBrowse(object sender, System.EventArgs e)
        {
        }
        private void OnClickOK(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void OnClickCancel(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
