using ApiUnitins.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUnitins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHoookController : ControllerBase
    {

        [HttpPost]
        public string receberDadosWebHookBB(ReceberPix receberPix)
        {
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