using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("usuarios_roles")]
    public class Usuarios_roles
    {
        public int id_usuario { get; set; }
        public int id_rol { get; set; }
        public Usuarios Usuario { get; set; }
        public Roles Rol { get; set; }

    }
}
