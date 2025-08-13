using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace LogicaNegocio.Implementacion
{
    public class UsuarioLN : IUsuarioLN
    {
        private readonly IUsuarioAD _usuarioAD;
        public UsuarioLN(IUsuarioAD usuarioAD)
        {
            _usuarioAD = usuarioAD;
        }

        public IEnumerable<Usuarios> ObtenerTodos()
        {
            return _usuarioAD.ObtenerTodos();
        }
        public Usuarios ObtenerPorId(int id_usuario)
        {
            return _usuarioAD.ObtenerPorId(id_usuario);
        }
        public void Agregar(Usuarios u, string contrasenaPlano)
        {
            var usuarioExistente = _usuarioAD.ObtenerPorNombreUsuario(u.nombre_usuario);

            if (usuarioExistente != null)
                throw new InvalidOperationException("El nombre de usuario ya existe.");

            u.contrasena = Hash(contrasenaPlano);
            _usuarioAD.Agregar(u);
        }


        public void Actualizar(Usuarios u)
        {
            _usuarioAD.Actualizar(u);
        }
        public void Eliminar(int id_usuario)
        {
            _usuarioAD.Eliminar(id_usuario);
        }
        public void AsignarRol(int id_usuario, int id_rol)
        {
            _usuarioAD.AsignarRol(id_usuario, id_rol);
        }
        public void QuitarRol(int id_usuario, int id_rol)
        {
            _usuarioAD.QuitarRol(id_usuario, id_rol);

        }

        public Usuarios ObtenerUsuarioPorIdColaborador(int idColaborador)
        {
            return _usuarioAD.ObtenerUsuarioPorIdColaborador(idColaborador);
        }


        private string Hash(string pwd)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            return Convert.ToBase64String(bytes);
        }

        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            return _usuarioAD.ObtenerTodos().Any(u => u.nombre_usuario.ToLower() == nombreUsuario.ToLower());
        }

        public bool ColaboradorTieneUsuario(int colaboradorId)
        {
            return _usuarioAD.ColaboradorTieneUsuario(colaboradorId);

        }
    }
}
