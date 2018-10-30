using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneDesignServer
{
    public class SocketObject
    {
        Socket client;
        public string className { get; set; }
        public SocketObject(Socket socket)
        {
            client = socket;
        }
        
        public string Receive()
        {
            string dataString = "";
            try
            {
                byte[] data = new byte[128 * 50];
                client.Receive(data);
                foreach (char r in Encoding.UTF8.GetString(data))
                {
                    if (r != 0)
                    {
                        dataString += r;
                    }
                    else if (r == 0)
                    {
                        break;
                    }
                }
            }
            catch
            {
                dataString = null;
            }
            return dataString;
        }

        public bool Send(string content)
        {
            try
            {
                client.Send(Encoding.UTF8.GetBytes(content));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Close()
        {
            client.Close();
        }
    }
}
