using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanches.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId  { get; set; }

        [Required (ErrorMessage ="Informe o nome da categoria")]
        [StringLength(100, ErrorMessage ="O tamanho maximo e 100 caracteres")]
        [Display(Name ="Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [StringLength(100, ErrorMessage = "O tamanho maximo e 100 caracteres")]
        [Display(Name = "Descricao")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }
    }
}
