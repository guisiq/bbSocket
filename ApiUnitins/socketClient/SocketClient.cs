using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socketClient
{
    public class SocketClient
    {
        private Socket client;
        private string pixKey;

        public SocketClient(string pixKey)
        {
            this.pixKey = pixKey;
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void ConnectToServer()
        {
            client.Connect(new IPEndPoint(IPAddress.Loopback, 11000));
            byte[] msg = Encoding.ASCII.GetBytes(pixKey);
            client.Send(msg);
        }

        public void ListenForMessages()
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                string message = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (message == "Payment confirmed")
                {
                    Console.WriteLine("Payment confirmed");
                }
            }
        }
    }
}