using ControleDeValidade.Context;
using ControleDeValidade.Models;
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

        public IActionResult Details(int id)
        {
            var almoxarifado = _context.Almoxarifados.FirstOrDefault(a => a.AlmoxarifadoId == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }

            return View(almoxarifado);
        }

        public IActionResult Create()
        {
            return View("_Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Almoxarifado almoxarifado)
        {
            if (ModelState.IsValid)
            {
                _context.Almoxarifados.Add(almoxarifado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("_Create", almoxarifado);
        }

        public IActionResult Edit()
        {
            return PartialView("_Edit");
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id)
        {
            var almoxarifado = _context.Almoxarifados.FirstOrDefault(a => a.AlmoxarifadoId == id);
            if (almoxarifado == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", almoxarifado);
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
