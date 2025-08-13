using Entidades;

namespace AccesoDatos.Interfaz
{
    public interface IColaboradorAD
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
