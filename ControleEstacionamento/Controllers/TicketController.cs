using ControleEstacionamento.Models.Tickets;
using ControleEstacionamento.Utils.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ControleEstacionamento.Controllers
{
    public class TicketController : Controller
    {
        
        [HttpPost]
        public IActionResult MarcarEntrada(string placa)
        {
            new Ticket().MarcarEntrada(placa);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult MarcarSaida(string placa)
        {
            new Ticket().MarcarSaida(placa);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult FormularioEntrada()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FormularioSaida()
        {
            return View();
        }

    }
}
