using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanches.Models
{
    public class Pedido
    {
        public int Pedidoid {  get; set; }

        [Required(ErrorMessage ="Informe O Nome")]
        [StringLength(200)]
        public string Nome {  get; set; }

        [Required(ErrorMessage ="Informe o Sobrenome")]
        [StringLength(200)]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage ="Informe o Endereço")]
        [StringLength(200)]
        public string Endereco1 { get; set; }

        [Display(Name ="Complemento")]
        [StringLength(200)]
        public string Endereco2 { get; set; }

        [Required(ErrorMessage ="Informe o seu CEP")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength =8)]
        public string Cep {  get; set; }

        [StringLength(10)]
        public string Estado {  get; set; }

        [StringLength(50)]
        public string Cidade { get; set; }

        [Required(ErrorMessage ="Informe o seu Telefone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="Informe o seu E-mail")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage ="Informe um formato valido de E-mail")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName ="decimal(18,2)")]
        [Display(Name ="Total Do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name ="Itens no Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name ="Data do Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString ="{0: dd/mm/yyyy hh:mm",ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data do Envio do Pedido")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/mm/yyyy hh:mm", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm {  get; set; }

        public List<PedidoDetalhe> PedidoItens {  get; set; }
    }
}
