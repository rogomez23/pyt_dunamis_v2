using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("permisos_roles")]
    public class Permisos_roles
    {

        public int id_rol { get; set; }
        public int id_permiso { get; set; }
        public Permisos Permiso { get; set; }
        public Roles Rol { get; set; }
    }

}
