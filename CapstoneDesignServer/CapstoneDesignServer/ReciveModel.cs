using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneDesignServer
{
    class ReciveModel
    {
        public ReciveModel(SocketObject socketObject)
        {
            while (true)
            {
                string socketRecive = socketObject.Receive();
                Program.professor.Send("게임 감지!");
            }
        }
    }
}
