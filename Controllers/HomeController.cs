using Microsoft.AspNetCore.Mvc;


namespace taskApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
    }
}