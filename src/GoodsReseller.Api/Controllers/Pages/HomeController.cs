using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers.Pages
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