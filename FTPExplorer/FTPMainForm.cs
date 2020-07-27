using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FTPExplorer
{
    public partial class FTPMainForm : Form
    {
        public MyFTP myFTP;
        private TreeNode LocalNode, RemoteNode;
        private LocalTreeAfterEdit flag;
        public FTPMainForm()
        {
            myFTP = null;
            LocalNode = RemoteNode = null;
            flag = LocalTreeAfterEdit.DefaultOptions;
            InitializeComponent();
            LocalPathLabel.Text = "";
            RemotePathLabel.Text = "";
            PreLoadTree();
        }

        private void PreLoadTree()
        {
            foreach (string drive in Environment.GetLogicalDrives())
            {
                var dir = new DriveInfo(drive);
                switch (dir.DriveType)           //判断驱动器类型
                {
                    case DriveType.Fixed:        //仅取固定磁盘盘符 Removable-U盘
                        {
                            TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);
                            tNode.Name = dir.Name;
                            tNode.Tag = tNode.Name;
                            tNode.ImageIndex = ImageIndex.Drive;         //获取结点显示图片
                            tNode.SelectedImageIndex = ImageIndex.Drive; //选择显示图片
                            LocalTree.Nodes.Add(tNode);                   //加载驱动节点
                            tNode.Nodes.Add("");
                        }
                        break;
                }
            }
        }

        private void PreLoadRemoteTree()
        {
            TreeNode root = new TreeNode("/",ImageIndex.FolderClose, ImageIndex.FolderOpen);
            root.Tag = "/";
            root.Name = "/";
            root.Nodes.Add("");
            RemoteTree.Nodes.Add(root);
            SetRemoteNode(root);
        }

        private void AddLog(string msg,int type=0)
        {
            //输入日志
            logBox.Items.Add(msg);
            if (logBox.Items.Count > 100)
            {
                logBox.Items.RemoveAt(0);
            }
            logBox.TopIndex = this.logBox.Items.Count - (int)(this.logBox.Height / this.logBox.ItemHeight);
        }


        private void FTPMainForm_Load(object sender, EventArgs e)
        {

        }

        private void SetLocalNode(TreeNode node)
        {
            LocalNode = node;
            LocalPathLabel.Text = node.Name;
        }

        private void SetRemoteNode(TreeNode node)
        {
            RemoteNode = node;
            RemotePathLabel.Text = node.Name;
        }

        private void LocalTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.ImageIndex == ImageIndex.FolderClose)
            {
                node.ImageIndex = ImageIndex.FolderOpen;
            }
            node.Nodes.Clear();
            string path = node.Name;
            int sz = 0;
            foreach(string DirName in Directory.GetDirectories(path))
            {
                TreeNode sonNode = new TreeNode(new DirectoryInfo(DirName).Name);
                sonNode.Name = new DirectoryInfo(DirName).FullName;               //完整目录
                sonNode.Tag = sonNode.Name;
                sonNode.ImageIndex = ImageIndex.FolderClose;       //获取节点显示图片
                sonNode.SelectedImageIndex = ImageIndex.FolderOpen; //选择节点显示图片
                node.Nodes.Add(sonNode);
                sonNode.Nodes.Add("");
                sz += 1;
            }
            foreach (string FileName in Directory.GetFiles(path))
            {
                TreeNode sonNode = new TreeNode(new DirectoryInfo(FileName).Name);
                sonNode.Name = new DirectoryInfo(FileName).FullName;               //完整目录
                sonNode.Tag = sonNode.Name;
                sonNode.ImageIndex = ImageIndex.File;       //获取节点显示图片
                sonNode.SelectedImageIndex = ImageIndex.File;
                node.Nodes.Add(sonNode);
                sz += 1;
            }
            if (sz == 0&&node.ImageIndex==ImageIndex.FolderOpen)
            {
                node.ImageIndex = ImageIndex.FolderClose;
            }
            SetLocalNode(node);
        }

        private void LocalTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.ImageIndex == ImageIndex.FolderOpen)
            {
                node.ImageIndex = ImageIndex.FolderClose;
            }
            SetLocalNode(node);
        }

        private void RemoteTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            AddLog($"正在获取{e.Node.Name}的目录列表......");
            if(myFTP.Connected()==false)myFTP.Connect();
            TreeNode node = e.Node;
            if (node.ImageIndex == ImageIndex.FolderClose)
            {
                node.ImageIndex = ImageIndex.FolderOpen;
            }
            node.Nodes.Clear();
            int sz = 0;
            string path = node.Name;
            List<MyFTPItem> list = new List<MyFTPItem>();
            try
            {
                myFTP.ChangeDir(node.Name);
                list = myFTP.GetFileList();
            }
            catch(Exception ex)
            {
                AddLog(ex.Message,1);
                return;
            }
            foreach (MyFTPItem item in list)
            {
                TreeNode sonNode = new TreeNode(item.Name);
                sonNode.Name = node.Name+item.Name;               //完整目录
                sonNode.Tag = sonNode.Name;
                if (item.Type == "dir")
                {
                    sonNode.Name = sonNode.Name+"/";
                    sonNode.ImageIndex = ImageIndex.FolderClose;       //获取节点显示图片
                    sonNode.SelectedImageIndex = ImageIndex.FolderOpen; //选择节点显示图片
                    sonNode.Nodes.Add("");
                }
                else
                {
                    sonNode.ImageIndex = ImageIndex.File;       //获取节点显示图片
                    sonNode.SelectedImageIndex = ImageIndex.File; //选择节点显示图片
                }

                node.Nodes.Add(sonNode);
                sz += 1;
            }
            if (sz == 0 && node.ImageIndex == ImageIndex.FolderOpen)
            {
                node.ImageIndex = ImageIndex.FolderClose;
            }
            AddLog($"获取成功！共获取{sz}项。");
            SetRemoteNode(node);
        }

        private void RemoteTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            myFTP.ChangeDir(e.Node.Name);
            if (e.Node.ImageIndex == ImageIndex.FolderOpen)
            {
                e.Node.ImageIndex = ImageIndex.FolderClose;
            }
            //AddLog($"目录已切换至：{e.Node.Name}");
            SetRemoteNode(e.Node);
        }

        private void LocalTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetLocalNode(e.Node);
        }

        private void RemoteTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetRemoteNode(e.Node);
        }
        public static void DeleteFolder(string dir)
        {
            if (System.IO.Directory.Exists(dir))
            {
                string[] fileSystemEntries = System.IO.Directory.GetFileSystemEntries(dir);
                for (int i = 0; i < fileSystemEntries.Length; i++)
                {
                    string text = fileSystemEntries[i];
                    if (System.IO.File.Exists(text))
                    {
                        System.IO.File.Delete(text);
                    }
                    else
                    {
                        DeleteFolder(text);
                    }
                }
                System.IO.Directory.Delete(dir);
            }
        }
        private void LocalDeleteBtn_Click(object sender, EventArgs e)
        {
            if (LocalNode == null) return;
            string deleteDir = LocalTree.SelectedNode.Name;
            LocalTree.SelectedNode.Remove();
            if (Directory.Exists(deleteDir)){
                DeleteFolder(deleteDir);
            }
            else{
                File.Delete(deleteDir);
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            myFTP = new MyFTP();
            myFTP.Server = ServerBox.Text.Trim();
            if (myFTP.Server.Length == 0)
            {
                ServerWarning.Visible = true;
                ServerBox.Focus();
                return;
            }
            ServerWarning.Visible = false;
            myFTP.UserName = UserNameBox.Text.Trim();
            if (myFTP.UserName.Length == 0)
            {
                UserWarning.Visible = true;
                UserNameBox.Focus();
                return;
            }
            UserWarning.Visible = false;
            myFTP.Password = PwdBox.Text.Trim();
            try { myFTP.Port = int.Parse(PortBox.Text); }catch{ PortWarning.Visible = true;PortBoxLabel.Focus();return; }
            PortWarning.Visible = false;

            RemoteTree.Nodes.Clear();
            AddLog($"正在尝试连接 {myFTP.Server}:{myFTP.Port}......");
            try { myFTP.Connect(); } catch (Exception ex) { AddLog("错误： " + ex.Message, 1); return; }
            AddLog($"FTP已连接到 {myFTP.Server}:{myFTP.Port}, 成功登录！");
            Text = $"极简FTP [{myFTP.Server}:{myFTP.Port}]";
            PreLoadRemoteTree();
        }

        private void uploadBtn_Click(object sender, EventArgs e){
            throw new System.NotImplementedException();
        }
        //TODO:把重命名写了（7/27日完成）
        private void LocalRenameBtn_Click(object sender, EventArgs e){
            LocalTree.LabelEdit = true;
            LocalTree.SelectedNode.BeginEdit();
            flag = LocalTreeAfterEdit.Renamed;
        }
        private void LocalNewFolderBtn_Click(object sender, EventArgs e){
            string newNodeDir = Path.Combine(Path.GetFullPath(LocalTree.SelectedNode.Name), "新建文件夹");
            if (Directory.Exists(newNodeDir)){
                AddLog($"文件夹已经存在！");
                return;
            }
            TreeNode newNode = new TreeNode("新建文件夹");
            flag = LocalTreeAfterEdit.Added;
            LocalTree.SelectedNode.Nodes.Add(newNode);
            newNode.Nodes.Add("");
            newNode.Name = newNodeDir;
            newNode.Tag = newNode.Name;
            newNode.ImageIndex = ImageIndex.FolderClose;       //获取节点显示图片
            newNode.SelectedImageIndex = ImageIndex.FolderOpen;
            Directory.CreateDirectory(newNodeDir);
            LocalPathLabel.Text = newNodeDir;
            var oldSelected = LocalTree.SelectedNode;
            //LocalTree.SelectedNode = oldSelected.Nodes.Find(newNode.Name,false)[0];
            if (!LocalTree.SelectedNode.IsExpanded){
                LocalTree.SelectedNode.Expand();
            }
        }
        private void LocalTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e){
            string originalPath, parentDie, currentPath;
            switch (flag){
                case LocalTreeAfterEdit.Renamed:{
                    originalPath = Path.GetFullPath(e.Node.Name);
                    parentDie = e.Node.Parent.Name;
                    if (Environment.GetLogicalDrives().Contains(parentDie)){
                        currentPath = e.Node.Parent.Name + e.Label;
                    }
                    else{
                        currentPath = $@"{e.Node.Parent.Name}\{e.Label}";
                    }

                    e.Node.Name = currentPath;
                    if (!Directory.Exists(currentPath)){
                        FileInfo originalInfo = new FileInfo(originalPath);
                        originalInfo.MoveTo(currentPath);
                        LocalPathLabel.Text = currentPath;
                        LocalTree.LabelEdit = false;
                    }
                }
                    break;
                default:
                    break;
            }
        }
    }
}
