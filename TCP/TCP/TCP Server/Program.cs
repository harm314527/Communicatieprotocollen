using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TCP_Server
{

    class Program
    {
        static bool ServerStarted = false;
        static bool Clientconnected = false;
        static String input { get; set; }
        static TCPServer server;
        
        
        static void askinput()
        {
            Console.WriteLine("Geef input voor Server");
            input = new string(Console.ReadLine());   
        }
        static void Main()
        {
           
                
                askinput();
                executeServer();

            
        }
        static private void executeServer()
        {
            if(input == "Start")
            {

                if (!ServerStarted && input != "Exit")
                {
                    Console.WriteLine("Voer een IP adres in voor de Server");
                    string ip = Console.ReadLine();
                    Console.WriteLine("Voer een poortnummer in");
                    string inputport = Console.ReadLine();
                    server = new TCPServer(ip, Convert.ToInt32(inputport));
                    ServerStarted = true;
                }
                bool waiting = false;
                while(waiting == false)
                {
                    if (server.ClientsWaiting())
                    {
                        waiting = true;
                        Console.WriteLine("Clients Waiting");
                        if (server.ConnectToClient())
                        {
                            Clientconnected = true;
                            Console.WriteLine("Client Connected");
                        }
                    }
                }
                 
                if (Clientconnected && input != "Exit")
                {
                    
                    while (true)
                    {
                        
                        string msg = server.Receive();
                       
                        
                        if (msg == "Logoff")
                        {
                            break; //Sensordata: 0.10PPM
                        }
                        if (msg != "CANTREAD")
                        {
                            // Console.WriteLine("Clients Sended: ");
                            // Console.WriteLine(msg);

                            if (msg.Substring(0, 11) == "Sensordata:")
                            {
                                string data = msg.Substring(11, 4);
                                Console.WriteLine(data);
                                Sensordata s = new Sensordata(Convert.ToDouble(data), data.Substring(17));
                            }
                        }
                        
                    }
                }
            }

            if (ServerStarted)
            {
                ServerStarted = false;
                Clientconnected = false;
                server.StopServer();
            }
        }
    }
}
