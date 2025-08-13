using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Telefono
    {
        public int id_telefono { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números")]
        public string numero { get; set; }
        public int catalogo_tipo_contacto_id_catalogo_tipo_contacto { get; set; }
        public string persona_id_persona { get; set; }
        public Persona Persona { get; set; }
        public Catalogo_tipo_contacto Catalogo_tipo_contacto { get; set; }
    }
}
