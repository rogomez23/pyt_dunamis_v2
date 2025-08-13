namespace Entidades
{
    public class Email
    {
        public int id_email { get; set; }
        public string email { get; set; }
        public int catalogo_tipo_contacto_id_catalogo_tipo_contacto { get; set; }
        public string persona_id_persona { get; set; }
        public Persona Persona { get; set; }
        public Catalogo_tipo_contacto Catalogo_tipo_contacto { get; set; }
    }
}
