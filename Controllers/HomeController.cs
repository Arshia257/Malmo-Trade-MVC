using Microsoft.AspNetCore.Mvc;

//Home Controller

namespace MalmöTradera.web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}