using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaz
{
    public interface IHorasExtraAD
    {
        List<Horas_extra> ListaHorasExtraPorColaborador(int idColaborador);
        List<Horas_extra> ListaHorasExtraPorIdEstadoAprobacion(int idEstado);
        string SolicitarHorasExtra(Horas_extra he);
        void EditarHorasExtra(Horas_extra he);
        void EliminarHorasExtra(int id);
        List<Horas_extra> ObtenerHorasExtraProcesadas(int idColaborador);
        void ActualizarEstadosHorasExtra(List<int> ids, int nuevoEstado);
        void ActualizarEstadosYPeriodoHorasExtra(List<int> ids, int nuevoEstado, int idPeriodo);
        bool ExisteSolicitudEnFecha(int idColaborador, DateTime fechaHoraExtras);

    }
}
