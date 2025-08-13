using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class CatalogosAD : ICatalogosAD
    {
        private readonly AppDbContext _context;

        public CatalogosAD(AppDbContext context)
        {
            _context = context;
        }


        //Catalogos de contacto

        public List<Catalogo_tipo_contacto> ObtenerCatalogoContacto()
        {
            return _context.Catalogo_tipo_contacto.ToList();
        }

        public List<Catalogo_tipo_direccion> ObtenerCatalogoDireccion()
        {
            return _context.Catalogo_tipo_direccion.ToList();
        }


        //Catalogos de puestos
        public List<Catalogo_perfil_puesto> ObtenerCatalogoPerfilPuesto()
        {
            return _context.Catalogo_perfil_puesto.ToList();
        }

        public Catalogo_perfil_puesto ObtenerCatalogoPuestosXId(int idPuesto)
        {
            return _context.Catalogo_perfil_puesto.Find(idPuesto);
        }


        //Catalogos de estado de aprobacion y nomina
        public Catalogo_estado_aprobacion ObtenerCatalogoEstadoAprobacionXId(int idEstado)
        {
            return _context.Catalogo_estado_aprobacion.Find(idEstado);
        }
        public List<Catalogo_estado_aprobacion> ObtenerCatalogoEstadoAprobacion()
        {
            return _context.Catalogo_estado_aprobacion.ToList();
        }

        public Periodo_nomina ObtenerPeriodoNominaXId(int idPeriodo)
        {
            return _context.Periodo_nomina.Find(idPeriodo);
        }

        public List<Periodo_nomina> ObtenerCatalogoPeriodoNomina()
        {
            return _context.Periodo_nomina.Where(p => p.estado_periodo == "Abierto").ToList();
        }

        public List<Catalogo_estado_orden> ObtenerCatalogoEstadoOrden()
        {
            return _context.Catalogo_estado_orden.ToList();
        }

        public Catalogo_estado_orden ObtenerCatalogoEstadoOrdenXId(int idEstadoOrden)
        {
            return _context.Catalogo_estado_orden.Find(idEstadoOrden);
        }


        //Catalogos de bonificaciones

        public List<Catalogo_bonificaciones> ObtenerCatalogoBonificacion()
        {
            return _context.Catalogo_Bonificaciones.ToList();
        }

        public Catalogo_bonificaciones ObtenerCatalogoBonificacionXId(int idBonificacion)
        {
            return _context.Catalogo_Bonificaciones.Find(idBonificacion);
        }


        //Catalogos de ubicaciones
        public List<Catalogo_provincia> ObtenerCatalogo_Provincias()
        {
            return _context.Catalogo_provincia.ToList();
        }
        public List<Catalogo_canton> ObtenerCatalogo_Cantones(int idProvincia)
        {
            return _context.Catalogo_canton.Where(c => c.catalogo_provincia_id_provincia == idProvincia).ToList();
        }
        public List<Catalogo_distrito> ObtenerCatalogo_Distritos(int idCanton)
        {
            return _context.Catalogo_distrito.Where(d => d.catalogo_canton_id_canton == idCanton).ToList();
        }

        public List<Catalogo_perfil_puesto> ObtenerPuestosPorIds(List<int> ids)
        {
            return _context.Catalogo_perfil_puesto
                .Where(p => ids.Contains(p.id_perfil_puesto))
                .ToList();
        }

    }
}
