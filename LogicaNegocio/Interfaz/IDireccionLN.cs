using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IDireccionLN
    {
        void InsertarDireccion(Direccion direccion);
        void ActualizarDireccion(Direccion direccion);
        void EliminarDireccion(int idDireccion);
        Direccion ObtenerDireccionPorId(int idDireccion);

        List<Direccion> ObtenerDireccionesPorPersona(string personaId);
        List<Direccion> ObtenerDireccionCompletaPorPersona(string personaId);
    }
}
