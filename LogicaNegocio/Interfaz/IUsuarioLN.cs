using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IUsuarioLN
    {
        IEnumerable<Usuarios> ObtenerTodos();
        Usuarios ObtenerPorId(int id_usuario);
        void Agregar(Usuarios u, string contrasenaPlano);
        void Actualizar(Usuarios u);
        void Eliminar(int id_usuario);
        void AsignarRol(int id_usuario, int id_rol);
        void QuitarRol(int id_usuario, int id_rol);
        Usuarios ObtenerUsuarioPorIdColaborador(int idColaborador);
        bool ExisteNombreUsuario(string nombreUsuario);
        bool ColaboradorTieneUsuario(int colaboradorId);

    }
}
