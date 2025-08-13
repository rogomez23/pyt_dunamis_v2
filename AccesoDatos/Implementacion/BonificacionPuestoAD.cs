using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class BonificacionPuestoAD : IBonificacionPuestoAD
    {
        private readonly AppDbContext _appDbContext;

        public BonificacionPuestoAD(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Catalogo_perfil_puesto> ObtenerListaBonificacionPuesto() =>
            _appDbContext.Catalogo_perfil_puesto
                .Include(u => u.Bonificacion_puesto)
                    .ThenInclude(ur => ur.catalogo_Bonificaciones)
                .Include(u => u.Colaboradores)
                    .ThenInclude(c => c.Persona)
                .ToList();

        public Catalogo_perfil_puesto ObtenerBonificacionPuestoPorId(int id_perfil_puesto) =>
            _appDbContext.Catalogo_perfil_puesto
                .Include(u => u.Bonificacion_puesto)
                    .ThenInclude(ur => ur.catalogo_Bonificaciones)
                .FirstOrDefault(u => u.id_perfil_puesto == id_perfil_puesto);

        public void AsignarBonificacion(int id_puesto, int id_bonificacion)
        {
            _appDbContext.Bonificacion_puesto
                .Add(new Bonificacion_puesto { catalogo_perfil_puesto_id_perfil_puesto = id_puesto, 
                    catalogo_bonificaciones_id_catalogo_bonificacion = id_bonificacion });
            _appDbContext.SaveChanges();
        }

        public void QuitarBonificacion(int id_puesto, int id_bonificacion)
        {
            // 1) Recupera todas las filas que matcheen
            var lista = _appDbContext.Bonificacion_puesto
                .Where(b =>
                    b.catalogo_perfil_puesto_id_perfil_puesto == id_puesto &&
                    b.catalogo_bonificaciones_id_catalogo_bonificacion == id_bonificacion
                )
                .ToList();

            if (lista.Any())
            {
                // 2) Elimínalas de golpe
                _appDbContext.Bonificacion_puesto.RemoveRange(lista);

                // 3) Guarda los cambios
                _appDbContext.SaveChanges();
            }
        }

    }
}
