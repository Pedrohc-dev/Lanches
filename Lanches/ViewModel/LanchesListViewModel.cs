using Lanches.Models;

namespace Lanches.ViewModel
{
    public class LanchesListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }

        public string CategoriaAtual { get; set; } 
    }
}
