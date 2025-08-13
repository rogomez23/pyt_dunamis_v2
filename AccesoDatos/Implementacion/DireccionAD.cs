using AccesoDatos.Interfaz;
using Entidades;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class DireccionAD : IDireccionAD
    {
        private readonly AppDbContext _context;

        public DireccionAD(AppDbContext context)
        {
            _context = context;
        }
        public void InsertarDireccion(Direccion direccion)
        {
            _context.Direccion.Add(direccion);
            _context.SaveChanges();
        }
        public void ActualizarDireccion(Direccion direccion)
        {
            _context.Direccion.Update(direccion);
            _context.SaveChanges();
        }
        public void EliminarDireccion(int idDireccion)
        {
            var direccion = _context.Direccion.Find(idDireccion);
            if (direccion != null)
            {
                _context.Direccion.Remove(direccion);
                _context.SaveChanges();
            }
        }

        public Direccion ObtenerDireccionPorId(int idDireccion)
        {
            return _context.Direccion.Find(idDireccion);
        }

        public List<Direccion> ObtenerDireccionesPorPersona(string personaId)
        {
            return _context.Direccion
                           .Where(d => d.persona_id_persona == personaId)
                           .ToList();
        }
        public List<Direccion> ObtenerDireccionCompletaPorPersona(string personaId)
        {
            return _context.Direccion
                .Include(d => d.Catalogo_distrito)
                .Include(d => d.Catalogo_canton)
                .Include(d => d.Catalogo_provincia)
                .Include(d => d.Catalogo_tipo_direccion)
                .Where(d => d.persona_id_persona == personaId)
                .ToList();
        }


    }
}
