using Lanches.Models;
using Microsoft.EntityFrameworkCore;

namespace Lanches.Contex
{
   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) //Dbopitons carrega as informacoes necessarias para a configuracao do Dbcontext
        { 
        }

        public DbSet<Lanche>Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoCompraItem> CarinhoCompraItens { get; set; }
    }
}
