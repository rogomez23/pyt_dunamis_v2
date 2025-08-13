using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Horas_extra
    {
        public int id_horas_extra { get; set; }
        public DateTime fecha_solicitud { get; set; } = DateTime.Now;

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime fecha_hora_extras { get; set; }

        [Column(TypeName = "decimal(2,1)")]
        [Range(0.5, 365, ErrorMessage = "Debe solicitar al menos media hora extra.")]
        public decimal cantidad_extras { get; set; }
        public decimal tarifa_por_hora { get; set; }
        public decimal monto_extras { get; set; }
        public int colaborador_id_colaborador { get; set; }
        public int periodo_nomina_idperiodo_nomina { get; set; }
        public int catalogo_estado_aprobacion_id_estado_aprobacion { get; set; }
        public Colaborador Colaborador { get; set; }
        public Catalogo_estado_aprobacion Catalogo_estado_aprobacion { get; set; }
        public Periodo_nomina Periodo_nomina { get; set; }
    }
}
