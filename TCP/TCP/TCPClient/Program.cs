using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace TCPClient
{
    class Program
    {
        static string adres;
        static int poort;
        static TcpClient client;
        static NetworkStream stream;
        static void Main(string[] args)
        {
            Console.WriteLine("Voer een IP adres in voor de Client");
            string input = Console.ReadLine();
            adres = input;
            Console.WriteLine("Voer een poortnummer in");
            string inputport = Console.ReadLine();
            poort = Convert.ToInt32(inputport);
            HandleClient();

        }
        static void HandleClient()
        {
            client = new TcpClient();
            try
            {
                client.Connect(adres, poort);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (client.Connected == true)
            {
                stream = client.GetStream();
                bool Serverturn = false;
                while (true)
                {
                   
                    if (stream.DataAvailable == false && Serverturn == false)
                    {
                        if (stream.CanWrite)
                        {
                            Console.WriteLine("Typ message to send");
                            string message = Console.ReadLine();
                            Serverturn = true;
                            byte[] msg = Encoding.Default.GetBytes(message);
                            if (msg.Length > 0)
                            {
                                stream.Write(msg, 0, msg.Length);
                               
                            }
                        }
                    }
                    else if (stream.DataAvailable && stream.CanRead)
                    {
                        byte[] message = new byte[1024];
                        int count = stream.Read(message, 0, 1024);
                        if (count > 0)
                        {
                            string msg = Encoding.Default.GetString(message);
                            Console.WriteLine("Server Sended: " + msg);
                            stream.Flush();
                            Serverturn = false;
                        }
                    }
                }
            }
        }
    }
}
