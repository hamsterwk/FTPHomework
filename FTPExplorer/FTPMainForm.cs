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
        private int allowRep;//是否允许替换？
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

        private bool IsDirectory(TreeNode node)
        {
            return node.Name[node.Name.Length-1]=='/';
        }

        private void FTPMainForm_Load(object sender, EventArgs e)
        {

        }

        private void SetLocalNode(TreeNode node)
        {
            LocalNode = node;
            LocalPathLabel.Text = node.Name;
            //LocalTree.SelectedNode = node;
            //node.Expand();
        }

        private void SetRemoteNode(TreeNode node)
        {
            //if (node == null) return;
            RemoteNode = node;
            if (node == null) return;
            RemotePathLabel.Text = node.Name;
            //node.Expand();
            //RemoteTree.SelectedNode = node;
            //AddLog(RemoteNode.Name);
        }

        private void ReloadLocalNode(TreeNode node)
        {
            node.Nodes.Clear();
            string path = node.Name;
            int sz = 0;
            foreach (string DirName in Directory.GetDirectories(path))
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
            LocalTree.SelectedNode = node;
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

        private void ReloadRemoteNode(TreeNode node)
        {
            AddLog($"正在获取{node.Name}的目录列表......");
            if (myFTP.Connected() == false) myFTP.Connect();
            node.Nodes.Clear();
            int sz = 0;
            string path = node.Name;
            List<MyFTPItem> list = new List<MyFTPItem>();
            try
            {
                myFTP.ChangeDir(node.Name);
                list = myFTP.GetFileList();
            }
            catch (Exception ex)
            {
                AddLog(ex.Message, 1);
                return;
            }
            foreach (MyFTPItem item in list)
            {
                TreeNode sonNode = new TreeNode(item.Name);
                sonNode.Name = node.Name + item.Name;               //完整目录
                sonNode.Tag = sonNode.Name;
                if (item.Type == "dir")
                {
                    sonNode.Name = sonNode.Name + "/";
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
            AddLog($"获取成功！共获取{sz}项。");
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
            RemoteTree.SelectedNode = node;
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


        private void LocalRenameBtn_Click(object sender, EventArgs e){
            if (LocalTree.SelectedNode == null) return;
            LocalTree.SelectedNode.BeginEdit();
            flag = LocalTreeAfterEdit.Renamed;
        }
        private void LocalNewFolderBtn_Click(object sender, EventArgs e){
            if (LocalNode == null) return;
            InputForm inputForm = new InputForm("请输入文件夹名", "文件夹名非法！");
            inputForm.ShowDialog();
            if (inputForm.DialogResult != DialogResult.OK) return;
            string newDirName = inputForm.Response;
            string newNodeDir = Path.Combine(Path.GetFullPath(LocalTree.SelectedNode.Name), newDirName);
            if (Directory.Exists(newNodeDir)){
                AddLog($"文件夹已经存在！");
                return;
            }
            TreeNode newNode = new TreeNode(newDirName);
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

        private void RemoteRenameBtn_Click(object sender, EventArgs e)
        {
            if (RemoteNode == null)
            {
                AddLog("未选中任何文件/文件夹");
                return;
            }
            if (RemoteNode.Parent == null) return;
            RemoteNode.BeginEdit();
        }

        private void RemoteTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string oldName = e.Node.Text;
            string newName = e.Label;
            string newPath = e.Node.Parent.Name + newName;
            string oldPath = e.Node.Parent.Name + oldName;
            AddLog($"正准备将{oldPath}重命名为{newPath}...");
            try
            {
                AddLog($"切换目录至{RemoteNode.Parent.Name}...");
                myFTP.ChangeDir(RemoteNode.Parent.Name);
                AddLog($"正在请求重命名操作...");
                myFTP.RenameDir(oldName, newName);
                RemoteNode.Text = newName;
                if (IsDirectory(RemoteNode))
                {
                    myFTP.ChangeDir(newPath);
                    AddLog($"切换目录至{newPath}...");
                }
            }
            catch (Exception ex)
            {
                e.CancelEdit = true;
                RemoteTree.SelectedNode = null;
                AddLog(ex.Message);
                return;
            }
            AddLog("重命名成功！");
        }

        private void RemoveNode(TreeNode node)
        {
            if (node.Text == "")
            {
                node.Remove();
                return;
            }
            try
            {
                if (IsDirectory(node))
                {
                    myFTP.ChangeDir(node.Name);
                    node.Expand();
                    while (node.Nodes.Count > 0)
                    {
                        RemoveNode(node.Nodes[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                return;
            }


            AddLog($"正准备删除{node.Text}...");
            try
            {
                myFTP.ChangeDir(node.Parent.Name);
                if (IsDirectory(node)) myFTP.RemoveDir(node.Text);
                else myFTP.RemoveFile(node.Text);
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
                return;
            }
            node.Remove();
            AddLog($"删除成功！");
        }

        private void RemoteDeleteBtn_Click(object sender, EventArgs e)
        {
            if (RemoteTree.SelectedNode == null||RemoteNode.Parent==null) return;

            RemoveNode(RemoteNode);
        }

        private void RemoteNewFolderBtn_Click(object sender, EventArgs e)
        {
            if (RemoteTree.SelectedNode == null) return;
            InputForm inputForm = new InputForm("请输入新目录名：","文件夹名输入非法！");
            inputForm.ShowDialog();
            if (inputForm.DialogResult == DialogResult.Cancel)
            {
                return;
            }
            string NewFolderName = inputForm.Response;
            TreeNode ParentNode = RemoteNode;
            if (IsDirectory(RemoteNode) == false)
            {
                ParentNode = ParentNode.Parent;
            }
            try
            {
                AddLog($"正在创建目录 {NewFolderName}");
                myFTP.ChangeDir(ParentNode.Name);
                myFTP.MakeDir(NewFolderName);
                AddLog($"{NewFolderName}创建成功！");
                TreeNode node = new TreeNode(NewFolderName);
                node.Name = ParentNode.Name + NewFolderName + '/';
                node.Tag = node.Name;
                node.SelectedImageIndex = ImageIndex.FolderOpen;
                node.ImageIndex = ImageIndex.FolderClose;
                node.Nodes.Add("");
                ParentNode.Nodes.Add(node);
                ParentNode.Collapse();
                ParentNode.Expand();
            }
            catch(Exception ex)
            {
                AddLog(ex.Message);
            }

        }

        void DownloadFile(TreeNode node, TreeNode Lnode)
        {
            string newFileName = System.IO.Path.Combine(Lnode.Name, node.Text);
            if (File.Exists(newFileName))
            {//先默认覆盖重传
                int opcode = allowRep;
                if (opcode == 0)
                {
                    FileReplace fileReplace = new FileReplace(node.Text);
                    fileReplace.ShowDialog();
                    if (fileReplace.Flag) allowRep = fileReplace.Status;
                    opcode = fileReplace.Status;
                }
                if (opcode == -1) return;
                File.Delete(newFileName);
            }
            string tempFileName = Path.Combine(Lnode.Name, System.IO.Path.GetFileNameWithoutExtension(newFileName) + ".tmp");
            FileStream tmpStream=null;
            if (File.Exists(tempFileName))
            {
                try
                {
                    tmpStream = new FileStream(tempFileName, FileMode.Open);
                    byte[] bufs = new byte[14];
                    tmpStream.Read(bufs, 0, 14);
                    string oldDate = myFTP.ecd.GetString(bufs, 0, 14);
                    tmpStream.Close();
                    string newDate = myFTP.GetDate(node.Name);
                    if (oldDate != newDate)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    AddLog("缓存不可用，尝试重新下载：");
                    tmpStream.Close();
                    File.Delete(tempFileName);
                }
            }

            //准备缓存文件，并开始下载
            try
            {
                //重新生成缓存文件
                if (!File.Exists(tempFileName))
                {
                    tmpStream = new FileStream(tempFileName, FileMode.Create);
                    string newDate = myFTP.GetDate(node.Name);
                    byte[] tmps = myFTP.ecd.GetBytes(newDate);
                    //AddLog(tmps.Length.ToString());
                    tmpStream.Write(myFTP.ecd.GetBytes(newDate), 0, tmps.Length);
                    tmpStream.Close();
                }
                myFTP.DownLoadFile(tempFileName, node.Name);
            }
            catch (Exception ex)
            {
                tmpStream.Close();
                AddLog(ex.Message);
            }

            //保存，并删除缓存
            FileStream file = new FileStream(tempFileName, FileMode.Open);
            byte[] tmp2 = new byte[8192];
            file.Seek(14, SeekOrigin.Begin);
            FileStream saveFile = new FileStream(newFileName, FileMode.Create);
            while (true)
            {
                int BytesDone = file.Read(tmp2, 0, tmp2.Length);
                if (BytesDone <= 0)
                {
                    file.Close();
                    saveFile.Close();
                    break;
                }
                saveFile.Write(tmp2, 0, BytesDone);
            }
            File.Delete(tempFileName);
        }

        void DownloadDir(TreeNode Rnode,TreeNode Lnode)
        {//递归下载目录内的文件
            if (Rnode.Text == "") return;
            if (!IsDirectory(Rnode))
            {
                DownloadFile(Rnode, Lnode);
                return;
            }
            ReloadRemoteNode(Rnode);
            ReloadLocalNode(Lnode);
            AddLog($"准备开始下载目录{Rnode.Name}中的内容...");
            foreach (TreeNode node in Rnode.Nodes)
            {
                if (IsDirectory(node))
                {
                    string LocalDirName = System.IO.Path.Combine(Lnode.Name, node.Text);
                    TreeNode newNode = new TreeNode();
                    if (System.IO.Directory.Exists(LocalDirName) == false)
                    {
                        Directory.CreateDirectory(LocalDirName);
                        newNode = new TreeNode(Rnode.Text);
                        newNode.Name = LocalDirName;
                        newNode.Tag = newNode.Name;
                        Lnode.Nodes.Add(newNode);
                    }
                    else
                    {

                        foreach (TreeNode tmpnode in Lnode.Nodes)
                        {
                            if (tmpnode.Name == LocalDirName)
                            {
                                newNode = tmpnode;
                                break;
                            }
                        }
                    }
                    DownloadDir(node, newNode);
                }
                else
                {
                    
                    AddLog($"尝试下载文件{node.Name}...");
                    //DownloadFile()
                    DownloadFile(node, Lnode);
                    AddLog($"{node.Name}下载完毕！...");
                }
            }
            AddLog($"目录{Rnode.Name}中的内容下载完毕！");
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            if (RemoteNode == null) return;
            if (LocalNode == null) return;
            allowRep = 0;
            if (System.IO.File.Exists(LocalNode.Name))
            {
                DownloadDir(RemoteNode,LocalNode.Parent);
            }
            else
            {
                DownloadDir(RemoteNode, LocalNode);
            }
            ReloadLocalNode(LocalNode);
            ReloadRemoteNode(RemoteNode);
            return;
        }

        bool FindNode(TreeNode node,string name)
        {
            foreach(TreeNode tmp in node.Nodes) { if (tmp.Name == name) return true; }
            return false;
        }

        void UploadFile(TreeNode node, TreeNode Rnode)
        {
            if (Path.GetExtension(node.Name) == "tmp") return;
            string newFileName = Rnode.Name+node.Text;
            //ReloadRemoteNode(Rnode);
            if(FindNode(Rnode,newFileName))
            {//先确认是否替换or跳过
                int opcode = allowRep;
                if (opcode == 0)
                {
                    FileReplace fileReplace = new FileReplace(node.Text);
                    fileReplace.ShowDialog();
                    if (fileReplace.Flag) allowRep = fileReplace.Status;
                    opcode = fileReplace.Status;
                }
                if (opcode == -1) return;
                myFTP.RemoveFile(newFileName);
            }

            string tempFileName = Path.Combine(node.Parent.Name, System.IO.Path.GetFileNameWithoutExtension(node.Name) + ".tmp");
            string remoteTempFileName = Rnode.Name + Path.GetFileNameWithoutExtension(node.Text) + ".tmp";
            FileStream tmpStream = null;
            byte[] bufs = new byte[8192];

            if (FindNode(Rnode, remoteTempFileName))
            {
                try
                {
                    tmpStream = new FileStream(tempFileName, FileMode.Open);
                    tmpStream.Read(bufs, 0, 18);
                    string oldDate = myFTP.ecd.GetString(bufs, 0, 18);
                    tmpStream.Close();
                    string newDate = myFTP.GetDate(remoteTempFileName);
                    if (oldDate != newDate)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    AddLog("缓存不可用，尝试重新上传：");
                    if(tmpStream!=null)tmpStream.Close();
                    File.Delete(tempFileName);
                }
            }

            //准备缓存文件，并开始下载
            FileStream newStream = null;
            try
            {
                //重新生成缓存文件
                if (!File.Exists(tempFileName))
                {
                    tmpStream = new FileStream(tempFileName, FileMode.Create);
                    string newDate = File.GetLastWriteTime(node.Name).ToString();
                    byte[] tmps = myFTP.ecd.GetBytes(newDate);
                    //AddLog(tmps.Length.ToString());
                    tmpStream.Write(myFTP.ecd.GetBytes(newDate), 0, tmps.Length);
                    newStream = new FileStream(node.Name, FileMode.Open);
                    while(true)
                    {
                        int bytes = newStream.Read(bufs, 0, bufs.Length);
                        if (bytes <= 0)
                        {
                            tmpStream.Close();
                            newStream.Close();
                            break;
                        }
                        tmpStream.Write(bufs, 0, bytes);
                    }
                    tmpStream.Close();
                }
                myFTP.UploadFile(tempFileName, remoteTempFileName);
            }
            catch (Exception ex)
            {
                newStream.Close();
                tmpStream.Close();
                AddLog(ex.Message);
            }

            //远程重命名，并删除缓存
            myFTP.RenameDir(remoteTempFileName, newFileName);
            File.Delete(tempFileName);
        }

        void UploadDir(TreeNode Lnode,TreeNode Rnode)
        {//递归上传目录内的文件
            if (Rnode.Text == "") return;
            if (File.Exists(Lnode.Name))
            {
                UploadFile(Lnode,Rnode);
                return;
            }
            ReloadLocalNode(Lnode);
            ReloadRemoteNode(Rnode);
            AddLog($"准备开始上传目录{Lnode.Name}中的内容...");
            foreach (TreeNode node in Lnode.Nodes)
            {
                if (Directory.Exists(node.Name))
                {
                    string RemoteDirName = Rnode.Name + node.Text+'/';
                    TreeNode newNode = null;
                    foreach (TreeNode tmpnode in Rnode.Nodes)
                    {
                        if (tmpnode.Name == RemoteDirName)
                        {
                            newNode = tmpnode;
                            break;
                        }
                    }
                    if (newNode == null)
                    {
                        myFTP.MakeDir(RemoteDirName);
                        newNode = new TreeNode(node.Text);
                        newNode.Name = RemoteDirName;
                        newNode.Tag = newNode.Tag;
                        newNode.Nodes.Add("");
                        Rnode.Nodes.Add(newNode);
                    }
                    UploadDir(node, newNode);
                }
                else
                {

                    AddLog($"尝试上传文件{node.Name}...");
                    //DownloadFile()
                    UploadFile(node,Rnode);
                    AddLog($"{node.Name}上传完毕！...");
                }
            }
            ReloadRemoteNode(Rnode);
            AddLog($"目录{Rnode.Name}中的内容上传完毕！");
        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            if (RemoteNode == null) return;
            if (LocalNode == null) return;
            allowRep = 0;
            if (!IsDirectory(RemoteNode))
            {
                UploadDir(LocalNode,RemoteNode.Parent);
            }
            else
            {
                UploadDir(LocalNode, RemoteNode);
            }
            ReloadRemoteNode(RemoteNode);
            ReloadLocalNode(LocalNode);
            return;
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
