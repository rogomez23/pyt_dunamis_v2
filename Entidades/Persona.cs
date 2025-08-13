
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Persona
    {
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe tener 9 dígitos numéricos.")]
        [Display(Name = "Número de cédula")]
        public string id_persona { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
        ErrorMessage = "El nombre sólo puede contener letras y espacios.")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$",
        ErrorMessage = "El primer apellido sólo puede contener letras y espacios.")]
        [Display(Name = "Primer apellido")]
        public string apellido_1 { get; set; }


        [RegularExpression(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]*$",
        ErrorMessage = "El segundo apellido sólo puede contener letras y espacios.")]
        [Display(Name = "Segundo apellido")]
        public string apellido_2 { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public bool inactivo { get; set; }
        public List<Email> emails { get; set; } = new List<Email>();
        public List<Cliente> clientes { get; set; } = new List<Cliente>();
        public List<Telefono> telefono { get; set; } = new List<Telefono>();
        public List<Direccion> direcciones { get; set; } = new List<Direccion>();
        public Colaborador colaborador { get; set; }

    }
}
