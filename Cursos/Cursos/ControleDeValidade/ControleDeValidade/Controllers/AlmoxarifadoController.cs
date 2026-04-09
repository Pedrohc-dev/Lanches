using ControleDeValidade.Context;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeValidade.Controllers
{
    public class AlmoxarifadoController : Controller
    {
        private readonly AppDbContext _context;


        public AlmoxarifadoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var almoxarifados = _context.Almoxarifados.ToList();
            return View(almoxarifados);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
