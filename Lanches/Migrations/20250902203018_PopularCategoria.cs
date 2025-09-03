using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanches.Migrations
{
    public partial class PopularCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(nome,Descricao)" +
                "VALUES('Normal','Lanche feito com ingrediente normais')");

            migrationBuilder.Sql("INSERT INTO Categorias(nome,Descricao)" +
             "VALUES('Natura','Lanche feito com ingrediente naturais')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DELETE FROM Categorias");
            
        }
    }
}
