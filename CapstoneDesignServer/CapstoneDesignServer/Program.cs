using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CapstoneDesignServer
{
    public class Program
    {
        static List<SocketObject> sockets = new List<SocketObject>();
        public static SocketObject professor = null;
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
                        Recive();
                    }
                    else
                    {
                        sockets.Add(client);
                        Console.WriteLine("학생 접속!");
                        StudentReceive(client); //비동기 오류
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task StudentReceive(SocketObject student)
        {
            if (professor != null)
            {
                professor.Send(student.Receive());
                await Task.Delay(10);
            }
        }

        static async Task Recive()
        {
            while (true)
            {
                string comment = professor.Receive();
                Console.WriteLine(comment);
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
                await Task.Delay(10);
            }
        }
    }
}
