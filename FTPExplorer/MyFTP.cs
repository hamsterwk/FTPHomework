using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FTPExplorer
{
	public class MyFTP
	{

		public static int TIMEOUT = 5000;//固定超时时长为5s

		public string Server;//服务器地址
		public string UserName;//用户名
		public string Password;//登录使用的密码
		public int Port;//端口

		/*主套接字的声明部分*/
		private Socket MainSock;
		private IPEndPoint MainIPEndPoint;
		public Encoding ecd;

		/*数据操作相关*/
		private Socket DataSock;
		private IPEndPoint DataIPEndPoint;
		private FileStream file;


		private string BufferPool;//接受字符缓存池
		private MyFTPResponse myFTPResponse;

		public MyFTP()
		{
			this.Server = null;
			this.UserName = null;
			this.Password = null;
			this.Port = 21;//默认端口为21。

			this.MainSock = null;
			this.MainIPEndPoint = null;
			this.ecd = Encoding.UTF8;

			this.DataSock = null;
			this.DataIPEndPoint = null;
			this.file = null;

			this.myFTPResponse = new MyFTPResponse();
			this.BufferPool = "";
		}
		//构造FTP对象：传入参数为服务器地址+端口号+用户名+密码
		public MyFTP(string server, int port, string user, string pass)
		{
			this.Server = server;
			this.UserName = user;
			this.Password = pass;
			this.Port = port;

			this.MainSock = null;
			this.MainIPEndPoint = null;
			this.ecd = Encoding.UTF8;

			this.DataSock = null;
			this.DataIPEndPoint = null;
			this.file = null;

			this.myFTPResponse = new MyFTPResponse();
			this.BufferPool = "";
		}

		//获取本机IP

		public string GetLocalIP()
		{
			string HostName = Dns.GetHostName(); //得到主机名
			IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
			for (int i = 0; i < IpEntry.AddressList.Length; i++)
			{
				//从IP地址列表中筛选出IPv4类型的IP地址
				//AddressFamily.InterNetwork表示此IP为IPv4,
				//AddressFamily.InterNetworkV6表示此地址为IPv6类型
				if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
				{
					string ip = "";
					ip = IpEntry.AddressList[i].ToString();
					return IpEntry.AddressList[i].ToString();
				}
			}
			return "";
		}

		//判端当前FTP链接情况
		public bool Connected()
		{
			if (MainSock == null) return false;
			return MainSock.Connected;
		}

		//链接异常的处理
		private void Fail(MyFTPResponse myFTPResponse)
		{
			this.CloseConnect();
			throw new Exception(myFTPResponse.Message);
		}

		//向远程服务器发送命令
		private void SendCommand(string command)
		{
			byte[] commandBytes = ecd.GetBytes((command + "\r\n").ToCharArray());
			this.MainSock.Send(commandBytes);
		}

		/**********************
		 * 接受远程服务器的指令
		 * 首先需要接受远程服务器发来的字节，存入缓存池
		 * 然后需要从缓存池内逐行读取接收到的信息
		 **********************/

		private void GetBuffer()
		{
			byte[] bufs = new byte[256];//每次读取的缓存大小
			int receivedBytes;
			int tickPassed = 0;

			//等待套接字可用
			while (this.MainSock.Available < 1)
			{
				System.Threading.Thread.Sleep(50);
				tickPassed += 50;

				if (tickPassed > MyFTP.TIMEOUT)
				{
					this.CloseConnect();
					throw new Exception("Connection Timed Out");
				}
			}

			//读取字节流
			while (this.MainSock.Available > 0)
			{
				receivedBytes = this.MainSock.Receive(bufs, 256, 0);
				this.BufferPool += ecd.GetString(bufs, 0, (int)receivedBytes);
			}
		}

		//从字节流中读取一整行字符
		private string GetLine()
		{
			string rtn = "";
			while (BufferPool.IndexOf('\n') < 0) GetBuffer();
			int idx = BufferPool.IndexOf('\n');
			rtn = BufferPool.Substring(0, idx);
			BufferPool = BufferPool.Substring(idx + 1);
			return rtn;
		}

		public MyFTPResponse GetFTPResponse()
		{
			string rtn = "";
			while (true)
			{
				string buf = GetLine();
				// "000 This is the end of the response"
				if (Regex.Match(buf, "^[0-9]+ ").Success)
				{
					MyFTPResponse tmp = new MyFTPResponse(buf);
					return new MyFTPResponse(tmp.Status, rtn + tmp.Message);
				}
				else
				{
					rtn += buf;
				}
			}
		}

		/*
		 * Socket相关
		 * (默认使用被动模式)
		 * DataSock的open,connect,close
		 * MainSock的open,connect,close
		 */

		public void Connect()
		{
			//验证用户名与服务器是否为空
			if (this.Server == null)
			{
				throw new Exception("服务器为空！");
			}

			if (this.UserName == null)
			{
				throw new Exception("用户名为空！");
			}
			//如果已经连上，则不用做任何事情。
			if (this.MainSock != null)
			{
				if (this.MainSock.Connected)
				{
					return;
				}
			}

			//连接到FTP服务器
			this.MainSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.MainIPEndPoint = new IPEndPoint(Dns.GetHostByName(this.Server).AddressList[0], this.Port);
			//this.MainSock.Bind(ipEndPoint);

			try
			{
				this.MainSock.Connect(this.MainIPEndPoint);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 220)
			{
				this.Fail(myFTPResponse);
			}

			this.SendCommand("USER " + this.UserName);
			myFTPResponse = GetFTPResponse();

			switch (myFTPResponse.Status)
			{
				case 331:
					if (this.Password == null)
					{
						this.CloseConnect();
						throw new Exception("No password has been set.");
					}

					this.SendCommand("PASS " + this.Password);
					myFTPResponse = GetFTPResponse();
					if (myFTPResponse.Status != 230)
					{
						this.Fail(myFTPResponse);
					}

					break;
				case 230:
					break;
			}

			return;
		}


		//打开DataSocket
		private void OpenDataSock()
		{
			Connect();
			SendCommand("PASV");
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 227)
			{
				Fail(myFTPResponse);
			}

			//判断227回应是否正常

			string[] pasvResponse;
			try
			{
				string tmp = Regex.Replace(myFTPResponse.Message, "(.*)(\\()(.*)(\\))(.*)", "$3");
				pasvResponse = tmp.Split(',');
				if (pasvResponse.Length < 6) throw new Exception($"PASV回应非法！（{ myFTPResponse.Message }）");
			}
			catch
			{
				CloseConnect();
				throw new Exception($"PASV回应非法！（{ myFTPResponse.Message }）");
			}

			string server = string.Format("{0}.{1}.{2}.{3}", pasvResponse[0], pasvResponse[1], pasvResponse[2], pasvResponse[3]);
			int port = (int.Parse(pasvResponse[4]) << 8) + int.Parse(pasvResponse[5]);

			//建立DataSocket
			try
			{
				//先关闭已有的Socket
				CloseDataSock();

				//创建新的DataSocket
				DataSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				DataIPEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);

				//连接
				DataSock.Connect(DataIPEndPoint);
			}
			catch (Exception ex)
			{
				throw new Exception("无法建立数据传输！ " + ex.Message);
			}
		}

		//关闭DataSocket
		private void CloseDataSock()
		{
			DataIPEndPoint = null;
			if (DataSock == null) return;
			if (DataSock.Connected)
			{
				DataSock.Close();
			}
			DataSock = null;
		}

		//关闭MainSocket
		public void CloseConnect()
		{
			CloseDataSock();
			MainIPEndPoint = null;
			if (MainSock == null) return;
			if (MainSock.Connected)
			{
				SendCommand("QUIT");
				MainSock.Close();
			}
			MainSock = null;
		}

		/*
		 * 获取文件目录
		*/

		/*
		 * 获取文件目录（返回列表）
		 *  -r--r--r-- 1 ftp ftp         202752 Apr 16  2020 18级os设计讲稿.doc
			-r--r--r-- 1 ftp ftp        2115458 May 22  2020 2018302110186-吴轲-操作系统实验 报告.docx
			drwxr-xr-x 1 ftp ftp              0 Jul 24 23:00 619
			drwxr-xr-x 1 ftp ftp              0 Jul 24 23:00 624
			drwxr-xr-x 1 ftp ftp              0 Jul 24 23:00 723
		*/

		public string GetList()
		{
			byte[] Recbufs = new byte[1024];
			byte[] bufs = new byte[1030];
			int receiveBytes;
			string fileList = "";
			ArrayList list = new ArrayList();

			Connect();
			OpenDataSock();
			SendCommand("LIST -l");
			myFTPResponse = GetFTPResponse();

			if (myFTPResponse.Status != 150 && myFTPResponse.Status != 125)
			{
				Fail(myFTPResponse);
			}
			int tmp = 0;
			while (DataSock.Available > 0)
			{
				receiveBytes = DataSock.Receive(Recbufs, Recbufs.Length, 0);
				for (int i = 0; i < receiveBytes; i++) bufs[i + tmp] = Recbufs[i];
				tmp = 0;
				string tmpList = ecd.GetString(bufs, 0, receiveBytes);
				
				while (tmpList[tmpList.Length - 1] == 65533)
				{
					tmp++;
					tmpList = ecd.GetString(bufs, 0, receiveBytes-tmp);
				}
				for (int i = 0; i < tmp; i++) bufs[i] = Recbufs[receiveBytes - tmp + i];
				fileList += tmpList;
				System.Threading.Thread.Sleep(50);
			}

			CloseDataSock();
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 226)
			{
				Fail(myFTPResponse);
			}

			CloseConnect();
			return fileList;
		}

		public List<MyFTPItem> GetFileList()
		{
			string fileList = GetList();
			//fileList = Regex.Replace(fileList, " +", " ");
			List<MyFTPItem> list = new List<MyFTPItem>();
			foreach (string f in fileList.Split('\n'))
			{
				if (f.Length > 0 && !Regex.Match(f, "^total").Success)
				{
					
					string tmp = Regex.Replace(f,"\r","");
					MyFTPItem newItem = new MyFTPItem();
					newItem.Name = tmp.Substring(49);
					tmp = Regex.Replace(tmp, " +", " ");
					string[] tmpList = tmp.Split(' ');
					if ((f[0] != 'd') && (f.ToUpper().IndexOf("<DIR>") < 0))
					{
						newItem.Type = "file";
					}
					else
					{
						newItem.Type = "dir";
					}
					newItem.Size = long.Parse(tmpList[4]);
					list.Add(newItem);
				}
			}
			list.Sort();
			return list;
		}

		public string GetWorkingDirectory()
		{
			// PWD - print working directory
			Connect();
			SendCommand("PWD");
			myFTPResponse = GetFTPResponse();

			if (myFTPResponse.Status != 257)
			{
				throw new Exception(myFTPResponse.Message);
			}

			string pwd;
			try
			{
				pwd = myFTPResponse.Message.Substring(myFTPResponse.Message.IndexOf("\"", 0) + 1); //5;
				pwd = pwd.Substring(0, pwd.LastIndexOf("\""));
				pwd = pwd.Replace("\"\"", "\""); // directories with quotes in the name come out as "" from the server
			}
			catch (Exception ex)
			{
				CloseConnect();
				throw new Exception("Uhandled PWD response: " + ex.Message);
			}
			CloseConnect();
			return pwd;
		}

		/**
		 * 修改 删除 创建 重命名
		 */
		public void ChangeDir(string path)
		{
			Connect();
			this.SendCommand("CWD " + path);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 250)
			{
				throw new Exception(myFTPResponse.Message);
			}

		}

		public void RenameDir(string oldName,string newName)
		{
			Connect();
			SendCommand("RNFR " + oldName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 350)
			{
				CloseConnect();
				throw new Exception(myFTPResponse.Message);
			}
			SendCommand("RNTO " + newName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 250)
			{

				throw new Exception(myFTPResponse.Message);
			}

		}

		public void RemoveDir(string dir)
		{
			Connect();
			SendCommand("RMD " + dir);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 250)
			{
				throw new Exception(myFTPResponse.Message);
			}
		}

		public void MakeDir(string dir)
		{
			Connect();
			this.SendCommand("MKD " + dir);
			myFTPResponse = GetFTPResponse();

			switch (myFTPResponse.Status)
			{
				case 257:
				case 250:
					break;
				default:
					throw new Exception(myFTPResponse.Message);
			}
		}

		public void RemoveFile(string fileName)
		{
			Connect();
			SendCommand("DELE " + fileName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 250)
			{
				throw new Exception(myFTPResponse.Message);
			}
		}

		/*
		 * 下载部分的实现（已实现断点续传）
		 * 首先需要搞清楚本地文件下载了多少，然后转告服务器
		 * 利用RETR命令即可实现
		 * 但是有可能在发起续传的时候，服务器端文件已经发生更新，那么此时必须重新开始传输
		 * 因此需要比对本地文件的修改时间与远程的修改时间
		 * 解决方法：将缓存文件命名为：<文件名>.tmp
		 * 在文件开头插入时间戳
		 */

		//设置二进制模式
		private void SetBinaryMode(bool mode)
		{
			if (mode)SendCommand("TYPE I");
			else SendCommand("TYPE A");

			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 200)
			{
				Fail(myFTPResponse);
			}
		}

		public string GetDate(string fileName)
		{//从远程服务器获取所需文件的日期
			Connect();

			SendCommand("MDTM " + fileName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 213)
			{
				throw new Exception(myFTPResponse.Message);
			}

			return Regex.Replace(myFTPResponse.Message.Substring(4),"\r","");
		}

		//DownLoadFile("D:\\in.txt",string "in.txt")
		public void DownLoadFile(string LocalDirName,string RemotefileName,int StartOffset=14)
		{
			//首先建立下载请求
			Connect();
			SetBinaryMode(true);
			OpenDataSock();
			file = new FileStream(LocalDirName, FileMode.Append);
			SendCommand("REST " + (file.Length-StartOffset).ToString());
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 350)
			{
				CloseDataSock();
				file = null;
				throw new Exception(myFTPResponse.Message);
			}

			SendCommand("RETR " + RemotefileName);
			myFTPResponse = GetFTPResponse();

			if (myFTPResponse.Status != 125 && myFTPResponse.Status!=150)
			{
				file = null;
				throw new Exception(myFTPResponse.Message);
			}

			//开始下载
			byte[] bytes = new byte[4096];
			long bytesGot;
			while (true)
			{
				try
				{
					bytesGot = DataSock.Receive(bytes, bytes.Length, 0);
					if (bytesGot <= 0)
					{
						CloseDataSock();
						file.Close();
						myFTPResponse = GetFTPResponse();
						if(myFTPResponse.Status!=250&& myFTPResponse.Status != 226)
						{
							throw new Exception(myFTPResponse.Message);
						}

						SetBinaryMode(false);
						return;
					}
					file.Write(bytes, 0, (int)bytesGot);
				}
				catch(Exception ex)
				{
					CloseDataSock();
					if (file != null)
					{
						file.Close();
						file = null;
					}
					GetFTPResponse();
					SetBinaryMode(false);
					throw (ex);
				}
			}
		}

		/*
		 * 上传部分的实现（已实现断点续传）
		 * 首先需要搞清楚本地文件上传了多少，然后转告服务器
		 * 利用RETR命令即可实现
		 * 但是有可能在发起续传的时候，服务器端文件已经发生更新，那么此时必须重新开始传输
		 * 因此需要比对本地文件的修改时间与远程的修改时间
		 * 解决方法：将缓存文件命名为：<文件名>.tmp
		 * 在文件开头插入时间戳
		 */

		public long GetFileSize(string filename)
		{//获取远程文件大小
			Connect();
			SendCommand("SIZE " + filename);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 213)
			{
				throw new Exception(myFTPResponse.Message);
			}
			return long.Parse(myFTPResponse.Message.Substring(4));
		}

		public void UploadFile(string LocalfileName, string RemotefileName,int StartOffset = 18)
		{
			//首先建立上传请求
			Connect();
			SetBinaryMode(true);
			OpenDataSock();
			//获取远程文件的大小
			long sizeReceived;
			SendCommand("SIZE " + RemotefileName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 213)
			{
				if (myFTPResponse.Status == 550)
				{
					sizeReceived = 0;
				}
				else throw new Exception(myFTPResponse.Message);
			}else sizeReceived = long.Parse(myFTPResponse.Message.Substring(4));

			file = new FileStream(LocalfileName, FileMode.Open);
			file.Seek(StartOffset + sizeReceived, SeekOrigin.Begin);

			SendCommand("APPE " + RemotefileName);
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 150&&myFTPResponse.Status!=125)
			{
				throw new Exception(myFTPResponse.Message);
			}

			byte[] buf = new byte[8192];
			int bytes;

			while (true)
			{
				try
				{
					bytes = file.Read(buf, 0, buf.Length);
					DataSock.Send(buf, bytes, 0);

					if (bytes <= 0)
					{
						file.Close();
						file = null;
						CloseDataSock();
						myFTPResponse = GetFTPResponse();
						if (myFTPResponse.Status != 250 && myFTPResponse.Status != 226)
						{
							throw new Exception(myFTPResponse.Message);
						}

						SetBinaryMode(false);
						return;
					}
				}catch(Exception ex)
				{
					if(file!=null)file.Close();
					file = null;
					CloseDataSock();
					GetFTPResponse();
					throw new Exception(ex.Message);
				}

			}
		}
	}
}
