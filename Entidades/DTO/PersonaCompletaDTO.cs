namespace Entidades.DTOs
{
    public class PersonaCompletaDTO
    {
        public Persona Persona { get; set; }
        public Colaborador Colaborador { get; set; }
        public List<Telefono> Telefonos { get; set; }
        public List<Email> Emails { get; set; }
        public List<Direccion> Direcciones { get; set; }

        public PersonaCompletaDTO()
        {
            // Inicialización para evitar errores de referencia nula
            Persona = new Persona();
            Colaborador = new Colaborador();
            Telefonos = new List<Telefono>();
            Emails = new List<Email>();
            Direcciones = new List<Direccion>();
        }
    }
}
