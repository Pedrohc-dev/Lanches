using ControleDeValidade.Context;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeValidade.Controllers
{
    public class HistoricoMaterialController : Controller
    {
        private readonly AppDbContext _context;

        public HistoricoMaterialController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var historicoMateriais = _context.HistoricoMateriais.ToList();
            return View(historicoMateriais);
        }

    }
}
