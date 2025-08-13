using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class EmailLN : IEmailLN
    {
        private readonly IEmailAD _emailAD;
        public EmailLN(IEmailAD emailAD)
        {
            _emailAD = emailAD;
        }

        public void InsertarEmail(Email email)
        {
            _emailAD.InsertarEmail(email);
        }
        public void ActualizarEmail(Email email)
        {
            _emailAD.ActualizarEmail(email);
        }
        public void EliminarEmail(int idEmail)
        {
            _emailAD.EliminarEmail(idEmail);
        }
        public Email ObtenerEmailPorId(int idEmail)
        {
            return _emailAD.ObtenerEmailPorId(idEmail);
        }
        public List<Email> ObtenerEmailsPorPersona(string personaId)
        {
            return _emailAD.ObtenerEmailsPorPersona(personaId);
        }
    }
}
