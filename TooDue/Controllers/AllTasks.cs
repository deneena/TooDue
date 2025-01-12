using Microsoft.AspNetCore.Mvc;

namespace TooDue.Controllers
{
    public class AllTasks : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
