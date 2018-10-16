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
        static List<SocketObject> students = new List<SocketObject>();
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
                        Task.Run(() => Recive());
                    }
                    else
                    {
                        students.Add(client);
                        Console.WriteLine("학생 접속!");
                        client.Send("TurnOnLogin");
                        Task.Run(() => StudentReceive(client));
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void StudentReceive(SocketObject student)
        {
            while (true)
            {
                if (professor != null)
                {
                    string temp = student.Receive();
                    if (temp == null)
                    {
                        students.Remove(student);
                    }
                    try {
                        professor.Send(temp);
                    }
                    catch
                    {
                        professor = null;
                    }
                }
            }
        }

        static void Recive()
        {
            while (professor != null)
            {
                string comment = "";
                try
                {
                    comment = professor.Receive();
                    if(comment == null)
                    {
                        professor = null;
                    }else if(comment == "")
                    {
                        professor = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    professor = null;
                }
                Console.WriteLine(comment);
                Console.WriteLine($"학생수 : {students.Count}");
                for(int index=0; index<students.Count; index++)
                {
                    if (!students[index].Send(comment))
                    {
                        Console.WriteLine($"학생오류");
                        students.RemoveAt(index);
                    }
                }
            }
        }
    }
}
