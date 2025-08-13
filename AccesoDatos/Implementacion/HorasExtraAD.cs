using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class HorasExtraAD : IHorasExtraAD
    {
        private readonly AppDbContext _context;
        public HorasExtraAD(AppDbContext context)
        {
            _context = context;
        }

        public List<Horas_extra> ListaHorasExtraPorColaborador(int idColaborador)
        {
            return _context.Horas_extra
                .Where(he => he.colaborador_id_colaborador == idColaborador)
                .Include(he => he.Catalogo_estado_aprobacion)
                .ToList();
        }

        public List<Horas_extra> ListaHorasExtraPorIdEstadoAprobacion(int idEstado)
        {
            return _context.Horas_extra
                .Where(he => he.catalogo_estado_aprobacion_id_estado_aprobacion == idEstado)
                .Include(he => he.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(he => he.Catalogo_estado_aprobacion)
                .Include(he => he.Periodo_nomina)
                .ToList();
        }
        public string SolicitarHorasExtra(Horas_extra he)
        {
            _context.Horas_extra.Add(he);
            _context.SaveChanges();

            return null;
        }
        public void EditarHorasExtra(Horas_extra he)
        {
            _context.Horas_extra.Update(he);
            _context.SaveChanges();
        }
        public void EliminarHorasExtra(int id)
        {
            var he = _context.Horas_extra.Find(id);
            if (he != null)
            {
                _context.Horas_extra.Remove(he);
                _context.SaveChanges();
            }
        }
        public List<Horas_extra> ObtenerHorasExtraProcesadas(int idColaborador)
        {
            return _context.Horas_extra
                .Where(he => he.colaborador_id_colaborador == idColaborador &&
                             he.catalogo_estado_aprobacion_id_estado_aprobacion != 5)
                .ToList();
        }

        public void ActualizarEstadosHorasExtra(List<int> ids, int nuevoEstado)
        {
            var extras = _context.Horas_extra.Where(v => ids.Contains(v.id_horas_extra)).ToList();

            foreach (var ext in extras)
            {
                ext.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
            }

            _context.SaveChanges();
        }

        public void ActualizarEstadosYPeriodoHorasExtra(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            var extras = _context.Horas_extra.Where(v => ids.Contains(v.id_horas_extra)).ToList();

            foreach (var ext in extras)
            {
                ext.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstado;
                ext.periodo_nomina_idperiodo_nomina = idPeriodo;
            }

            _context.SaveChanges();
        }


        public bool ExisteSolicitudEnFecha(int idColaborador, DateTime fecha)
        {
            return _context.Horas_extra
                .Any(h => h.colaborador_id_colaborador == idColaborador &&
                          h.fecha_hora_extras.Date == fecha.Date && h.catalogo_estado_aprobacion_id_estado_aprobacion != 5);
        }



    }
}
