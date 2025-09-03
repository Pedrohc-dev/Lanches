using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Lanches.Models
{
    [Table("Lanche")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required (ErrorMessage ="O nome do Lanche deve ser informado")]
        [StringLength(80,MinimumLength =10,ErrorMessage ="O {0} deve ter no minimo {1} ou no maximo {2} caracteres")]
        [Display (Name ="Nome do Lanche")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descricao do Lanche deve ser informado")]
        [Display(Name ="Descricao do Lanche ")]
        [MinLength(20,ErrorMessage ="Descricao deve ter no minimo {1} caractere")]
        [MaxLength(200, ErrorMessage = "Descricao deve ter no minimo {1} caractere")]
        public string DescricaoCurta { get; set; }
       
        [Required(ErrorMessage = "A Descricao Detalhada do Lanche deve ser Informado")]
        [Display(Name = "Descricao Detalhada do Lanche ")]
        [MinLength(20, ErrorMessage = "Descricao deve ter no minimo {1} caractere")]
        [MaxLength(200, ErrorMessage = "Descricao deve ter no minimo {1} caractere")]
        public string DescriaoDetalhada { get; set; }

        [Required(ErrorMessage ="Informa o Preco Do Lanche")]
        [Display(Name ="Preco")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O Preco deve estar entre 1 e 999.99")]
        public decimal Preco {  get; set; }

        [Display(Name ="Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="O {0} deve ter no maximo {1} caractere")]
        public string ImagemURL { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no maximo {1} caractere")]
        public string ImagemThumbnailURL {  get; set; }

        [Display(Name ="Preferido")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name ="Estoque")]
        public bool EmEstoque {  get; set; }

        public int CategoriaId {  get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
