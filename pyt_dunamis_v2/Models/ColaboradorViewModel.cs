using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Models
{
    public class ColaboradorViewModel
    {
        public int id_colaborador { get; set; }
        public DateTime fecha_ingreso { get; set; }        
        public string persona_id_persona { get; set; }
        public bool inactivo { get; set; }
        public int catalogo_perfil_puesto_id_perfil_puesto { get; set; }
        // Para llenar el dropdown:
        [ValidateNever]
        public List<SelectListItem> PerfilesPuestos { get; set; }

    }
}
