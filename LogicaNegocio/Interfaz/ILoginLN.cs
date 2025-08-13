using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface ILoginLN
    {
        Usuarios Autenticar(string nombreUsuario, string contrasenaPlano);
    }
}
