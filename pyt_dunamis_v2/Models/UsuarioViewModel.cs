using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Specialized;

namespace pyt_dunamis_v2.Models
{
    public class UsuarioViewModel
    {
        public int? id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasena { get; set; }
        public bool inactivo { get; set; }
        public int colaborador_id_colaborador { get; set; }
        public string id_persona { get; set; }

        public List<int> rolesSeleccionados { get; set; } = new List<int>();

        public List<SelectListItem> RolesDisponibles { get; set; } = new();
    }
}
