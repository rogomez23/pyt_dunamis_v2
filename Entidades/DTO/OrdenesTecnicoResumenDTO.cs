using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class OrdenesTecnicoResumenDTO
    {
        public int ColaboradorId { get; set; }
        public string NombreTecnico { get; set; }
        public int TotalCompletadas { get; set; }
        public string EstadoOrden { get; set; }             
        public string EstadoAprobacion { get; set; }        
        public int PeriodoNominaId { get; set; }

    }
}
