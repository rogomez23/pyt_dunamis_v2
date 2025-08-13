using Entidades;

namespace pyt_dunamis_v2.Models
{
    public class PanelVacacionesViewModel
    {
        public Colaborador Colaborador { get; set; }
        public List<Vacaciones> HistorialVacaciones { get; set; }
        public Vacaciones Vacaciones { get; set; }
    }
}
