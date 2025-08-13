namespace Entidades
{
    public class Periodo_nomina
    {
        public int id_periodo_nomina { get; set; }
        public string nombre_periodo { get; set; }
        public DateTime fecha_inicio_periodo { get; set; }
        public DateTime fecha_fin_periodo { get; set; }
        public DateTime fecha_pago { get; set; }
        public string estado_periodo { get; set; } // Valores posibles: Pendiente, Aprobado, Pagado
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public List<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
        public List<Horas_extra> Horas_extras { get; set; } = new List<Horas_extra>();
        public List<Ordenes> Ordenes { get; set; } = new List<Ordenes>();
        public List<Bonificaciones> Bonificaciones { get; set;} = new List<Bonificaciones>();
    }
}
