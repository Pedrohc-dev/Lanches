using Lanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancherepository;

        public LancheController(ILancheRepository lancherepository)
        {
            _lancherepository = lancherepository;
        }
        public IActionResult List()
        {

            var lanches = _lancherepository.Lanches;

            return View(lanches);
        }

    }
}
