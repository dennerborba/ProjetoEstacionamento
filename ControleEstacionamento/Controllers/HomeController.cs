using System.Diagnostics;
using ControleEstacionamento.Models;
using ControleEstacionamento.Models.Tickets;
using ControleEstacionamento.Utils.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstacionamento.Controllers
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
            var model = new TicketsModel();
            model.Tickets = new List<TicketModel>();
            var tickets = new Ticket().GetAll();

            model.Tickets = tickets.Select(ticketEntidade => new TicketModel(ticketEntidade)).ToList();

            return View(model);
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
