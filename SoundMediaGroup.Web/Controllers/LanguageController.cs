using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Common;

namespace WebApplication1.Controllers
{
    public class LanguageController : ApiController
    {
        [HttpGet]
        public ActionResult LanguageSwap(string language)
        {
            HttpContext.Session.SetString("Language", language);
            return Ok();
        }

    }
}
