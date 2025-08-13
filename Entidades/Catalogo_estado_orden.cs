using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Catalogo_estado_orden
    {
        public int id_estado_orden { get; set; }
        public string descripcion_estado { get; set; }
        public List<Ordenes> Ordenes { get; set; } = new List<Ordenes>();
    }
}
