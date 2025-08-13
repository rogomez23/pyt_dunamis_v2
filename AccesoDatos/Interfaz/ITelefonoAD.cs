using Entidades;


namespace AccesoDatos.Interfaz
{
    public interface ITelefonoAD
    {
        void InsertarTelefono(Telefono telefono);
        void ActualizarTelefono(Telefono telefono);
        void EliminarTelefono(int idTelefono);
        List<Telefono> ObtenerTelefonosPorPersona(string personaId);
        Telefono ObtenerTelefonoPorId(int idTelefono);
    }
}
