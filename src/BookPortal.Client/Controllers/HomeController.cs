using Microsoft.AspNet.Mvc;

namespace BookPortal.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
