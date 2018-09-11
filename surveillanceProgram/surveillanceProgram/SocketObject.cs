using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneDesign
{
    public class SocketObject
    {
        Socket client;
        public SocketObject()
        {
            this.client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("121.65.76.85");
            IPEndPoint ipep = new IPEndPoint(ipAddress, 9001);
            client.Connect(ipep);
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

        public void Send(string content)
        {
            try
            {
                client.Send(Encoding.UTF8.GetBytes(content));
            }
            catch
            {

            }
            return;
        }

        public void Close()
        {
            client.Close();
        }
    }
}
