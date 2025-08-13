using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface IUsuarioAD
    {
        IEnumerable<Usuarios> ObtenerTodos();
        Usuarios ObtenerPorId(int id_usuario);
        void Agregar(Usuarios u);
        Usuarios ObtenerPorNombreUsuario(string nombreUsuario);
        void Actualizar(Usuarios u);
        void Eliminar(int id_usuario);
        void AsignarRol(int id_usuario, int id_rol);
        void QuitarRol(int id_usuario, int id_rol);
        Usuarios ObtenerUsuarioPorIdColaborador(int idColaborador);
        bool ColaboradorTieneUsuario(int colaboradorId);

    }
}
