namespace Entidades
{
    public class Bonificaciones
    {
        public int id_bonificaciones {  get; set; }
        public DateTime fecha_asignacion {  get; set; }
        public decimal monto_calculado { get; set; }
        public int colaborador_id_colaborador { get; set; }
        public int catalogo_estado_aprobacion_id_estado_aprobacion { get; set; }
        public int catalogo_bonificacion_id_catalogo_bonificacion { get; set; }
        public int periodo_nomina_id_periodo_nomina { get; set; }
        public Catalogo_bonificaciones CatalogoBonificaciones { get; set; }
        public Colaborador Colaborador { get; set; }
        public Periodo_nomina Periodo_nomina { get; set; }
        public Catalogo_estado_aprobacion Catalogo_estado_aprobacion { get; set; }
    }
}
