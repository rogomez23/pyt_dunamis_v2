namespace Entidades.DTO
{
    public class PreviewBonificacion
    {
        public int ColaboradorId { get; set; }
        public string NombreColaborador { get; set; }
        public string DescripcionPuesto { get; set; }
        public string TipoBonificacion { get; set; }
        public decimal MontoCalculado { get; set; }
        public int CatalogoBonificacionId { get; set; }
        public string DescripcionBonificacion { get; set; }
        public int CantidadOrdenes { get; set; }
        public int PeriodoNominaId { get; set; }
        public string PeriodoNominaDesc { get; set; }
        public string EstadoAprobacionId { get; set; }
        public string EstadoAprobacion { get; set; }
        public int BonificacionId { get; set; }
    }
}
