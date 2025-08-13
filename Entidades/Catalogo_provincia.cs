namespace Entidades
{
    public class Catalogo_provincia
    {
        public int id_provincia { get; set; }
        public string descripcion_provincia { get; set; }
        public List<Catalogo_canton> Catalogo_cantones { get; set; } = new List<Catalogo_canton>();
        public List<Catalogo_distrito> Catalogo_distritos { get; set; } = new List<Catalogo_distrito>();
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();

    }
}
