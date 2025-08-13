using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class RolesAD : IRolesAD
    {
        private readonly AppDbContext _ctx;

        public RolesAD(AppDbContext context)
        {
            _ctx = context;
        }

        public IEnumerable<Roles> ObtenerTodos() =>
            _ctx.Roles
                .Include(u => u.PermisoRol)
                    .ThenInclude(ur => ur.Permiso)
                .ToList();

        public Roles ObtenerPorId(int id_rol) =>
            _ctx.Roles
                .Include(u => u.PermisoRol)
                    .ThenInclude(ur => ur.Permiso)
                .FirstOrDefault(u => u.id_rol == id_rol);

        public void Agregar(Roles u)
        {
            _ctx.Roles.Add(u);
            _ctx.SaveChanges();
        }

        public void Actualizar(Roles u)
        {
            _ctx.Roles.Update(u);
            _ctx.SaveChanges();
        }

        public void Eliminar(int id_rol)
        {
            var u = _ctx.Roles.Find(id_rol);
            if (u != null) _ctx.Roles.Remove(u);
            _ctx.SaveChanges();
        }

        public void AsignarPermiso(int id_rol, int id_permiso)
        {
            _ctx.PermisoRoles.Add(new Permisos_roles { id_rol = id_rol, id_permiso = id_permiso, });
            _ctx.SaveChanges();
        }

        public void QuitarPermiso(int id_rol, int id_permiso)
        {
            var ur = _ctx.PermisoRoles
                .FirstOrDefault(x => x.id_rol == id_rol && x.id_permiso == id_permiso);
            if (ur != null) _ctx.PermisoRoles.Remove(ur);
            _ctx.SaveChanges();
        }

    }
}
