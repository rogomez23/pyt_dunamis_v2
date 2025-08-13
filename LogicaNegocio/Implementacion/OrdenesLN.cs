using AccesoDatos.Interfaz;
using LogicaNegocio.Interfaz;
using Entidades;
using Entidades.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Implementacion
{
    public class OrdenesLN : IOrdenesLN
    {
        private readonly IOrdenesAD _ordenesAD;
        public OrdenesLN(IOrdenesAD ordenesAD)
        {
            _ordenesAD = ordenesAD;
        }

        public List<Ordenes> ObtenerTodasOrdenes()
        {
            return _ordenesAD.ObtenerTodasOrdenes();
        }

        public Ordenes ObtenerOrdenPorIdOrden(int idOrden)
        {
            return _ordenesAD.ObtenerOrdenPorIdOrden(idOrden);
        }


        public List<Ordenes> ObtenerOrdenesPorColaborador(int idColaborador)
        {
            return _ordenesAD.ObtenerOrdenesPorColaborador(idColaborador);
        }
        public List<Ordenes> ObtenerOrdenesPorEstado(int estadoId)
        {
            return _ordenesAD.ObtenerOrdenesPorEstado(estadoId);
        }
        public string CrearOrden(Ordenes orden)
        {
            if (_ordenesAD.ExisteOrdenClienteMismaFecha(orden.contrato_cliente, orden.fecha_visita))
            {
                return "Ya existe una orden para el cliente en la misma fecha.";
            }
            _ordenesAD.CrearOrden(orden);

            return null;
        }
        public void ActualizarOrden(Ordenes orden)
        {
            _ordenesAD.ActualizarOrden(orden);
        }
        public void EliminarOrden(int id)
        {
            _ordenesAD.EliminarOrden(id);
        }
        public List<Ordenes> ObtenerOrdenesCompletadas(int idColaborador)
        {
            return _ordenesAD.ObtenerOrdenesCompletadas(idColaborador);
        }
        public List<Ordenes> ObtenerOrdenesPorPeriodoNomina(int idPeriodoNomina)
        {
            return _ordenesAD.ObtenerOrdenesPorPeriodoNomina(idPeriodoNomina);
        }

        public void ActualizarPeriodoOrdenes(List<int> ids, int idPeriodo)
        {
            _ordenesAD.ActualizarPeriodoOrdenes(ids, idPeriodo);
        }

        public bool ExisteOrdenClienteMismaFecha(string contrato, DateTime fechaVisita)
        {
            return _ordenesAD.ExisteOrdenClienteMismaFecha(contrato, fechaVisita);

        }


        public List<Ordenes> ListaOrdenesPorIdEstadoAprobacion(int idEstado)
        {
            return _ordenesAD.ListaOrdenesPorIdEstadoAprobacion(idEstado);
        }
        public void ActualizarEstadosAprobacionOrdenes(List<int> ids, int nuevoEstado)
        {
            _ordenesAD.ActualizarEstadosAprobacionOrdenes(ids, nuevoEstado);
        }
        public void ActualizarEstadosAprobacionYPeriodoOrdenes(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            _ordenesAD.ActualizarEstadosAprobacionYPeriodoOrdenes(ids, nuevoEstado, idPeriodo);

        }

        public List<OrdenesTecnicoResumenDTO> ObtenerResumenOrdenesCompletadasPorPeriodo(int estadoOrdenId, int periodoNominaId)
        {
            return _ordenesAD.ObtenerResumenOrdenesCompletadasPorPeriodo(estadoOrdenId, periodoNominaId);

        }

        public int ContarOrdenesCompletadasAprobadas(int idColaborador)
        {
            return _ordenesAD.ContarOrdenesCompletadasAprobadas(idColaborador);

        }

    }
}
