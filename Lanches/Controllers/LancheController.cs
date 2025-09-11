using Lanches.Models;
using Lanches.Repositories.Interfaces;
using Lanches.ViewModel;
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
        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancherepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                //{
                //    lanches = _lancherepository.Lanches
                //        .Where(l => l.Categoria.Nome.Equals("Normal"))
                //        .OrderBy(l => l.Nome);
                //}
                //else
                //{
                //    lanches = _lancherepository.Lanches
                //        .Where(l => l.Categoria.Nome.Equals("Natural"))
                //        .OrderBy(l => l.Nome);
                //}
                 
                lanches = _lancherepository.Lanches
                    .Where(l => l.Categoria.Nome.Equals(categoria))
                    .OrderBy(C => C.Nome);

                categoriaAtual = categoria;
            }
            var lanchesListViewModel = new LanchesListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual,
            };
            return View(lanchesListViewModel);
        }

        public IActionResult Detail(int lancheid)
        { 
            var lanche =_lancherepository.Lanches.FirstOrDefault(l =>l.LancheId == lancheid);

            return View(lanche);
        }

        public ViewResult Search(string searchString) 
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual= string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancherepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else 
            {
                lanches = _lancherepository.Lanches.
                    Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum Lanche Encontrado";
            }

            return View("~/View/Lanche/List.cshtml", new LanchesListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual

            });
        }


    }
}
