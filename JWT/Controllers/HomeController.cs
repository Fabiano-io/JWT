using System.Collections.Generic;
using JWT.Extenssions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace JWT.Controllers
{

    [Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public string Index()
        {
            return "Aplicação iniciada";
        }


        [HttpGet("GerarToken")]
        public ActionResult<IEnumerable<string>> GerarToken()
        {
            var token = JWT.Configuration.JWTConfig.GenerateToken(_appSettings);

            return Ok(new
            {
                token = token
            });            
        }

        [HttpGet("SemValidacao")]
        public string SemValidacao()
        {
            return "Sem Validação";
        }

        [Authorize]
        [HttpGet("ComValidacaoSemRole")]
        public string ComValidacaoSemRole()
        {
            return "Com Validacao sem role";
        }

        [Authorize(Roles = "NivelAdm")]
        [HttpGet("ComValidacaoNivelAdm")]
        public string ComValidacaoNivelAdm()
        {
            return "Com Validacao Nível Adm";
        }

        [Authorize(Roles = "NivelUsr")]
        [HttpGet("ComValidacaoNivelUsr")]
        public string ComValidacaoNivelUsr()
        {
            return "Com Validacao Nível Usr";
        }
    }
}