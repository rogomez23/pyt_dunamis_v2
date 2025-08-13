using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface IPersonaAD
    {
        List<Persona> ObtenerPersonas();
        void InsertarPersona(Persona persona);
        Persona ObtenerPersonaPorId(string id);
        void ActualizarPersona(Persona persona);
        void EliminarPersona(Persona persona);
        List<Persona> BuscarPersona(string tipoBusqueda, string valorr);
        bool ExistePersonaPorId(string id);

    }
}
