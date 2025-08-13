using AccesoDatos.Implementacion;
using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class CatalogosLN : ICatalogosLN
    {
        private readonly ICatalogosAD _catalogosAD;
        public CatalogosLN(ICatalogosAD catalogosAD)
        {
            _catalogosAD = catalogosAD;
        }
        public List<Catalogo_tipo_contacto> ObtenerCatalogoContacto()
        {
            return _catalogosAD.ObtenerCatalogoContacto();
        }

        //Catalogo de contacto y direccion

        public List<Catalogo_tipo_direccion> ObtenerCatalogoDireccion()
        {
            return _catalogosAD.ObtenerCatalogoDireccion();
        }


        //Catalogos de puestos
        public List<Catalogo_perfil_puesto> ObtenerCatalogoPerfilPuesto()
        {
            return _catalogosAD.ObtenerCatalogoPerfilPuesto();
        }

        public Catalogo_perfil_puesto ObtenerCatalogoPuestosXId(int id)
        {
            return _catalogosAD.ObtenerCatalogoPuestosXId(id);
        }

        public List<Catalogo_perfil_puesto> ObtenerPuestosTecnicos()
        {
            var idsTecnicos = new List<int> { 10, 11 };
            return _catalogosAD.ObtenerPuestosPorIds(idsTecnicos);
        }




        //Catalogos de estado de aprobacion y nomina
        public Catalogo_estado_aprobacion ObtenerCatalogoEstadoAprobacionXId(int idEstado)
        {
            return _catalogosAD.ObtenerCatalogoEstadoAprobacionXId(idEstado);
        }
        public List<Catalogo_estado_aprobacion> ObtenerCatalogoEstadoAprobacion()
        {
            return _catalogosAD.ObtenerCatalogoEstadoAprobacion();
        }
        public Periodo_nomina ObtenerPeriodoNominaXId(int idPeriodo)
        {
            return _catalogosAD.ObtenerPeriodoNominaXId(idPeriodo);
        }
        public List<Periodo_nomina> ObtenerCatalogoPeriodoNomina()
        {
            return _catalogosAD.ObtenerCatalogoPeriodoNomina();
        }


        //Catalogos de estado de orden
        public List<Catalogo_estado_orden> ObtenerCatalogoEstadoOrden()
        {
            return _catalogosAD.ObtenerCatalogoEstadoOrden();
        }
        public Catalogo_estado_orden ObtenerCatalogoEstadoOrdenXId(int idEstadoOrden)
        {
            return _catalogosAD.ObtenerCatalogoEstadoOrdenXId(idEstadoOrden);
        }


        //Catalogos de bonificaciones
        public List<Catalogo_bonificaciones> ObtenerCatalogoBonificacion()
        {
            return _catalogosAD.ObtenerCatalogoBonificacion();
        }
        public Catalogo_bonificaciones ObtenerCatalogoBonificacionXId(int idBonificacion)
        {
            return _catalogosAD.ObtenerCatalogoBonificacionXId(idBonificacion);
        }


        //Catalogos de ubicaciones

        public List<Catalogo_provincia> ObtenerCatalogo_Provincias()
        {
            return _catalogosAD.ObtenerCatalogo_Provincias();
        }

        public List<Catalogo_canton> ObtenerCatalogo_Cantones(int idProvincia)
        {
            return _catalogosAD.ObtenerCatalogo_Cantones(idProvincia);
        }

        public List<Catalogo_distrito> ObtenerCatalogo_Distritos(int idCanton)
        {
            return _catalogosAD.ObtenerCatalogo_Distritos(idCanton);
        }

    }
}
