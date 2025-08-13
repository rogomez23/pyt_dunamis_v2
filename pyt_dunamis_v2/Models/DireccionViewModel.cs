using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Models
{
    public class DireccionViewModel
    {
        public int id_direccion { get; set; }
        public string puntos_referencia { get; set; }
        public int catalogo_distrito_id_distrito { get; set; }
        public int catalogo_canton_id_canton { get; set; }
        public int catalogo_provincia_id_provincia { get; set; }
        public int catalogo_tipo_direccion_id_tipo_direccion { get; set; }
        public string persona_id_persona { get; set; }

        // Para llenar el dropdown:
        [ValidateNever]
        public List<SelectListItem> Provincias { get; set; }
        [ValidateNever]
        public List<SelectListItem> Cantones { get; set; }
        [ValidateNever]
        public List<SelectListItem> Distritos { get; set; }
        [ValidateNever]
        public List<SelectListItem> TipoDireccion { get; set; }
    }
}
