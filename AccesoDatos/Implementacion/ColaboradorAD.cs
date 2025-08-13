using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class ColaboradorAD : IColaboradorAD
    {
        private readonly AppDbContext _context;

        public ColaboradorAD(AppDbContext context)
        {
            _context = context;
        }

        public List<Colaborador> ObtenerColaboradores()
        {
            return _context.Colaborador.ToList();
        }

        public void InsertarColaborador(Colaborador colaborador)
        {
            _context.Colaborador.Add(colaborador);
            _context.SaveChanges();
        }

        public void ActualizarColaborador(Colaborador colaborador)
        {
            _context.Colaborador.Update(colaborador);
            _context.SaveChanges();
        }

        public void EliminarColaborador(Colaborador colaborador)
        {
            _context.Colaborador.Update(colaborador);
            _context.SaveChanges();
        }

        public Colaborador ObtenerColaboradorPorId(int id)
        {
            return _context.Colaborador
                .Include(c => c.Persona)
                .Include(c => c.Catalogo_perfil_puesto)
                .FirstOrDefault(c => c.id_colaborador == id);
        }

        public Colaborador ObtenerColaboradorPorIdPersona(string idPersona)
        {
            return _context.Colaborador.Include(c => c.Catalogo_perfil_puesto)
                                       .FirstOrDefault(c => c.persona_id_persona == idPersona);
        }

        public List<Colaborador> ObtenerColaboradoresPorPuesto(List<int> idsPuesto)
        {
            return _context.Colaborador
                .Include(c => c.Persona)
                .Include(c => c.Catalogo_perfil_puesto)
                .Where(c => idsPuesto.Contains(c.catalogo_perfil_puesto_id_perfil_puesto))
                .ToList();
        }

    }
}
