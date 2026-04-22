namespace ControleDeValidade.Models.Historico
{
    public class HistoricoMaterial
    {
        public int HistoricoMaterialId { get; set; }
        public int AlmoxarifadoId { get; set; }
        public Almoxarifado Almoxarifado { get; set; } 
        public int MaterialId { get; set; }
        public Material Material { get; set; }
        public DateTime DataTransferencia { get; set; }
        public string Setor { get; set; }

    }
}
