using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class TelefonoAD : ITelefonoAD
    {
        private readonly AppDbContext _context;
        public TelefonoAD(AppDbContext context)
        {
            _context = context;
        }
        public void InsertarTelefono(Telefono telefono)
        {
            _context.Telefono.Add(telefono);
            _context.SaveChanges();
        }
        public void ActualizarTelefono(Telefono telefono)
        {
            _context.Telefono.Update(telefono);
            _context.SaveChanges();
        }
        public void EliminarTelefono(int idTelefono)
        {
            var telefono = _context.Telefono.Find(idTelefono);
            if (telefono != null)
            {
                _context.Telefono.Remove(telefono);
                _context.SaveChanges();
            }
        }
        public List<Telefono> ObtenerTelefonosPorPersona(string personaId)
        {
            return _context.Telefono
                           .Where(t => t.persona_id_persona == personaId)
                           .Include(t => t.Catalogo_tipo_contacto)
                           .ToList();
        }

        public Telefono ObtenerTelefonoPorId(int idTelefono)
        {
            return _context.Telefono.Find(idTelefono);
        }

    }
}
