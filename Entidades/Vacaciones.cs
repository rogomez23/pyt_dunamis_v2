using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Vacaciones
    {
        public int id_vacaciones { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public DateTime fecha_inicio_vacaciones { get; set; }
        public DateTime fecha_fin_vacaciones { get; set; }

        [Column(TypeName = "decimal(2,1)")]
        [Range(0.5, 365, ErrorMessage = "Debe solicitar al menos medio día de vacaciones.")]
        public decimal cantidad_dias_solicitados { get; set; }
        public int colaborador_id_colaborador { get; set; }
        public int periodo_nomina_idperiodo_nomina { get; set; }
        public int catalogo_estado_aprobacion_id_estado_aprobacion { get; set; }
        public Colaborador Colaborador { get; set; }
        public Catalogo_estado_aprobacion Catalogo_estado_aprobacion { get; set; }
        public Periodo_nomina Periodo_nomina { get; set; }


    }
}
