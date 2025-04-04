using Microsoft.AspNetCore.Mvc;

namespace ControleEstacionamento.Controllers
{
    public class PrecosController : Controller
    {
        public IActionResult Precos()
        {
            return View();
        }
    }
}
