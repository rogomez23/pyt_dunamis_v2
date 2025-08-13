namespace Entidades
{
    public class Catalogo_distrito
    {
        public int id_distrito { get; set; }
        public string descripcion_distrito { get; set; }
        public int catalogo_canton_id_canton { get; set; }
        public int catalogo_provincia_id_provincia { get; set; }
        public Catalogo_canton Catalogo_canton { get; set; }
        public Catalogo_provincia Catalogo_provincia { get; set; }
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();

    }
}
