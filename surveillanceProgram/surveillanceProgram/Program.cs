﻿using CapstoneDesign;
using System;
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
            SocketObject server = new SocketObject();
            server.Send("student");
            string temp =  server.Receive();
            switch (temp)
            {
                case "TurnOffGame":
                    KillGame();
                    break;
                case "TurnOffScreen":
                    break;
                case "TurnOffComputer":
                    break;

            }
            
        }

        private static void TrunOffSomputer()
        {
            Process.Start("shutdown.exe", "-s -f");
        }

        private static void FillScreen()
        {
            // Change WPF
        }

        private static void KillGame()
        {
            string[] programList = { /*"LeagueClient" ,"MapleStory", "KakaoTalk",*/ "chrome" };
            foreach (string program in programList)
            {
                for (int i = 0; i < 20; i++)
                {
                    var info = Process.GetProcessesByName(program).FirstOrDefault();
                    
                    if (info != null)
                    {
                        try
                        {
                            info.Kill();
                            info.Close();
                            info.Dispose();
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
