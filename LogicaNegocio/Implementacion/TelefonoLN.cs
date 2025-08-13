using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class TelefonoLN : ITelefonoLN
    {
        private readonly ITelefonoAD _telefonoAD;
        public TelefonoLN(ITelefonoAD telefonoAD)
        {
            _telefonoAD = telefonoAD;
        }
        public void InsertarTelefono(Telefono telefono)
        {
            _telefonoAD.InsertarTelefono(telefono);
        }
        public void ActualizarTelefono(Telefono telefono)
        {
            _telefonoAD.ActualizarTelefono(telefono);
        }
        public void EliminarTelefono(int idTelefono)
        {
            _telefonoAD.EliminarTelefono(idTelefono);
        }
        public List<Telefono> ObtenerTelefonosPorPersona(string personaId)
        {
            return _telefonoAD.ObtenerTelefonosPorPersona(personaId);
        }

        public Telefono ObtenerTelefonoPorId(int idTelefono)
        {
            return _telefonoAD.ObtenerTelefonoPorId(idTelefono);
        }
    }
}
