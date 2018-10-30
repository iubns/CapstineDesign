﻿using System.Net.Sockets;
using System.Text;

namespace StudentControl
{
    public class SocketObject
    {
        Socket client;
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
