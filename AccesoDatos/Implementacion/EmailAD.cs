using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class EmailAD : IEmailAD
    {
        private readonly AppDbContext _context;

        public EmailAD(AppDbContext context)
        {
            _context = context;
        }

        public void InsertarEmail(Email email)
        {
            _context.Email.Add(email);
            _context.SaveChanges();
        }

        public void ActualizarEmail(Email email)
        {
            _context.Email.Update(email);
            _context.SaveChanges();
        }

        public void EliminarEmail(int idEmail)
        {
            var e = _context.Email.Find(idEmail);
            if (e != null)
            {
                _context.Email.Remove(e);
                _context.SaveChanges();
            }
        }

        public Email ObtenerEmailPorId(int idEmail)
        {
            return _context.Email.Find(idEmail);
        }

        public List<Email> ObtenerEmailsPorPersona(string personaId)
        {
            return _context.Email
                           .Where(e => e.persona_id_persona == personaId)
                           .Include(e => e.Catalogo_tipo_contacto)
                           .ToList();
        }


    }
}
