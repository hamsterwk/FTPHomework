## Form1

![image-20200723223415215](C:\Users\OkitaSan\AppData\Roaming\Typora\typora-user-images\image-20200723223415215.png)

### 连接状态

连接状态设置中，其中的label是连接指示灯，指示当前的连接状态。

具体实现的话可参见<a href="http://www.luofenming.com/show.aspx?id=ART2018022600001">这个博文</a>

然后如果要更改连接状态设置的话，可以点击联机状态设置得按钮，会进入联机状态设置的窗口（也就是下文提到的FTPConnectForm）

### 编码

右侧有三个编码，UTF8,GBK和ASCII。方便用户根据需要选择自己需要的编码

### 从本地选择文件

本地路径中，先点击图片里的button,选择一个文件夹，然后就会在localFileBox中显示文件夹里的文件，同时label4会更新对应的目录。选择一个文件，点击上传即可把文件传上去。

### 从远程下载文件

label6显示对应服务器的ip.serverFileBox显示服务器中的文件。选择一个文件，点击下载就可以把文件下载下来。

### 程序日志

上传下载文件中的日志都会出现在里面。

## FTPConnectForm

![image-20200723224905173](C:\Users\OkitaSan\AppData\Roaming\Typora\typora-user-images\image-20200723224905173.png)

### 服务器地址

应填入对应的服务器地址

### 端口

填上对应的端口

### 用户名

填上对应的用户名

### 登陆密码

填上对应的密码

## 对应控件在程序中的名字

| 控件名                                                | 对应代码名       |
| ----------------------------------------------------- | ---------------- |
| Form1中指示灯                                         | connectLightLbl  |
| Form1中连接状态设置按钮                               | connectBtn       |
| Form1中UTF8编码状态                                   | UTF8RadioBtn     |
| Form1中GKB编码状态                                    | GBKRadioBtn      |
| Form1中ASCII编码状态                                  | ASCIIRadioBtn    |
| Form1中本地路径中显示的label4，用来显示本地文件夹路径 | pathIndexLbl     |
| Form1中label4后面跟的button                           | fileIndexBtn     |
| Form1中localFileBox                                   | localFileBox     |
| Form1中上传按钮                                       | uploadBtn        |
| Form1中label6                                         | ipIndexLbl       |
| Form1中serverFileBox                                  | serverFileBox    |
| Form1中下载按钮                                       | downloadBtn      |
| Form1中logBox                                         | logBox           |
| Form2中服务器链接地址文本框                           | serverAddressBox |
| Form2中端口文本框                                     | portBox          |
| Form2中登陆用户名                                     | loginNameBox     |
| Form2中登陆密码                                       | loginPwdBox      |
| Form2中测试连接                                       | testConnectBtn   |
| Form2中连接                                           | ftpConnectBtn    |

