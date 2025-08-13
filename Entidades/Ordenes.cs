
namespace Entidades
{
    public class Ordenes
    {
        public int id_orden { get; set; }
        //public DateTime fecha_creacion { get; set; } 
        public string contrato_cliente { get; set; }
        public string tipo_trabajo { get; set; } 
        public DateTime fecha_visita { get; set; }
        public string descripcion_trabajo { get; set; }
        public int colaborador_id_colaborador { get; set; }
        public int periodo_nomina_id_periodo_nomina { get; set; }
        public int estado_orden_id_estado_orden { get; set; }
        public int catalogo_estado_aprobacion_id_estado_aprobacion { get; set; }
        public Catalogo_estado_aprobacion Catalogo_estado_aprobacion { get; set; }
        public Catalogo_estado_orden Estado_orden { get; set; }
        public Colaborador Colaborador { get; set; }
        public Periodo_nomina Periodo_nomina { get; set; }
    }
}
