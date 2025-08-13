using Entidades;

namespace LogicaNegocio.Interfaz
{
    public interface IPersonaLN
    {
        List<Persona> ObtenerPersonas();
        void InsertarPersona(Persona persona);
        Persona ObtenerPersonaPorId(string id);
        void ActualizarPersona(Persona persona);
        void EliminarPersona(Persona persona);
        List<Persona> BuscarPersona(string tipoBusqueda, string valor);
        bool ExistePersonaPorId(string id);
    }
}
