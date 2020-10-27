using System;
using System.Collections.Generic;
using System.Text;

namespace TCP_Server
{
    interface IServer
    {
        public bool connectClient();
        public string receive();
        public void send(string message);
    }
}
