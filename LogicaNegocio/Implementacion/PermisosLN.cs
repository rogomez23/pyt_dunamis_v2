using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class PermisosLN : IPermisosLN
    {
        private readonly IPermisosAD _permisosAD;
        public PermisosLN(IPermisosAD permisosAD)
        {
            _permisosAD = permisosAD;
        }
        public List<Permisos> ObtenerPermisos()
        {
            return _permisosAD.ObtenerPermisos();
        }
        public Permisos ObtenerPermisosPorId(int id_permiso)
        {
            return _permisosAD.ObtenerPermisosPorId(id_permiso);
        }
        public void Agregar(Permisos p)
        {
            _permisosAD.Agregar(p);
        }
        public void Actualizar(Permisos p)
        {
            _permisosAD.Actualizar(p);
        }
        public void Eliminar(int id_permiso)
        {
            _permisosAD.Eliminar(id_permiso);

        }
    }
}
