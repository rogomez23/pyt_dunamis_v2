using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Catalogo_canton
    {
        public int id_canton { get; set; }
        public string descripcion_canton { get; set; }

        [Column("catalogo_provincia_id_provincia")]
        public int catalogo_provincia_id_provincia { get; set; }
        public Catalogo_provincia Catalogo_provincia { get; set; }
        public List<Catalogo_distrito> Catalogo_distritos { get; set; } = new List<Catalogo_distrito>();
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();
    }
}
