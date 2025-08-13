using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class BonificacionesAD : IBonificacionesAD
    {
        private readonly AppDbContext _ctx;
        public BonificacionesAD(AppDbContext context)
        {
            _ctx = context;
        }

        public void InsertarBonificacion(Bonificaciones b)
        {
            _ctx.Bonificaciones.Add(b);
            _ctx.SaveChanges();
        }

        public List<Bonificaciones> ObtenerBonificacionesPorEstado(int estadoAprobacionId)
        {
            return _ctx.Bonificaciones
                .Where(b => b.catalogo_estado_aprobacion_id_estado_aprobacion == estadoAprobacionId)
                .Include(b => b.Colaborador)
                    .ThenInclude(c => c.Persona)
                .Include(b => b.Colaborador)
                    .ThenInclude(b => b.Catalogo_perfil_puesto)
                .Include(b => b.CatalogoBonificaciones)
                .Include(b => b.Periodo_nomina)
                .Include(b => b.Catalogo_estado_aprobacion)

                .ToList();
        }


        public void ActualizarEstadoBonificaciones(List<int> idsBonificaciones,int nuevoEstadoAprobacion)
        {
            var list = _ctx.Bonificaciones
                .Where(b => idsBonificaciones.Contains(b.id_bonificaciones))
                .ToList();

            foreach (var b in list)
            {
                b.catalogo_estado_aprobacion_id_estado_aprobacion = nuevoEstadoAprobacion;
            }

            _ctx.SaveChanges();
        }

        public void ActualizarPeriodoBonificaciones(List<int> idsBonificaciones, int nuevoPeriodoNomina)
        {
            var list = _ctx.Bonificaciones
                .Where(b => idsBonificaciones.Contains(b.id_bonificaciones))
                .ToList();

            foreach (var b in list)
            {
                b.periodo_nomina_id_periodo_nomina = nuevoPeriodoNomina;
            }

            _ctx.SaveChanges();
        }






    }
}
