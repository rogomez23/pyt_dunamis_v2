namespace Entidades
{
    public class Catalogo_tipo_contacto
    {
        public int id_catalogo_tipo_contacto { get; set; }
        public string descripcion_tipo_contacto { get; set; }
        public List<Email> Emails { get; set; } = new List<Email>();
        public List<Telefono> Telefonos { get; set; } = new List<Telefono>();

    }
}
