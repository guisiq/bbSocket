using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ApiUnitins.Model;


namespace ApiUnitins.Servise{

public class SocketServer
{
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

    public void SendPaymentConfirmation(string pixKey,Pix? pix = null)
    {
        if (clients.ContainsKey(pixKey))
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
            clients[pixKey].Send(msg);
        }
    }
}
}