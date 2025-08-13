using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface IVacacionesAD
    {
        List<Vacaciones> ListaVacacionesPorColaborador(int idColaborador);
        List<Vacaciones> ListaVacacionesPorIdEstadoAprobacion(int idEstado);
        string SolicitarVacaciones(Vacaciones v);
        void EditarVacaciones(Vacaciones v);
        void EliminarVacaciones(int id);
        List<Vacaciones> ObtenerVacacionesProcesadas(int idColaborador);
        void ActualizarEstadosVacaciones(List<int> ids, int nuevoEstado);
        void ActualizarEstadosYPeriodoVacaciones(List<int> ids, int nuevoEstado, int idPeriodo);
        bool ExisteSolicitudEnFecha(int idColaborador, DateTime fechaInicio, DateTime fechaFin);

    }
}
