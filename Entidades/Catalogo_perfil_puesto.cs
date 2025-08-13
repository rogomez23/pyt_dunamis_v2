namespace Entidades
{
    public class Catalogo_perfil_puesto
    {
        public int id_perfil_puesto { get; set; }
        public string descripcion_puesto { get; set; }
        public Decimal salario_base { get; set; }
        public bool inactivo { get; set; } = false;
        public List<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();
        public List<Bonificacion_puesto> Bonificacion_puesto { get; set; } = new List<Bonificacion_puesto> { };

    }
}
