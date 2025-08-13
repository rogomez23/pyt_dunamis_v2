using Entidades;
namespace LogicaNegocio.Interfaz
{
    public interface IColaboradorLN
    {
        List<Colaborador> ObtenerColaboradores();
        void InsertarColaborador(Colaborador colaborador);
        void ActualizarColaborador(Colaborador colaborador);
        void EliminarColaborador(Colaborador colaborador);
        Colaborador ObtenerColaboradorPorId(int id);
        Colaborador ObtenerColaboradorPorIdPersona(string idPersona);
        List<Colaborador> ObtenerColaboradoresPorPuesto(List<int> idsPuesto);
    }
}
