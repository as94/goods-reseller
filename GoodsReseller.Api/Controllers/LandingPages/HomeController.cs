using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers.LandingPages
{
    [Controller]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}