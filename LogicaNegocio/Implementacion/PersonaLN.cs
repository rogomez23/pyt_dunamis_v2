using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class PersonaLN : IPersonaLN
    {
        private readonly IPersonaAD _personaAD;

        public PersonaLN(IPersonaAD personaAD)
        {
            _personaAD = personaAD;
        }

        //Obtener lista de personas desde la base de datos
        public List<Persona> ObtenerPersonas()
        {
            return _personaAD.ObtenerPersonas();
        }

        //Buscar personas por criterio y valor
        public List<Persona> BuscarPersona(string tipoBusqueda, string valor)
        {
            return _personaAD.BuscarPersona(tipoBusqueda, valor);
        }


        //Verificar si existe una persona por ID
        public bool ExistePersonaPorId(string id)
        {
            return _personaAD.ExistePersonaPorId(id);
        }

        //Insertar persona en la base de datos
        public void InsertarPersona(Persona persona)
        {
            _personaAD.InsertarPersona(persona);
        }

        public Persona ObtenerPersonaPorId(string id)
        {
            return _personaAD.ObtenerPersonaPorId(id);
        }

        public void ActualizarPersona(Persona persona)
        {
            _personaAD.ActualizarPersona(persona);
        }

        public void EliminarPersona(Persona persona)
        {
            _personaAD.EliminarPersona(persona);
        }

    }
}
