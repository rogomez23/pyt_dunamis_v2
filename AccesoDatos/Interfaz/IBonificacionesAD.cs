using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaz
{
    public interface IBonificacionesAD
    {
        void InsertarBonificacion(Bonificaciones b);
        void ActualizarEstadoBonificaciones(List<int> idsBonificaciones, int nuevoEstadoAprobacion);
        void ActualizarPeriodoBonificaciones(List<int> idsBonificaciones, int nuevoPeriodoNomina);
        List<Bonificaciones> ObtenerBonificacionesPorEstado(int estadoAprobacionId);
    }
}
