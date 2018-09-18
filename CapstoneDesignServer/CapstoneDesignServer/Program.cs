using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CapstoneDesignServer
{
    public class Program
    {
        static List<SocketObject> sockets = new List<SocketObject>();
        public static SocketObject professor = null;
        static string login = "TurnOnLogin";
        static void Main(string[] args)
        {
            Socket server = null;
            int port = 9001;
            IPEndPoint ipep;
            if (server == null)
            {
                ipep = new IPEndPoint(IPAddress.Any, port);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Bind(ipep);
                server.Listen(10);
            }
            try
            {
                while (true)
                {
                    SocketObject client = new SocketObject(server.Accept());
                    if (client.Receive() == "pro")
                    {
                        professor = client;
                        Console.WriteLine("교수님 접속!");
                        new Thread(Recive).Start();
                    }
                    else
                    {
                        sockets.Add(client);
                        Console.WriteLine("학생 접속!");
                        client.Send(login);
                        new Thread(StudentReceive).Start();
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void StudentReceive()
        {
            SocketObject student = sockets[sockets.Count-1];
            while (true)
            {
                if (professor != null)
                {
                    string temp = student.Receive();
                    professor.Send(temp);
                }
            }
        }

        static void Recive()
        {
            while (true)
            {
                string comment = professor.Receive();
                Console.WriteLine(comment);
                if(comment == "TurnOffLogin")
                {
                    login = "TurnOffLogin";
                }
                else if(comment == "TurnOnLogin")
                {
                    login = "TurnOnLogin";
                }
                Console.WriteLine($"학생수 : {sockets.Count}");
                for (int index = 0; index < sockets.Count; index++)
                {
                    try
                    {
                        sockets[index].Send(comment);
                    }
                    catch
                    {
                        sockets.RemoveAt(index);
                    }
                }
            }
        }
    }
}
