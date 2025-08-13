namespace Entidades
{
    public class Catalogo_estado_aprobacion
    {
        public int id_estado_aprobacion { get; set; }
        public string descripcion_estado { get; set; }
        public List<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
        public List<Horas_extra> Horas_extra { get; set; } = new List<Horas_extra>();
        public List<Ordenes> Ordenes { get; set; } = new List<Ordenes>();
        public List<Bonificaciones> Bonificaciones { get;set; } = new List<Bonificaciones>();

    }
}
