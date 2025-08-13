using AccesoDatos.Interfaz;
using Entidades;

namespace AccesoDatos.Implementacion
{
    public class PersonaAD : IPersonaAD
    {
        private readonly AppDbContext _context;

        public PersonaAD(AppDbContext context)
        {
            _context = context;
        }

        public List<Persona> ObtenerPersonas()
        {
            return _context.Persona.ToList();
        }



        public List<Persona> BuscarPersona(string tipoBusqueda, string valor)
        {
            IQueryable<Persona> query = _context.Persona;

            // Incluir relaciones si las necesitas

            switch (tipoBusqueda)
            {
                case "1":
                    query = query
                        .Join(_context.Cliente,
                              persona => persona.id_persona,
                              contrato => contrato.persona_id_persona,
                              (persona, contrato) => new { persona, contrato })
                        .Where(joined => joined.contrato.id_contrato.ToString().Contains(valor))
                        .Select(joined => joined.persona);
                    break;

                case "2":
                    query = query.Where(p => p.id_persona.ToString().Contains(valor));
                    break;

                case "3":
                    query = query.Where(p => p.emails.Any(e => e.email.Contains(valor)));
                    break;

                
            }

            return query.ToList();
        }

        public bool ExistePersonaPorId(string id)
        {
            return _context.Persona.Any(p => p.id_persona == id);
        }


        public void InsertarPersona(Persona persona)
        {
            _context.Persona.Add(persona);
            _context.SaveChanges();
        }


        public Persona ObtenerPersonaPorId(string id)
        {
            return _context.Persona.Find(id);
        }

        public void ActualizarPersona(Persona persona)
        {
            _context.Persona.Update(persona);
            _context.SaveChanges();
        }

        public void EliminarPersona(Persona persona)
        {
            _context.Persona.Update(persona);
            _context.SaveChanges();
        }


    }
}
