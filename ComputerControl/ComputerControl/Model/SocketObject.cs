using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CapstoneDesign
{
    public class SocketObject
    {
        Socket client;
        public SocketObject()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("172.17.128.18");// "121.65.76.85"
            IPEndPoint ipep = new IPEndPoint(ipAddress, 9001);
            while (!client.Connected)
            {
                Thread.Sleep(1000);
                try
                {
                    client.Connect(ipep);
                }
                catch
                {

                }
            }
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
