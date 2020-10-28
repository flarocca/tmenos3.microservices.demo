using Microsoft.AspNetCore.Mvc;

namespace TMenos3.Microservices.Demo.Microservices.Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
