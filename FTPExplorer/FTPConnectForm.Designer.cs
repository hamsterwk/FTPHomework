namespace FTPExplorer
{
    partial class FTPConnectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serverAddressBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.portBox = new System.Windows.Forms.TextBox();
            this.loginNameBox = new System.Windows.Forms.TextBox();
            this.loginPwdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.testConnectBtn = new System.Windows.Forms.Button();
            this.ftpConnectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(42, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(43, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "登录用户名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label3.Location = new System.Drawing.Point(44, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "登录密码";
            // 
            // serverAddressBox
            // 
            this.serverAddressBox.Location = new System.Drawing.Point(181, 81);
            this.serverAddressBox.Name = "serverAddressBox";
            this.serverAddressBox.Size = new System.Drawing.Size(370, 28);
            this.serverAddressBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(44, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 31);
            this.label4.TabIndex = 4;
            this.label4.Text = "端口：";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(181, 140);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(370, 28);
            this.portBox.TabIndex = 5;
            // 
            // loginNameBox
            // 
            this.loginNameBox.Location = new System.Drawing.Point(181, 194);
            this.loginNameBox.Name = "loginNameBox";
            this.loginNameBox.Size = new System.Drawing.Size(370, 28);
            this.loginNameBox.TabIndex = 6;
            // 
            // loginPwdBox
            // 
            this.loginPwdBox.Location = new System.Drawing.Point(181, 253);
            this.loginPwdBox.Name = "loginPwdBox";
            this.loginPwdBox.PasswordChar = '*';
            this.loginPwdBox.Size = new System.Drawing.Size(370, 28);
            this.loginPwdBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(202, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 31);
            this.label5.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label6.Location = new System.Drawing.Point(217, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 31);
            this.label6.TabIndex = 8;
            this.label6.Text = "FTP连接信息";
            // 
            // testConnectBtn
            // 
            this.testConnectBtn.Location = new System.Drawing.Point(86, 310);
            this.testConnectBtn.Name = "testConnectBtn";
            this.testConnectBtn.Size = new System.Drawing.Size(115, 40);
            this.testConnectBtn.TabIndex = 9;
            this.testConnectBtn.Text = "测试连接";
            this.testConnectBtn.UseVisualStyleBackColor = true;
            // 
            // ftpConnectBtn
            // 
            this.ftpConnectBtn.Location = new System.Drawing.Point(358, 310);
            this.ftpConnectBtn.Name = "ftpConnectBtn";
            this.ftpConnectBtn.Size = new System.Drawing.Size(115, 40);
            this.ftpConnectBtn.TabIndex = 10;
            this.ftpConnectBtn.Text = "连接";
            this.ftpConnectBtn.UseVisualStyleBackColor = true;
            // 
            // FTPConnectForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(582, 370);
            this.Controls.Add(this.ftpConnectBtn);
            this.Controls.Add(this.testConnectBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.loginPwdBox);
            this.Controls.Add(this.loginNameBox);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.serverAddressBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FTPConnectForm";
            this.Padding = new System.Windows.Forms.Padding(40);
            this.Text = "FTPConnectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverAddressBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox loginNameBox;
        private System.Windows.Forms.TextBox loginPwdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button testConnectBtn;
        private System.Windows.Forms.Button ftpConnectBtn;
    }
}