using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    [Table("bonificacion_puestos")]
    public class Bonificacion_puesto
    {
        public int catalogo_perfil_puesto_id_perfil_puesto { get; set; }
        public int catalogo_bonificaciones_id_catalogo_bonificacion {  get; set; }
        public Catalogo_bonificaciones catalogo_Bonificaciones { get; set; }
        public Catalogo_perfil_puesto catalogo_Perfil_Puesto { get; set; }
    }
}
