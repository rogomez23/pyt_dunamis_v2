using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;
using System.Security.Cryptography;
using System.Text;

namespace LogicaNegocio.Implementacion
{
    public class LoginLN : ILoginLN
    {
        private readonly ILoginAD _loginAD;

        public LoginLN(ILoginAD loginAD)
        {
            _loginAD = loginAD;
        }

        public Usuarios Autenticar(string nombreUsuario, string contrasenaPlano)
        {
            var usuario = _loginAD.ObtenerPorNombreUsuario(nombreUsuario);

            if (usuario == null)
                return null;

            string hash = Hash(contrasenaPlano);

            return usuario.contrasena == hash ? usuario : null;
        }

        private string Hash(string pwd)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            return Convert.ToBase64String(bytes);
        }
    }
}

