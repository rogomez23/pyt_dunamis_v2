using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class RolesLN : IRolesLN
    {
        private readonly IRolesAD _rolesAD;
        public RolesLN(IRolesAD rolesAD)
        {
            _rolesAD = rolesAD;
        }

        public IEnumerable<Roles> ObtenerTodos()
        {
            return _rolesAD.ObtenerTodos();
        }
        public Roles ObtenerPorId(int id_rol)
        {
            return _rolesAD.ObtenerPorId(id_rol);
        }
        public void Agregar(Roles u)
        {
            _rolesAD.Agregar(u);
        }
        public void Actualizar(Roles u)
        {
            _rolesAD.Actualizar(u);
        }
        public void Eliminar(int id_rol)
        {
            _rolesAD.Eliminar(id_rol);
        }
        public void AsignarPermiso(int id_rol, int id_permiso)
        {
            _rolesAD.AsignarPermiso(id_rol, id_permiso);
        }
        public void QuitarPermiso(int id_rol, int id_permiso)
        {
            _rolesAD.QuitarPermiso(id_rol, id_permiso);
        }
    }
}
