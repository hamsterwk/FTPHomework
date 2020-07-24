﻿using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

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
		private Encoding ecd;

		private Socket DataSock;
		private IPEndPoint DataIPEndPoint;


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
			byte[] commandBytes = Encoding.UTF8.GetBytes((command + "\r\n").ToCharArray());
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
			string rtn;
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
			return new MyFTPResponse(rtn);
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

		public string GetList()
		{
			byte[] bufs = new byte[1024];
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

			while (DataSock.Available > 0)
			{
				receiveBytes = DataSock.Receive(bufs, bufs.Length, 0);
				fileList += ecd.GetString(bufs, 0, receiveBytes);
				System.Threading.Thread.Sleep(50);
			}

			CloseDataSock();
			myFTPResponse = GetFTPResponse();
			if (myFTPResponse.Status != 226)
			{
				Fail(myFTPResponse);
			}

			return fileList;
		}

	}
}
