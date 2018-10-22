﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CapstoneDesignServer
{
    public class Program
    {
        public static List<ClassObject> classObjects = new List<ClassObject>();
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
                    string clientClassName = client.Receive().Split('.')[2];
                    client.className = clientClassName;
                    ClassObject classObject = ClassObject.GetClassObjects(clientClassName);

                    if (classObject == null)
                    {
                        classObject = new ClassObject()
                        {
                            className = clientClassName,
                            students = new List<SocketObject>(),
                        };
                        classObjects.Add(classObject);
                    }

                    if (client.Receive() == "pro")
                    {
                        classObject.professor = client;
                        Console.WriteLine("교수님 접속!");
                        Task.Run(() => Recive(client));
                    }
                    else if(client.Receive() == "student")
                    {
                        classObject.students.Add(client);
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
                ClassObject classObject = ClassObject.GetClassObjects(student.className);
                if (classObject.professor != null)
                {
                    string temp = student.Receive();
                    if (temp == null)
                    {
                        classObject.students.Remove(student);
                    }
                    try {
                        classObject.professor.Send(temp);
                    }
                    catch
                    {
                        classObject.professor = null;
                    }
                }
            }
        }

        static void Recive(SocketObject student)
        {
            ClassObject classObject = ClassObject.GetClassObjects(student.className);
            while (classObject.professor != null)
            {
                string comment = "";
                try
                {
                    comment = classObject.professor.Receive();
                    if(comment == null)
                    {
                        classObject.professor = null;
                    }else if(comment == "")
                    {
                        classObject.professor = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    classObject.professor = null;
                }
                Console.WriteLine(comment);
                Console.WriteLine($"학생수 : {classObject.students.Count}");
                for(int index=0; index< classObject.students.Count; index++)
                {
                    if (!classObject.students[index].Send(comment))
                    {
                        Console.WriteLine($"학생오류");
                        classObject.students.RemoveAt(index);
                    }
                }
            }
        }
    }
}
