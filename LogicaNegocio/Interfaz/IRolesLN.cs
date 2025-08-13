using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IRolesLN
    {
        IEnumerable<Roles> ObtenerTodos();
        Roles ObtenerPorId(int id_rol);
        void Agregar(Roles u);
        void Actualizar(Roles u);
        void Eliminar(int id_rol);
        void AsignarPermiso(int id_rol, int id_permiso);
        void QuitarPermiso(int id_rol, int id_permiso);
    }
}
