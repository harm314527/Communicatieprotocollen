using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace TCP_Server
{
    class TCPServer
    {
        private string IP { get; set; }
        private int Port { get; set; }
        private TcpListener tcpServer { get; set; }
        private TcpClient Client { get; set; }
        private NetworkStream Stream { get; set; }

        //TCP Listener is made and started
        public TCPServer(string ip, int port)
        {
            IP = ip;
            Port = port;
            IPAddress ipadres = IPAddress.Parse(IP);
            tcpServer = new TcpListener(ipadres, Port);
       
            tcpServer.Start();
        }

        //Server is looking for pending connections
        public bool ClientsWaiting()
        {
            bool availeble = tcpServer.Pending();
            return availeble;
        }
        //Server is accepting a client and fills a client object for data froma dat client
        public bool ConnectToClient()
        {
            Client = tcpServer.AcceptTcpClient();
            Stream = Client.GetStream();
            if (Stream != null)
            {
                return true;
            }
            return false;
        }
        public string Receive()
        {
            if (Stream.CanRead)
            {

                while(Stream.DataAvailable)
                {
                    byte[] message = new byte[1024];
                    //Span<byte> message = null;
                    int count = Stream.Read(message, 0, 1024);
                    if (count > 0)
                    {
                        string msg = Encoding.UTF8.GetString(message, 0, count);

                        Stream.Flush();
                        return msg;
                    
                    }
                }
                
            }
            return "CANTREAD";
        }
        public void SendToClient(string message)
        {
            if (Stream.CanWrite)
            {
                
                byte[] msg = Encoding.Default.GetBytes(message);
                if (msg.Length > 0)
                {
                    Stream.Write(msg, 0, msg.Length);
                }
            }
        }
        //both server and client are stopped
        public void StopServer()
        {
            tcpServer.Stop();
        }
    }
}
