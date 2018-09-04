﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace surveillanceProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("172.17.128.17");
            IPEndPoint ipep = new IPEndPoint(ipAddress, 9001);
            
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] temp = new byte[100];
            server.Connect(ipep);
            server.Send(new byte[] { 10 });
            server.Receive(temp);//LeagueClient
           
            string[] programList = { "LeagueClient", "Google Chrome" };
            foreach (string program in programList)
            {
                for (int i = 0; i < 20; i++)
                {
                    var info = Process.GetProcessesByName(program).FirstOrDefault();

                    if (info != null)
                    {
                        try
                        {
                            Console.WriteLine(info.MainModule.FileVersionInfo.ProductName);
                            Console.WriteLine(info.MainModule.FileVersionInfo.FileDescription);
                            info.Kill();
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }
}
