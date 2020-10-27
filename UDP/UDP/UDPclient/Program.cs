using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace UDPclient
{
    class Program
    {
        static string adres;
        static int poort;
        static string cmdin, stringData;
        public static void Main()
        {
            Console.WriteLine("Voer een IP adres in voor de Server");
            string input = Console.ReadLine();
            adres = input;
            Console.WriteLine("Voer een poortnummer in");
            string inputport = Console.ReadLine();
            poort = Convert.ToInt32(inputport);


            byte[] data = new byte[1024];
           
            IPEndPoint iPEndPoint = new IPEndPoint(
                            IPAddress.Parse(adres), poort);

            Socket server = new Socket(AddressFamily.InterNetwork,
                           SocketType.Dgram, ProtocolType.Udp);


            string welcome = "Hello, are you there?";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, iPEndPoint);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Message received from {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            while (true)
            {
                     input = Console.ReadLine();
                        if (cmdin == "exit")
                            break;
            
                /*server.SendTo(Encoding.ASCII.GetBytes(cmdin), Remote);
                data = new byte[1024];*/
                recv = server.ReceiveFrom(data, ref Remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            Console.WriteLine("Stopping client");
            server.Close();
        }
    }
}

