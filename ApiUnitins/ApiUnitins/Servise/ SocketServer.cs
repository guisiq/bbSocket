using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ApiUnitins.Model;


namespace ApiUnitins.Servise{

public class SocketServer
{


    private Socket listener;
    private Dictionary<string, Socket> pixControler = new Dictionary<string, Socket>();
    private List<Thread> ClientsTreads = new List<Thread>();

    public SocketServer()
    {
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Any, 1234));
        listener.Listen(10);
        new Thread(StartListening).Start();
    }

    public void StartListening()
    {
        while (true)
        {
            Socket handler = listener.Accept();
            var thread = new Thread(HandleClient(handler));
            thread.Start();
            ClientsTreads.Add(thread);
        }
    }

    private ThreadStart HandleClient(Socket handler)
    {
        return () =>
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = handler.Receive(buffer);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    Console.WriteLine("Received message: " + message);

                    if (message.StartsWith("pixChave:"))
                    {
                        string pixChave = message.Substring(9);
                        pixControler[pixChave] = handler;
                    }
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: " + ex.Message);
            }
            finally
            {
                handler.Close();
            }
        };
    }

    public void SendPaymentConfirmation(string pixKey,Pix? pix = null)
    {
        if (pixControler.ContainsKey(pixKey))
        {   
            string pixformatado;
            if(pix == null){
                pixformatado = "sem dados";
            }else{
                pixformatado =     "\tChave       : " + pix.chave + "\n" +
                                   "\tValor       : " + pix.valor + "\n" +
                                   "\tPagador     : " + pix.pagador.nome + "\n" +
                                   "\tCPF         : " + pix.pagador.cpf + "\n" +
                                   "\tHorario     : " + pix.horario + "\n" +
                                   "\tInfoPagador : " + pix.infoPagador + "\n" +
                                   "\tValor       : " + pix.componentesValor.original.valor + "\n";
            } 

            byte[] msg = Encoding.ASCII.GetBytes("Payment confirmed\n" + pixformatado);
            pixControler[pixKey].Send(msg);
        }
    }
}
}