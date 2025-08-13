using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class VacacionesAD : IVacacionesAD
    {
        private readonly AppDbContext _context;
        public VacacionesAD(AppDbContext context)
        {
            _context = context;
        }

        public List<Vacaciones> ListaVacacionesPorColaborador(int idColaborador)
        {
            return _context.Vacaciones
                .Where(v => v.colaborador_id_colaborador == idColaborador)
                .Include(v => v.Catalogo_estado_aprobacion)
                .ToList();
        }

        public List<Vacaciones> ListaVacacionesPorIdEstadoAprobacion(int idEstado)
        {
            return _context.Vacaciones
                .Where(v => v.catalogo_estado_aprobacion_id_estado_aprobacion == idEstado)
                .Include(v => v.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(v => v.Catalogo_estado_aprobacion)
                .Include(v => v.Periodo_nomina)
                .ToList();
        }

        public string SolicitarVacaciones(Vacaciones v)
        {
            _context.Vacaciones.Add(v);
            _context.SaveChanges();

            return null;
        }

        public void EditarVacaciones(Vacaciones v)
        {
            _context.Vacaciones.Update(v);
            _context.SaveChanges();
        }


        public void EliminarVacaciones(int id)
        {
            var e = _context.Vacaciones.Find(id);
            if (e != null)
            {
                _context.Vacaciones.Remove(e);
                _context.SaveChanges();
            }
        }
        public List<Vacaciones> ObtenerVacacionesProcesadas(int idColaborador)
        {
            return _context.Vacaciones
                .Where(v => v.colaborador_id_colaborador == idColaborador &&
                            v.catalogo_estado_aprobacion_id_estado_aprobacion != 5)
                .ToList();
        }


        public void ActualizarEstadosVacaciones(List<int> ids, int nuevoEstado)
        {
            var vacaciones = _context.Vacaciones.Where(v => ids.Contains(v.id_vacaciones)).ToList();

            foreach (var vac in vacaciones)
            {
                vac.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
            }

            _context.SaveChanges();
        }

        public void ActualizarEstadosYPeriodoVacaciones(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            var vacaciones = _context.Vacaciones.Where(v => ids.Contains(v.id_vacaciones)).ToList();

            foreach (var vac in vacaciones)
            {
                vac.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
                vac.periodo_nomina_idperiodo_nomina = idPeriodo;
            }

            _context.SaveChanges();
        }


        public bool ExisteSolicitudEnFecha(int idColaborador, DateTime fechaInicio, DateTime fechaFin)
        {
            return _context.Vacaciones.Any(v =>
                v.colaborador_id_colaborador == idColaborador &&
                v.catalogo_estado_aprobacion_id_estado_aprobacion != 5 && // Solo solicitudes pendientes
                (
                    // Rango de fechas
                    (fechaInicio >= v.fecha_inicio_vacaciones && fechaFin <= v.fecha_fin_vacaciones) || 
                    (fechaInicio <= v.fecha_inicio_vacaciones && fechaFin >= v.fecha_fin_vacaciones) || 
                    (fechaInicio <= v.fecha_inicio_vacaciones && fechaFin <= v.fecha_fin_vacaciones) ||  
                    (fechaInicio >= v.fecha_inicio_vacaciones && fechaFin >= v.fecha_fin_vacaciones)
                )
            );
        }

    }
}
