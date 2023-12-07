using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketClient
{
    public class SocketClient
    {
        private Socket client;
        private List<String> pixKey;

        public SocketClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void ConnectToServer()
        {
            client.Connect(new IPEndPoint(IPAddress.Loopback, 1234));
            new Thread(ListenForMessages).Start();
            new Thread(WriteMessage).Start();
        }

        public void ListenForMessages()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                string message = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                Console.WriteLine("Payment confirmed");
            }
        }
        public void WriteMessage()
        {
            while (true)
            {
                string input = Console.ReadLine();
                byte[] bytes = Encoding.ASCII.GetBytes(input);
                client.Send(bytes);
            }
        }
    }
}