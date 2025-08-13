using Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Models
{
    public class BonificacionPuestoViewModel
    {
        public int SelectedPuesto { get; set; }
        public int SelectedBonificacion { get; set; }

        // Usamos SelectListItem para los dropdowns
        public List<SelectListItem> Puestos { get; set; }
        public List<SelectListItem> Bonificaciones { get; set; }

        // Sólo los puestos que ya tienen bonificaciones
        public IEnumerable<Catalogo_perfil_puesto> PuestosConBon { get; set; }
    }
}
