using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Produces("application/json")]
    public class Sample : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
