using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ApiUnitins.Servise{

public class SocketServer
{
    public event Action<string> DataReceived;

    private Socket listener;
    private Dictionary<string, Socket> clients = new Dictionary<string, Socket>();

    public SocketServer()
    {
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Any, 11000));
        listener.Listen(10);
    }

    public void StartListening()
    {
        while (true)
        {
            Socket handler = listener.Accept();
            byte[] bytes = new byte[1024];
            int bytesRec = handler.Receive(bytes);
            string pixKey = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            clients[pixKey] = handler;
        }
    }

    public void SendPaymentConfirmation(string pixKey)
    {
        if (clients.ContainsKey(pixKey))
        {
            byte[] msg = Encoding.ASCII.GetBytes("Payment confirmed");
            clients[pixKey].Send(msg);
        }
    }
}
}