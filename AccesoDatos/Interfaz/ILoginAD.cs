using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface ILoginAD
    {
        Usuarios ObtenerPorNombreUsuario(string nombreUsuario);
    }
}
