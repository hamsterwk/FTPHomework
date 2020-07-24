namespace FTPExplorer
{
    partial class FTPMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(){
            this.label1 = new System.Windows.Forms.Label();
            this.connectLightLbl = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.localFileBox = new System.Windows.Forms.ListBox();
            this.serverFileBox = new System.Windows.Forms.ListBox();
            this.uploadBtn = new System.Windows.Forms.Button();
            this.downloadBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pathIndexLbl = new System.Windows.Forms.Label();
            this.fileIndexBtn = new System.Windows.Forms.Button();
            this.UTF8RadioBtn = new System.Windows.Forms.RadioButton();
            this.GBKRadioBtn = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.ipIndexLbl = new System.Windows.Forms.Label();
            this.ASCIIRadioBtn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(50, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接状态：";
            // 
            // connectLightLbl
            // 
            this.connectLightLbl.AutoSize = true;
            this.connectLightLbl.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectLightLbl.Location = new System.Drawing.Point(285, 34);
            this.connectLightLbl.Name = "connectLightLbl";
            this.connectLightLbl.Size = new System.Drawing.Size(25, 27);
            this.connectLightLbl.TabIndex = 1;
            this.connectLightLbl.Text = "●";
            // 
            // connectBtn
            // 
            this.connectBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connectBtn.Location = new System.Drawing.Point(391, 29);
            this.connectBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(172, 36);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "连接状态设置";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // localFileBox
            // 
            this.localFileBox.FormattingEnabled = true;
            this.localFileBox.ItemHeight = 15;
            this.localFileBox.Location = new System.Drawing.Point(54, 109);
            this.localFileBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.localFileBox.Name = "localFileBox";
            this.localFileBox.Size = new System.Drawing.Size(509, 229);
            this.localFileBox.TabIndex = 3;
            // 
            // serverFileBox
            // 
            this.serverFileBox.FormattingEnabled = true;
            this.serverFileBox.ItemHeight = 15;
            this.serverFileBox.Location = new System.Drawing.Point(625, 109);
            this.serverFileBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.serverFileBox.Name = "serverFileBox";
            this.serverFileBox.Size = new System.Drawing.Size(512, 229);
            this.serverFileBox.TabIndex = 4;
            // 
            // uploadBtn
            // 
            this.uploadBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadBtn.Location = new System.Drawing.Point(55, 359);
            this.uploadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uploadBtn.Name = "uploadBtn";
            this.uploadBtn.Size = new System.Drawing.Size(87, 36);
            this.uploadBtn.TabIndex = 5;
            this.uploadBtn.Text = "上传";
            this.uploadBtn.UseVisualStyleBackColor = true;
            // 
            // downloadBtn
            // 
            this.downloadBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downloadBtn.Location = new System.Drawing.Point(625, 359);
            this.downloadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.downloadBtn.Name = "downloadBtn";
            this.downloadBtn.Size = new System.Drawing.Size(87, 36);
            this.downloadBtn.TabIndex = 6;
            this.downloadBtn.Text = "下载";
            this.downloadBtn.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.ItemHeight = 15;
            this.logBox.Location = new System.Drawing.Point(55, 421);
            this.logBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(1082, 94);
            this.logBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(52, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "本地路径";
            // 
            // pathIndexLbl
            // 
            this.pathIndexLbl.AutoSize = true;
            this.pathIndexLbl.Location = new System.Drawing.Point(127, 90);
            this.pathIndexLbl.Name = "pathIndexLbl";
            this.pathIndexLbl.Size = new System.Drawing.Size(55, 15);
            this.pathIndexLbl.TabIndex = 9;
            this.pathIndexLbl.Text = "label4";
            // 
            // fileIndexBtn
            // 
            this.fileIndexBtn.Location = new System.Drawing.Point(497, 81);
            this.fileIndexBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.fileIndexBtn.Name = "fileIndexBtn";
            this.fileIndexBtn.Size = new System.Drawing.Size(67, 23);
            this.fileIndexBtn.TabIndex = 10;
            this.fileIndexBtn.Text = "button4";
            this.fileIndexBtn.UseVisualStyleBackColor = true;
            // 
            // UTF8RadioBtn
            // 
            this.UTF8RadioBtn.AutoSize = true;
            this.UTF8RadioBtn.Location = new System.Drawing.Point(734, 42);
            this.UTF8RadioBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UTF8RadioBtn.Name = "UTF8RadioBtn";
            this.UTF8RadioBtn.Size = new System.Drawing.Size(60, 19);
            this.UTF8RadioBtn.TabIndex = 11;
            this.UTF8RadioBtn.TabStop = true;
            this.UTF8RadioBtn.Text = "UTF8";
            this.UTF8RadioBtn.UseVisualStyleBackColor = true;
            this.UTF8RadioBtn.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // GBKRadioBtn
            // 
            this.GBKRadioBtn.AutoSize = true;
            this.GBKRadioBtn.Location = new System.Drawing.Point(857, 41);
            this.GBKRadioBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GBKRadioBtn.Name = "GBKRadioBtn";
            this.GBKRadioBtn.Size = new System.Drawing.Size(52, 19);
            this.GBKRadioBtn.TabIndex = 12;
            this.GBKRadioBtn.TabStop = true;
            this.GBKRadioBtn.Text = "GBK";
            this.GBKRadioBtn.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(622, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 27);
            this.label5.TabIndex = 13;
            this.label5.Text = "编码";
            // 
            // ipIndexLbl
            // 
            this.ipIndexLbl.AutoSize = true;
            this.ipIndexLbl.Location = new System.Drawing.Point(622, 87);
            this.ipIndexLbl.Name = "ipIndexLbl";
            this.ipIndexLbl.Size = new System.Drawing.Size(55, 15);
            this.ipIndexLbl.TabIndex = 14;
            this.ipIndexLbl.Text = "label6";
            // 
            // ASCIIRadioBtn
            // 
            this.ASCIIRadioBtn.AutoSize = true;
            this.ASCIIRadioBtn.Location = new System.Drawing.Point(970, 41);
            this.ASCIIRadioBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ASCIIRadioBtn.Name = "ASCIIRadioBtn";
            this.ASCIIRadioBtn.Size = new System.Drawing.Size(68, 19);
            this.ASCIIRadioBtn.TabIndex = 15;
            this.ASCIIRadioBtn.TabStop = true;
            this.ASCIIRadioBtn.Text = "ASCII";
            this.ASCIIRadioBtn.UseVisualStyleBackColor = true;
            // 
            // FTPMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1161, 537);
            this.Controls.Add(this.ASCIIRadioBtn);
            this.Controls.Add(this.ipIndexLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.GBKRadioBtn);
            this.Controls.Add(this.UTF8RadioBtn);
            this.Controls.Add(this.fileIndexBtn);
            this.Controls.Add(this.pathIndexLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.downloadBtn);
            this.Controls.Add(this.uploadBtn);
            this.Controls.Add(this.serverFileBox);
            this.Controls.Add(this.localFileBox);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.connectLightLbl);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FTPMainForm";
            this.Text = ".";
            this.Load += new System.EventHandler(this.FTPMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label connectLightLbl;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.ListBox localFileBox;
        private System.Windows.Forms.ListBox serverFileBox;
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.Button downloadBtn;
        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pathIndexLbl;
        private System.Windows.Forms.Button fileIndexBtn;
        private System.Windows.Forms.RadioButton UTF8RadioBtn;
        private System.Windows.Forms.RadioButton GBKRadioBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ipIndexLbl;
        private System.Windows.Forms.RadioButton ASCIIRadioBtn;
    }
}

