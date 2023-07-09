using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {

        [HttpGet]
        public ActionResult LanguageSwap(string language)
        {
            HttpContext.Session.SetString("Language", language);
            return Ok();
        }

    }
}
