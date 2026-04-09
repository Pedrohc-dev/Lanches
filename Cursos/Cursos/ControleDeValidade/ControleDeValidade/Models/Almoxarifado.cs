using System.ComponentModel.DataAnnotations;

namespace ControleDeValidade.Models
{
    public class Almoxarifado
    {
        public int AlmoxarifadoId { get; set; }

        [Required]
        public int NF { get; set; }

        [Required]
        public string Material { get; set; }
        
        [Required]
        public string CodigoMaterial{ get; set; }
        
        public string Setor { get; set; }

        [Required]
        public DateTime DataDeEntrada { get; set; }

        [Required]
        public DateTime DataDeValidade { get; set; }
    }
}
