using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IEmailLN
    {
        void InsertarEmail(Email email);
        void ActualizarEmail(Email email);
        void EliminarEmail(int idEmail);
        Email ObtenerEmailPorId(int idEmail);
        List<Email> ObtenerEmailsPorPersona(string personaId);
    }
}
