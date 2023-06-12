﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThunderBank.Models;

namespace ThunderBank.Main.Controllers
{
    public class ResponsableController : Controller
    {
        [Authorize]
        public IActionResult Crear(RegistroViewModel modelo)
        {
            return View(modelo);
        }

    }
}
