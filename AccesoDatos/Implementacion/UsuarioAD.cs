using AccesoDatos.Interfaz;
using Entidades;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Implementacion
{
    public class UsuarioAD : IUsuarioAD
    {
        private readonly AppDbContext _ctx;

        public UsuarioAD(AppDbContext context)
        {
            _ctx = context;
        }


        public IEnumerable<Usuarios> ObtenerTodos() =>
            _ctx.Usuarios
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                .ToList();

        public Usuarios ObtenerPorId(int id_usuario) =>
            _ctx.Usuarios
                .Include(u => u.UsuarioRoles)
                    .ThenInclude(ur => ur.Rol)
                .FirstOrDefault(u => u.id_usuario == id_usuario);

        public void Agregar(Usuarios u)
        {
            _ctx.Usuarios.Add(u);
            _ctx.SaveChanges();
        }

        public Usuarios ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return _ctx.Usuarios.FirstOrDefault(u => u.nombre_usuario == nombreUsuario);
        }

        public void Actualizar(Usuarios u)
        {
            _ctx.Usuarios.Update(u);
            _ctx.SaveChanges();
        }

        public void Eliminar(int id_usuario)
        {
            var u = _ctx.Usuarios.Find(id_usuario);
            if (u != null) _ctx.Usuarios.Remove(u);
            _ctx.SaveChanges();
        }

        public void AsignarRol(int id_usuario, int id_rol)
        {
            _ctx.UsuarioRoles.Add(new Usuarios_roles { id_usuario = id_usuario, id_rol = id_rol });
            _ctx.SaveChanges();
        }

        public void QuitarRol(int id_usuario, int id_rol)
        {
            var ur = _ctx.UsuarioRoles
                .FirstOrDefault(x => x.id_usuario == id_usuario && x.id_rol == id_rol);
            if (ur != null) _ctx.UsuarioRoles.Remove(ur);
            _ctx.SaveChanges();
        }

        public Usuarios ObtenerUsuarioPorIdColaborador(int idColaborador)
        {
            return _ctx.Usuarios.Include(c => c.colaborador_id_colaborador)
                                       .FirstOrDefault(c => c.colaborador_id_colaborador == idColaborador);
        }

        public bool ColaboradorTieneUsuario(int colaboradorId)
        {
            // Verifica si existe algún usuario cuyo colaborador_id_colaborador coincida con el id dado
            return _ctx.Usuarios.Any(u => u.colaborador_id_colaborador == colaboradorId);
        }
    }
}
