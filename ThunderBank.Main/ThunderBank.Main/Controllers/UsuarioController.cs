using Microsoft.AspNetCore.Mvc;

namespace ThunderBank.Main.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
