using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class LoginAD : ILoginAD
    {
        private readonly AppDbContext _context;

        public LoginAD(AppDbContext context)
        {
            _context = context;
        }

        public Usuarios ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return _context.Usuarios
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                        .ThenInclude(r => r.PermisoRol)
                            .ThenInclude(pr => pr.Permiso)
                            .FirstOrDefault(u => u.nombre_usuario == nombreUsuario);
        }
    }
}
