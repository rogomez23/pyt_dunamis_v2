using AccesoDatos.Interfaz;
using Entidades;
using Entidades.DTO;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class OrdenesAD : IOrdenesAD
    {
        private readonly AppDbContext _context;
        public OrdenesAD(AppDbContext context)
        {
            _context = context;
        }

        public List<Ordenes> ObtenerTodasOrdenes()
        {
            return _context.Ordenes
                .Include(o => o.Estado_orden)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Catalogo_perfil_puesto)
                .Include(o => o.Periodo_nomina)
                .ToList();

        }

        public List<Ordenes> ObtenerOrdenesPorColaborador(int idColaborador)
        {
            return _context.Ordenes
                .Where(o => o.colaborador_id_colaborador == idColaborador)
                .Include(o => o.Estado_orden)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(o => o.Periodo_nomina)
                .ToList();
        }

        public List<Ordenes> ObtenerOrdenesPorEstado(int estadoId)
        {
            return _context.Ordenes
                .Where(o => o.estado_orden_id_estado_orden == estadoId)
                .Include(o => o.Estado_orden)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Catalogo_perfil_puesto)
                .Include(o => o.Periodo_nomina)
                .ToList();
        }

        public List<Ordenes> ListaOrdenesPorIdEstadoAprobacion(int idEstado)
        {
            return _context.Ordenes
                .Where(o => o.catalogo_estado_aprobacion_id_estado_aprobacion == idEstado &&
                                            o.estado_orden_id_estado_orden == 4)
                .Include(o => o.Estado_orden)
                .Include(c => c.Catalogo_estado_aprobacion)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                    .Include(o => o.Colaborador)                    
                    .ThenInclude(c => c.Catalogo_perfil_puesto)
                    .Include(o => o.Periodo_nomina)
                .ToList();
        }

        public void ActualizarEstadosAprobacionOrdenes(List<int> ids, int nuevoEstado)
        {
            var ordenes = _context.Ordenes.Where(v => ids.Contains(v.id_orden)).ToList();

            foreach (var vac in ordenes)
            {
                vac.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
            }

            _context.SaveChanges();
        }

        public void ActualizarEstadosAprobacionYPeriodoOrdenes(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            var ordenes = _context.Ordenes.Where(v => ids.Contains(v.id_orden)).ToList();

            foreach (var vac in ordenes)
            {
                vac.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
                vac.periodo_nomina_id_periodo_nomina = idPeriodo;
            }

            _context.SaveChanges();
        }


        public Ordenes ObtenerOrdenPorIdOrden(int idOrden)
        {
            return _context.Ordenes
                .Include(o => o.Estado_orden)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(o => o.Periodo_nomina)
                .FirstOrDefault(o => o.id_orden == idOrden);
        }

        public string CrearOrden(Ordenes orden)
        {
            _context.Ordenes.Add(orden);
            _context.SaveChanges();
            return null; // O puedes retornar el ID de la orden creada
        }

        public void ActualizarOrden(Ordenes orden)
        {
            _context.Ordenes.Update(orden);
            _context.SaveChanges();
        }

        public void EliminarOrden(int id)
        {
            var orden = _context.Ordenes.Find(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
                _context.SaveChanges();
            }
        }

        public List<Ordenes> ObtenerOrdenesCompletadas(int idColaborador)
        {
            return _context.Ordenes
                .Where(o => o.colaborador_id_colaborador == idColaborador &&
                             o.estado_orden_id_estado_orden == 4)
                .ToList();

        }

        public List<Ordenes> ObtenerOrdenesPorPeriodoNomina(int idPeriodoNomina)
        {
            return _context.Ordenes
                .Where(o => o.periodo_nomina_id_periodo_nomina == idPeriodoNomina)
                .Include(o => o.Estado_orden)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(o => o.Periodo_nomina)
                .ToList();
        }

        public void ActualizarPeriodoOrdenes(List<int> ids, int idPeriodo)
        {
            var ordenes = _context.Ordenes.Where(v => ids.Contains(v.id_orden)).ToList();

            foreach (var ot in ordenes)
            {
                ot.periodo_nomina_id_periodo_nomina = idPeriodo;
            }
            _context.SaveChanges();
        }

        public bool ExisteOrdenClienteMismaFecha(string contrato, DateTime fechaVisita)
        {
            return _context.Ordenes.Any(o => o.contrato_cliente == contrato &&
                            o.fecha_visita.Date == fechaVisita.Date);


        }


        //Resumen de ordenes completadas por periodo y estado de aprobacion

        public List<OrdenesTecnicoResumenDTO> ObtenerResumenOrdenesCompletadasPorPeriodo(int estadoOrdenId, int periodoNominaId)
        {
            return _context.Ordenes
                .Where(o =>
                    o.estado_orden_id_estado_orden == estadoOrdenId &&
                    o.periodo_nomina_id_periodo_nomina == periodoNominaId)
                .Include(o => o.Estado_orden)
                .Include(o => o.Catalogo_estado_aprobacion)
                .Include(o => o.Colaborador)
                    .ThenInclude(c => c.Persona)
                .GroupBy(o => o.colaborador_id_colaborador)
                .Select(g => new OrdenesTecnicoResumenDTO
                {
                    ColaboradorId = g.Key,
                    NombreTecnico = g.First().Colaborador.Persona.nombre
                                         + " "
                                         + g.First().Colaborador.Persona.apellido_1,
                    TotalCompletadas = g.Count(),
                    EstadoOrden = g.First().Estado_orden.descripcion_estado,
                    EstadoAprobacion = g.First().Catalogo_estado_aprobacion.descripcion_estado,
                    PeriodoNominaId = periodoNominaId
                })
                .ToList();
        }


        public int ContarOrdenesCompletadasAprobadas(int idColaborador)
        {
            const int estadoCompletado = 4;            // tu código para “completada”
            const int estadoAprobacion = 3;            // p.ej. 4 = “Aprobado por Planilla”
            return _context.Ordenes
                .Count(o =>
                    o.colaborador_id_colaborador == idColaborador &&
                    o.estado_orden_id_estado_orden == estadoCompletado &&
                    o.catalogo_estado_aprobacion_id_estado_aprobacion == estadoAprobacion
                );
        }






    }

}
