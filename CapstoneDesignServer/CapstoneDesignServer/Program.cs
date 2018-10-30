using System;
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
                    string clientClassName = "";
                    try
                    {
                        clientClassName = client.Receive().Split('.')[2];
                    }
                    catch
                    {
                        Console.WriteLine("구버전 접속");
                        client.Close();
                        continue;
                    }
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

                    string clientType = client.Receive();
                    if (clientType == "professor")
                    {
                        classObject.professor = client;
                        Console.WriteLine($"{classObject.className} : 교수님 접속!");
                        Task.Run(() => Recive(client));
                    }
                    else if(clientType == "student")
                    {
                        classObject.students.Add(client);
                        Console.WriteLine($"{classObject.className} : 학생 접속!");
                        client.Send("TurnOnLogin");
                        client.Send("startGameSearching");
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
                        student.Close();
                        classObject.students.Remove(student);
                    }
                    try {
                        classObject.professor.Send(temp);
                    }
                    catch
                    {
                        classObject.professor.Close();
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
                        classObject.professor.Close();
                        classObject.professor = null;
                    }
                    else if(comment == "")
                    {
                        classObject.professor.Close();
                        classObject.professor = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    classObject.professor.Close();
                    classObject.professor = null;
                }
                Console.WriteLine(comment);
                Console.WriteLine($"{classObject.className} , 학생수 : {classObject.students.Count}");
                for(int index=0; index< classObject.students.Count; index++)
                {
                    if (!classObject.students[index].Send(comment))
                    {
                        Console.WriteLine($"{classObject.className} 학생오류");
                        classObject.students.RemoveAt(index);
                    }
                }
            }
        }
    }
}
