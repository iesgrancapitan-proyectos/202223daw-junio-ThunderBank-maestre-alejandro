using Microsoft.AspNetCore.Mvc;

namespace ThunderBank.Main.Controllers
{
    public class TarjetaController : Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
    }
}
