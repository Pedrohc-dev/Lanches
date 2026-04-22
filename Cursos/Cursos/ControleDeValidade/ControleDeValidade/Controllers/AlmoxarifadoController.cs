using ControleDeValidade.Context;
using ControleDeValidade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeValidade.Controllers
{
    public class AlmoxarifadoController : Controller
    {
        private readonly AppDbContext _context;


        public AlmoxarifadoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string NF, string Material, string CodigoMaterial)
        {
            // Persistir valores nos inputs
            ViewBag.NF = NF;
            ViewBag.Material = Material;    
            ViewBag.CodigoMaterial = CodigoMaterial;

            var query = _context.Almoxarifados
                .AsQueryable()
                .AsQueryable();

            // incluir material para evitar acesso a propriedades nulas
            query = _context.Almoxarifados.Include(a => a.Material).AsQueryable();

            if (!string.IsNullOrWhiteSpace(NF) && int.TryParse(NF, out var nfValue))
            {
                query = query.Where(a => a.NF == nfValue);
            }

            if (!string.IsNullOrWhiteSpace(Material))
            {
                query = query.Where(a => a.Material != null && a.Material.Nome.Contains(Material)); 
            }

            if (!string.IsNullOrWhiteSpace(CodigoMaterial))
            {
                query = query.Where(a => a.Material != null && a.Material.CodigoMaterial.Contains(CodigoMaterial));
            }

            var almoxarifados = query.ToList();
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
            ViewBag.MaterialList = new SelectList(_context.Materiais.ToList(), "MaterialId", "Nome");
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
            ViewBag.MaterialList = new SelectList(_context.Materiais.ToList(), "MaterialId", "Nome", almoxarifado.MaterialId);
            return View("_Create", almoxarifado);
        }

        public IActionResult Edit()
        {
            ViewBag.MaterialList = new SelectList(_context.Materiais.ToList(), "MaterialId", "Nome");
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
            ViewBag.MaterialList = new SelectList(_context.Materiais.ToList(), "MaterialId", "Nome", almoxarifado.MaterialId);
            return PartialView("_Edit", almoxarifado);
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
