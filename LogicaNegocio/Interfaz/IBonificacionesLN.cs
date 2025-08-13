using Entidades;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Interfaz
{
    public interface IBonificacionesLN
    {
        void ActualizarEstadoBonificaciones(List<int> idsBonificaciones, int nuevoEstadoAprobacion);
        void ActualizarPeriodoBonificaciones(List<int> idsBonificaciones, int nuevoPeriodoNomina);
        List<PreviewBonificacion> ObtenerBonificacionesParaProcesar(int estadoId);
        List<PreviewBonificacion> ObtenerBonificacionesPreview();
        void GuardarBonificaciones(List<PreviewBonificacion> items, int idPeriodoNomina, int idEstadoAprobacion);
    }
}
