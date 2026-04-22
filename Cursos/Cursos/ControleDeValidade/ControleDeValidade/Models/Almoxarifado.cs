using System.ComponentModel.DataAnnotations;

namespace ControleDeValidade.Models
{
    public class Almoxarifado
    {
        public int AlmoxarifadoId { get; set; }

        [Required]
        public int NF { get; set; }

        [Required]
        public int MaterialId { get; set; }
        
       public Material Material { get; set; }

        public string Setor { get; set; }

        [Required]
        public DateTime DataDeEntrada { get; set; }

        [Required]
        public DateTime DataDeValidade { get; set; }

        [Required]
        public string Quantidade { get; set; }
    }
}
