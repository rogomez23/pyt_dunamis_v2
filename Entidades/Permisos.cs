namespace Entidades
{
    public class Permisos
    {
        public int id_permiso { get; set; }
        public string nom_permiso { get; set; }
        public string descripcion_permiso { get; set; }
        public List<Permisos_roles> PermisoRol { get; set; } = new List<Permisos_roles>();
    }
}
