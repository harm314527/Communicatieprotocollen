using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace UDPserver
{
    class Program
    {
        static string adres;
        static int poort;
        static void Main()
        {

            Console.WriteLine("Voer een IP adres in voor de Server");
            string input = Console.ReadLine();
            adres = input;
            Console.WriteLine("Voer een poortnummer in");
            string inputport = Console.ReadLine();
            poort = Convert.ToInt32(inputport);

            int recv;
            byte[] data = new byte[1024];
            IPAddress ip = IPAddress.Parse(adres);
            IPEndPoint iPEndPoint = new IPEndPoint(ip, poort);

            Socket UDPSocket = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram, ProtocolType.Udp);
            UDPSocket.Bind(iPEndPoint);

            Console.WriteLine("Wachten op clients");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);


            recv = UDPSocket.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Message received from client", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            Console.WriteLine("Typ message to send");
            string message = Console.ReadLine();
            data = Encoding.ASCII.GetBytes(message);
            UDPSocket.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                recv = UDPSocket.ReceiveFrom(data, ref Remote);

                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                UDPSocket.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }
    }
}
