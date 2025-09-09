using System.Diagnostics;
using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Lanches.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lanches.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILancheRepository _lancherepository;

        public HomeController(ILancheRepository lancherepository)
        {
            _lancherepository = lancherepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancherepository.LanchesPreferidos
            };
            return View(homeViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
