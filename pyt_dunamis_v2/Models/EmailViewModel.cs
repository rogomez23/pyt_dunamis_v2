using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace pyt_dunamis_v2.Models
{
    public class EmailViewModel
    {
        public int id_email { get; set; }

        public string email { get; set; }

        public int catalogo_tipo_contacto_id_catalogo_tipo_contacto { get; set; }

        public string persona_id_persona { get; set; }

        // Para llenar el dropdown:
        [ValidateNever]
        public List<SelectListItem> TiposContacto { get; set; }
    }
}
