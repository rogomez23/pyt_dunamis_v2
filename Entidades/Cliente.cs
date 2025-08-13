namespace Entidades
{
    public class Cliente
    {
        public int id_contrato { get; set; }
        public DateTime fecha_registro { get; set; }
        public string persona_id_persona { get; set; }
        public bool inactivo { get; set; }
        public Persona Persona { get; set; }
    }
}
