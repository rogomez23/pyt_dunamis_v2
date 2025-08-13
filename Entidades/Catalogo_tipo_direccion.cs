namespace Entidades
{
    public class Catalogo_tipo_direccion
    {
        public int id_tipo_direccion { get; set; }
        public string descripcion_tipo_direccion { get; set; }
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();

    }
}
