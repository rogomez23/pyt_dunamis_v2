using Entidades;

namespace pyt_dunamis_v2.Models
{
    public class PanelHorasExtraViewModel
    {
        public Colaborador Colaborador { get; set; }
        public List<Horas_extra> HistorialHorasExtra { get; set; }
        public Horas_extra Horas_Extra { get; set; }
    }
}
