namespace Entidades
{
    public class Catalogo_bonificaciones
    {
        public int id_catalogo_bonificacion { get; set; }
        public string descripcion_bonificacion { get; set; }
        public string tipo_bonificacion { get; set; }
        public decimal monto_fijo { get; set; }
        public decimal porcentaje { get; set; }
        public int rango_min {  get; set; }
        public int rango_max { get; set; }
        public int inactivo { get; set; }
        public List<Bonificacion_puesto> Bonificacion_puestos { get; set; } = new List<Bonificacion_puesto> { };
        public List<Bonificaciones> Bonificaciones { get; set; } = new List<Bonificaciones>();

    }
}
