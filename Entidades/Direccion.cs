namespace Entidades
{
    public class Direccion
    {
        public int id_direccion { get; set; }
        public string puntos_referencia { get; set; }
        public int catalogo_distrito_id_distrito { get; set; }
        public int catalogo_canton_id_canton { get; set; }
        public int catalogo_provincia_id_provincia { get; set; }
        public int catalogo_tipo_direccion_id_tipo_direccion { get; set; }
        public string persona_id_persona { get; set; }
        public Persona Persona { get; set; }
        public Catalogo_provincia Catalogo_provincia { get; set; }
        public Catalogo_canton Catalogo_canton { get; set; }
        public Catalogo_distrito Catalogo_distrito { get; set; }
        public Catalogo_tipo_direccion Catalogo_tipo_direccion { get; set; }

    }
}
