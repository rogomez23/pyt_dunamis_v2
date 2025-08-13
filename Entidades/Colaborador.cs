namespace Entidades
{
    public class Colaborador
    {
        public int id_colaborador { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public bool inactivo { get; set; } = false;
        public int catalogo_perfil_puesto_id_perfil_puesto { get; set; }
        public string persona_id_persona { get; set; }
        public Persona Persona { get; set; }
        public Catalogo_perfil_puesto Catalogo_perfil_puesto { get; set; }
        public List<Vacaciones> Vacaciones { get; set; } = new List<Vacaciones>();
        public List<Horas_extra> Horas_extra { get; set; } = new List<Horas_extra>();
        public List<Ordenes> Ordenes { get; set; } = new List<Ordenes>();
        public List<Bonificaciones> Bonificaciones { get;set; } = new List<Bonificaciones>();

    }
}
