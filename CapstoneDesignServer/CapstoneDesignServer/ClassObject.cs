using System.Collections.Generic;

namespace CapstoneDesignServer
{
    public class ClassObject
    {
        public string className { get; set; }
        public List<SocketObject> students = new List<SocketObject>();
        public SocketObject professor = null;

        static List<ClassObject> classObjects = Program.classObjects;
        public static ClassObject GetClassObjects(string className)
        {
            foreach (ClassObject tempClassObject in classObjects)
            {
                if (tempClassObject.className == className)
                {
                    return tempClassObject;
                }
            }
            return null;
        }
    }
}
