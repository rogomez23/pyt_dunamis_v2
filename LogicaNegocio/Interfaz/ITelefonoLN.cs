using Entidades;


namespace LogicaNegocio.Interfaz
{
    public interface ITelefonoLN
    {
        void InsertarTelefono(Telefono telefono);
        void ActualizarTelefono(Telefono telefono);
        void EliminarTelefono(int idTelefono);
        List<Telefono> ObtenerTelefonosPorPersona(string personaId);
        Telefono ObtenerTelefonoPorId(int idTelefono);
    }
}
