using Microsoft.AspNetCore.Mvc;

namespace ThunderBank.Main.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
