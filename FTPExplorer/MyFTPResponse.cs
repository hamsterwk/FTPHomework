using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPExplorer
{
    public class MyFTPResponse
    {
        private int status;
        private string messages;

        public MyFTPResponse()
        {
            status = 0;
            messages = "";
        }

        public MyFTPResponse(int _status,string _msg)
        {
            status = _status;
            messages = _msg;
        }

        public MyFTPResponse(string response)
        {
            try
            {
                status = int.Parse(response.Substring(0, 3));
                messages = response;
            }
            catch
            {
                status = -1;
                messages = response;
            }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Message
        {
            get { return messages; }
            set { messages = value; }
        }
    }
}
