using Entidades;
using Entidades.DTO;

namespace LogicaNegocio.Interfaz
{
    public interface IOrdenesLN
    {
        public List<Ordenes> ObtenerTodasOrdenes();
        public List<Ordenes> ObtenerOrdenesPorColaborador(int idColaborador);
        public List<Ordenes> ObtenerOrdenesPorEstado(int estadoId);
        public string CrearOrden(Ordenes orden);
        public void ActualizarOrden(Ordenes orden);
        public void EliminarOrden(int id);
        public List<Ordenes> ObtenerOrdenesCompletadas(int idColaborador);
        public List<Ordenes> ObtenerOrdenesPorPeriodoNomina(int idPeriodoNomina);
        public void ActualizarPeriodoOrdenes(List<int> ids, int idPeriodo);
        bool ExisteOrdenClienteMismaFecha(string contrato, DateTime fechaVisita);
        Ordenes ObtenerOrdenPorIdOrden(int idOrden);
        List<Ordenes> ListaOrdenesPorIdEstadoAprobacion(int idEstado);
        void ActualizarEstadosAprobacionOrdenes(List<int> ids, int nuevoEstado);
        void ActualizarEstadosAprobacionYPeriodoOrdenes(List<int> ids, int nuevoEstado, int idPeriodo);
        List<OrdenesTecnicoResumenDTO> ObtenerResumenOrdenesCompletadasPorPeriodo(int estadoOrdenId, int periodoNominaId);
        int ContarOrdenesCompletadasAprobadas(int idColaborador);
    }
}
