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
        private SocketServer socketServer;
        public WebHoookController()
        {
            socketServer = new SocketServer();
        }


        [HttpPost]
        public string receberDadosWebHookBB(ReceberPix receberPix)
        {
            receberPix.pix.ForEach(pix =>
            {
                socketServer.SendPaymentConfirmation(pix.chave);
            });
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