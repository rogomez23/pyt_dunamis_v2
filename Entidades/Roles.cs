namespace Entidades
{
    public class Roles
    {
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
        public List<Usuarios_roles> UsuarioRoles { get; set; } = new List<Usuarios_roles>();
        public List<Permisos_roles> PermisoRol { get; set; } = new List<Permisos_roles>();
    }
}
