using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThunderBank.Main.Models;

namespace ThunderBank.Main.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ErrorView()
        {
            ViewBag.ErrorMessage = TempData["Error"] as string;
            return View();
        }
    }
}