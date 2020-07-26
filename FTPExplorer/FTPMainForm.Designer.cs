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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPMainForm));
            this.uploadBtn = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LocalPathLabel = new System.Windows.Forms.Label();
            this.RemotePathLabel = new System.Windows.Forms.Label();
            this.LocalTree = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.RemoteTree = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.UserNameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PwdBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.PortBoxLabel = new System.Windows.Forms.Label();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.ServerWarning = new System.Windows.Forms.Label();
            this.UserWarning = new System.Windows.Forms.Label();
            this.PortWarning = new System.Windows.Forms.Label();
            this.LocalDeleteBtn = new System.Windows.Forms.Button();
            this.LocalRenameBtn = new System.Windows.Forms.Button();
            this.LocalNewFolderBtn = new System.Windows.Forms.Button();
            this.RemoteNewFolderBtn = new System.Windows.Forms.Button();
            this.RemoteRenameBtn = new System.Windows.Forms.Button();
            this.RemoteDeleteBtn = new System.Windows.Forms.Button();
            this.DownloadBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.HorizontalScrollbar = true;
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
            // LocalPathLabel
            // 
            this.LocalPathLabel.AutoSize = true;
            this.LocalPathLabel.Location = new System.Drawing.Point(127, 90);
            this.LocalPathLabel.Name = "LocalPathLabel";
            this.LocalPathLabel.Size = new System.Drawing.Size(31, 15);
            this.LocalPathLabel.TabIndex = 9;
            this.LocalPathLabel.Text = "123";
            // 
            // RemotePathLabel
            // 
            this.RemotePathLabel.AutoSize = true;
            this.RemotePathLabel.Location = new System.Drawing.Point(698, 90);
            this.RemotePathLabel.Name = "RemotePathLabel";
            this.RemotePathLabel.Size = new System.Drawing.Size(31, 15);
            this.RemotePathLabel.TabIndex = 14;
            this.RemotePathLabel.Text = "123";
            // 
            // LocalTree
            // 
            this.LocalTree.ImageIndex = 0;
            this.LocalTree.ImageList = this.imageList;
            this.LocalTree.Location = new System.Drawing.Point(55, 110);
            this.LocalTree.Name = "LocalTree";
            this.LocalTree.SelectedImageIndex = 0;
            this.LocalTree.Size = new System.Drawing.Size(508, 228);
            this.LocalTree.TabIndex = 16;
            this.LocalTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.LocalTree_AfterCollapse);
            this.LocalTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.LocalTree_BeforeExpand);
            this.LocalTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LocalTree_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Drive.ico");
            this.imageList.Images.SetKeyName(1, "FolderClose.ico");
            this.imageList.Images.SetKeyName(2, "FolderOpen.ico");
            this.imageList.Images.SetKeyName(3, "File.png");
            // 
            // RemoteTree
            // 
            this.RemoteTree.ImageIndex = 0;
            this.RemoteTree.ImageList = this.imageList;
            this.RemoteTree.Location = new System.Drawing.Point(627, 110);
            this.RemoteTree.Name = "RemoteTree";
            this.RemoteTree.SelectedImageIndex = 0;
            this.RemoteTree.Size = new System.Drawing.Size(510, 228);
            this.RemoteTree.TabIndex = 17;
            this.RemoteTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.RemoteTree_AfterCollapse);
            this.RemoteTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.RemoteTree_BeforeExpand);
            this.RemoteTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RemoteTree_AfterSelect);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(623, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "远程路径";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label1.Location = new System.Drawing.Point(51, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "FTP地址:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServerBox
            // 
            this.ServerBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ServerBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ServerBox.Location = new System.Drawing.Point(139, 29);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(194, 34);
            this.ServerBox.TabIndex = 1;
            this.ServerBox.Text = "127.0.0.1";
            // 
            // UserNameBox
            // 
            this.UserNameBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UserNameBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.UserNameBox.Location = new System.Drawing.Point(422, 29);
            this.UserNameBox.Name = "UserNameBox";
            this.UserNameBox.Size = new System.Drawing.Size(194, 34);
            this.UserNameBox.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label4.Location = new System.Drawing.Point(339, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 27);
            this.label4.TabIndex = 20;
            this.label4.Text = "用户名:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PwdBox
            // 
            this.PwdBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PwdBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PwdBox.Location = new System.Drawing.Point(685, 29);
            this.PwdBox.Name = "PwdBox";
            this.PwdBox.PasswordChar = '*';
            this.PwdBox.Size = new System.Drawing.Size(148, 34);
            this.PwdBox.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label5.Location = new System.Drawing.Point(622, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 27);
            this.label5.TabIndex = 22;
            this.label5.Text = "密码:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PortBox
            // 
            this.PortBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PortBox.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PortBox.Location = new System.Drawing.Point(902, 29);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(58, 34);
            this.PortBox.TabIndex = 25;
            this.PortBox.Text = "21";
            // 
            // PortBoxLabel
            // 
            this.PortBoxLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PortBoxLabel.AutoSize = true;
            this.PortBoxLabel.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.PortBoxLabel.Location = new System.Drawing.Point(839, 32);
            this.PortBoxLabel.Name = "PortBoxLabel";
            this.PortBoxLabel.Size = new System.Drawing.Size(57, 27);
            this.PortBoxLabel.TabIndex = 24;
            this.PortBoxLabel.Text = "端口:";
            this.PortBoxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ConnectBtn.Location = new System.Drawing.Point(1009, 30);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(128, 33);
            this.ConnectBtn.TabIndex = 26;
            this.ConnectBtn.Text = "建立连接";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // ServerWarning
            // 
            this.ServerWarning.AutoSize = true;
            this.ServerWarning.ForeColor = System.Drawing.Color.Red;
            this.ServerWarning.Location = new System.Drawing.Point(136, 66);
            this.ServerWarning.Name = "ServerWarning";
            this.ServerWarning.Size = new System.Drawing.Size(127, 15);
            this.ServerWarning.TabIndex = 27;
            this.ServerWarning.Text = "服务器地址非法！";
            this.ServerWarning.Visible = false;
            // 
            // UserWarning
            // 
            this.UserWarning.AutoSize = true;
            this.UserWarning.ForeColor = System.Drawing.Color.Red;
            this.UserWarning.Location = new System.Drawing.Point(419, 66);
            this.UserWarning.Name = "UserWarning";
            this.UserWarning.Size = new System.Drawing.Size(127, 15);
            this.UserWarning.TabIndex = 28;
            this.UserWarning.Text = "用户名不能为空！";
            this.UserWarning.Visible = false;
            // 
            // PortWarning
            // 
            this.PortWarning.AutoSize = true;
            this.PortWarning.ForeColor = System.Drawing.Color.Red;
            this.PortWarning.Location = new System.Drawing.Point(841, 66);
            this.PortWarning.Name = "PortWarning";
            this.PortWarning.Size = new System.Drawing.Size(97, 15);
            this.PortWarning.TabIndex = 29;
            this.PortWarning.Text = "端口号非法！";
            this.PortWarning.Visible = false;
            // 
            // LocalDeleteBtn
            // 
            this.LocalDeleteBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocalDeleteBtn.Location = new System.Drawing.Point(182, 359);
            this.LocalDeleteBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalDeleteBtn.Name = "LocalDeleteBtn";
            this.LocalDeleteBtn.Size = new System.Drawing.Size(87, 36);
            this.LocalDeleteBtn.TabIndex = 30;
            this.LocalDeleteBtn.Text = "删除";
            this.LocalDeleteBtn.UseVisualStyleBackColor = true;
            this.LocalDeleteBtn.Click += new System.EventHandler(this.LocalDeleteBtn_Click);
            // 
            // LocalRenameBtn
            // 
            this.LocalRenameBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocalRenameBtn.Location = new System.Drawing.Point(309, 359);
            this.LocalRenameBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalRenameBtn.Name = "LocalRenameBtn";
            this.LocalRenameBtn.Size = new System.Drawing.Size(87, 36);
            this.LocalRenameBtn.TabIndex = 31;
            this.LocalRenameBtn.Text = "重命名";
            this.LocalRenameBtn.UseVisualStyleBackColor = true;
            // 
            // LocalNewFolderBtn
            // 
            this.LocalNewFolderBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocalNewFolderBtn.Location = new System.Drawing.Point(436, 359);
            this.LocalNewFolderBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocalNewFolderBtn.Name = "LocalNewFolderBtn";
            this.LocalNewFolderBtn.Size = new System.Drawing.Size(126, 36);
            this.LocalNewFolderBtn.TabIndex = 32;
            this.LocalNewFolderBtn.Text = "新建文件夹";
            this.LocalNewFolderBtn.UseVisualStyleBackColor = true;
            // 
            // RemoteNewFolderBtn
            // 
            this.RemoteNewFolderBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RemoteNewFolderBtn.Location = new System.Drawing.Point(1008, 359);
            this.RemoteNewFolderBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RemoteNewFolderBtn.Name = "RemoteNewFolderBtn";
            this.RemoteNewFolderBtn.Size = new System.Drawing.Size(126, 36);
            this.RemoteNewFolderBtn.TabIndex = 36;
            this.RemoteNewFolderBtn.Text = "新建文件夹";
            this.RemoteNewFolderBtn.UseVisualStyleBackColor = true;
            // 
            // RemoteRenameBtn
            // 
            this.RemoteRenameBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RemoteRenameBtn.Location = new System.Drawing.Point(881, 359);
            this.RemoteRenameBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RemoteRenameBtn.Name = "RemoteRenameBtn";
            this.RemoteRenameBtn.Size = new System.Drawing.Size(87, 36);
            this.RemoteRenameBtn.TabIndex = 35;
            this.RemoteRenameBtn.Text = "重命名";
            this.RemoteRenameBtn.UseVisualStyleBackColor = true;
            // 
            // RemoteDeleteBtn
            // 
            this.RemoteDeleteBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RemoteDeleteBtn.Location = new System.Drawing.Point(754, 359);
            this.RemoteDeleteBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RemoteDeleteBtn.Name = "RemoteDeleteBtn";
            this.RemoteDeleteBtn.Size = new System.Drawing.Size(87, 36);
            this.RemoteDeleteBtn.TabIndex = 34;
            this.RemoteDeleteBtn.Text = "删除";
            this.RemoteDeleteBtn.UseVisualStyleBackColor = true;
            // 
            // DownloadBtn
            // 
            this.DownloadBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DownloadBtn.Location = new System.Drawing.Point(627, 359);
            this.DownloadBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DownloadBtn.Name = "DownloadBtn";
            this.DownloadBtn.Size = new System.Drawing.Size(87, 36);
            this.DownloadBtn.TabIndex = 33;
            this.DownloadBtn.Text = "下载";
            this.DownloadBtn.UseVisualStyleBackColor = true;
            // 
            // FTPMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1161, 537);
            this.Controls.Add(this.RemoteNewFolderBtn);
            this.Controls.Add(this.RemoteRenameBtn);
            this.Controls.Add(this.RemoteDeleteBtn);
            this.Controls.Add(this.DownloadBtn);
            this.Controls.Add(this.LocalNewFolderBtn);
            this.Controls.Add(this.LocalRenameBtn);
            this.Controls.Add(this.LocalDeleteBtn);
            this.Controls.Add(this.PortWarning);
            this.Controls.Add(this.UserWarning);
            this.Controls.Add(this.ServerWarning);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.PortBoxLabel);
            this.Controls.Add(this.PwdBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.UserNameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RemoteTree);
            this.Controls.Add(this.LocalTree);
            this.Controls.Add(this.RemotePathLabel);
            this.Controls.Add(this.LocalPathLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.uploadBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FTPMainForm";
            this.Text = "极简FTP [未建立连接]";
            this.Load += new System.EventHandler(this.FTPMainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button uploadBtn;
        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LocalPathLabel;
        private System.Windows.Forms.Label RemotePathLabel;
        private System.Windows.Forms.TreeView LocalTree;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TreeView RemoteTree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerBox;
        private System.Windows.Forms.TextBox UserNameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PwdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label PortBoxLabel;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Label ServerWarning;
        private System.Windows.Forms.Label UserWarning;
        private System.Windows.Forms.Label PortWarning;
        private System.Windows.Forms.Button LocalDeleteBtn;
        private System.Windows.Forms.Button LocalRenameBtn;
        private System.Windows.Forms.Button LocalNewFolderBtn;
        private System.Windows.Forms.Button RemoteNewFolderBtn;
        private System.Windows.Forms.Button RemoteRenameBtn;
        private System.Windows.Forms.Button RemoteDeleteBtn;
        private System.Windows.Forms.Button DownloadBtn;
    }
}

