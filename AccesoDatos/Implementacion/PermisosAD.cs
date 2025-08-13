using AccesoDatos.Interfaz;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Implementacion
{
    public class PermisosAD : IPermisosAD
    {
        private readonly AppDbContext _ctx;
        public PermisosAD(AppDbContext context)
        {
            _ctx = context;
        }
        public List<Permisos> ObtenerPermisos()
        {
            return _ctx.Permisos.ToList();

        }
        public Permisos ObtenerPermisosPorId(int id_permiso)
        {
            return _ctx.Permisos.Find(id_permiso);
        }
        public void Agregar(Permisos p)
        {
            _ctx.Permisos.Add(p);
            _ctx.SaveChanges();
        }
        public void Actualizar(Permisos p)
        {
            _ctx.Permisos.Update(p);
            _ctx.SaveChanges();
        }
        public void Eliminar(int id_permiso)
        {
            var p = _ctx.Permisos.Find(id_permiso);
            if (p != null) _ctx.Permisos.Remove(p);
            _ctx.SaveChanges();
        }

    }
}
