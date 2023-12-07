using ApiUnitins.Model;
using ApiUnitins.Servise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHoookController : ControllerBase
    {
        
        private readonly SocketServer socketServer;

        public WebHoookController(SocketServer socketServer)
        {
            this.socketServer = socketServer;
        }


        [HttpPost]
        public string receberDadosWebHookBB(ReceberPix receberPix)
        {
            receberPix.pix.ForEach(pix =>
            {
                Console.WriteLine("imprimindo messagem");
                socketServer.SendPaymentConfirmation(pix.chave,pix);
            });
            Console.WriteLine("imprimindo messagem");
            return "API Unitins BB";
        }

        [HttpGet]
        public string teste()
        {
            return "UNITINS";
        }
    }

}
//https://localhost:7291/api/webhook