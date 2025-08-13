using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface ICatalogosAD
    {
        List<Catalogo_tipo_contacto> ObtenerCatalogoContacto();
        List<Catalogo_tipo_direccion> ObtenerCatalogoDireccion();
        List<Catalogo_perfil_puesto> ObtenerCatalogoPerfilPuesto();
        Catalogo_perfil_puesto ObtenerCatalogoPuestosXId(int idPuesto);
        List<Catalogo_provincia> ObtenerCatalogo_Provincias();
        List<Catalogo_canton> ObtenerCatalogo_Cantones(int idProvincia);
        List<Catalogo_distrito> ObtenerCatalogo_Distritos(int idCanton);
        Catalogo_estado_aprobacion ObtenerCatalogoEstadoAprobacionXId(int idEstado);
        Periodo_nomina ObtenerPeriodoNominaXId(int idPeriodo);
        List<Catalogo_estado_aprobacion> ObtenerCatalogoEstadoAprobacion();
        List<Periodo_nomina> ObtenerCatalogoPeriodoNomina();
        List<Catalogo_estado_orden> ObtenerCatalogoEstadoOrden();
        Catalogo_estado_orden ObtenerCatalogoEstadoOrdenXId(int idEstadoOrden);
        List<Catalogo_perfil_puesto> ObtenerPuestosPorIds(List<int> ids);
        List<Catalogo_bonificaciones> ObtenerCatalogoBonificacion();
        Catalogo_bonificaciones ObtenerCatalogoBonificacionXId(int idBonificacion);


    }
}
