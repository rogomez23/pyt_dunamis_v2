using Entidades.DTOs;

namespace LogicaNegocio.Servicios
{
    public interface IRegistroColaboradorServiceLN
    {
        void InsertarColaborador(PersonaCompletaDTO vm);
    }
}
