using Entidades;
using Entidades.DTO;


namespace AccesoDatos.Interfaz
{
    public interface IOrdenesAD
    {
        List<Ordenes> ObtenerTodasOrdenes();
        List<Ordenes> ObtenerOrdenesPorColaborador(int idColaborador);
        List<Ordenes> ObtenerOrdenesPorEstado(int estadoId);
        string CrearOrden(Ordenes orden);
        void ActualizarOrden(Ordenes orden);
        void EliminarOrden(int id);
        List<Ordenes> ObtenerOrdenesCompletadas(int idColaborador);
        List<Ordenes> ObtenerOrdenesPorPeriodoNomina(int idPeriodoNomina);
        bool ExisteOrdenClienteMismaFecha(string contrato, DateTime fechaVisita);
        void ActualizarPeriodoOrdenes(List<int> ids, int idPeriodo);
        Ordenes ObtenerOrdenPorIdOrden(int idOrden);
        List<Ordenes> ListaOrdenesPorIdEstadoAprobacion(int idEstado);
        void ActualizarEstadosAprobacionOrdenes(List<int> ids, int nuevoEstado);
        void ActualizarEstadosAprobacionYPeriodoOrdenes(List<int> ids, int nuevoEstado, int idPeriodo);
        List<OrdenesTecnicoResumenDTO> ObtenerResumenOrdenesCompletadasPorPeriodo(int estadoOrdenId,int periodoNominaId);
        int ContarOrdenesCompletadasAprobadas(int idColaborador);

    }
}
