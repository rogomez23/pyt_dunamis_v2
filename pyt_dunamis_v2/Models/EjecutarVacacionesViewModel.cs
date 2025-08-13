using Entidades;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Models
{
    public class EjecutarVacacionesViewModel
    {
        public List<Vacaciones> Vacaciones { get; set; }
        public List<SelectListItem> PeriodosNomina { get; set; }
        public List<int> IdsSeleccionados { get; set; }
    }
}
