using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IVacacionesLN
    {
        List<Vacaciones> ListaVacacionesPorColaborador(int idColaborador);
        List<Vacaciones> ListaVacacionesPorIdEstadoAprobacion(int idEstado);
        string SolicitarVacaciones(Vacaciones v);
        void EditarVacaciones(Vacaciones v);
        void EliminarVacaciones(int id);
        List<Vacaciones> ObtenerVacacionesProcesadas(int idColaborador);
        decimal CalcularDiasDisponibles(int idColaborador);
        void ActualizarEstadosVacaciones(List<int> ids, int nuevoEstado);
        void ActualizarEstadosYPeriodoVacaciones(List<int> ids, int nuevoEstado, int idPeriodo);
    }
}
