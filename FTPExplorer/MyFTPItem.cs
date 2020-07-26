using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPExplorer
{
    public class MyFTPItem:IComparable<MyFTPItem>
    {
        private string type;
        private long size;
        private string name;
        private string date;
        public MyFTPItem()
        {
            type = null;
            size = 0;
            date = null;
            name = null;
        }

        public string Type { get => type; set => type = value; }
        public long Size { get => size; set => size = value; }
        public string Name { get => name; set => name = value; }
        public string Date { get => date; set => date = value; }

        public int CompareTo(MyFTPItem other)
        {
            if (other.type != this.type) return this.type.CompareTo(other.type);
            return this.name.CompareTo(other.name);
        }
    }
}
