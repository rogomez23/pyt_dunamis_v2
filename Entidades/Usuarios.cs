using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Usuarios
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public bool inactivo { get; set; } = true;

        [Column("colaborador_id_colaborador")]
        public int colaborador_id_colaborador { get; set; }

        [ForeignKey("colaborador_id_colaborador")]
        public Colaborador Colaborador { get; set; }
        public List<Usuarios_roles> UsuarioRoles { get; set; } = new List<Usuarios_roles>();
        
    }
}
