using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TooDue.Models;

namespace TooDue.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public MainController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
