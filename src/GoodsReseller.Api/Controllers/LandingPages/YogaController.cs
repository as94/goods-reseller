using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers.LandingPages
{
    [Controller]
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class YogaController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}