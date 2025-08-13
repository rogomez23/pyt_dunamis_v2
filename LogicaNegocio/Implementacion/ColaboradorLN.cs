using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class ColaboradorLN : IColaboradorLN
    {
        private readonly IColaboradorAD _colaboradorAD;

        public ColaboradorLN(IColaboradorAD colaboradorAD)
        {
            _colaboradorAD = colaboradorAD;
        }

        // Obtener lista de colaboradores desde la base de datos
        public List<Colaborador> ObtenerColaboradores()
        {
            return _colaboradorAD.ObtenerColaboradores();
        }
        // Insertar colaborador en la base de datos
        public void InsertarColaborador(Colaborador colaborador)
        {
            _colaboradorAD.InsertarColaborador(colaborador);
        }
        // Actualizar colaborador en la base de datos
        public void ActualizarColaborador(Colaborador colaborador)
        {
            _colaboradorAD.ActualizarColaborador(colaborador);
        }
        // Eliminar colaborador en la base de datos
        public void EliminarColaborador(Colaborador colaborador)
        {
            _colaboradorAD.EliminarColaborador(colaborador);
        }
        // Obtener colaborador por ID
        public Colaborador ObtenerColaboradorPorId(int id)
        {
            return _colaboradorAD.ObtenerColaboradorPorId(id);
        }

        // Obtener colaborador por ID de persona
        public Colaborador ObtenerColaboradorPorIdPersona(string idPersona)
        {
            return _colaboradorAD.ObtenerColaboradorPorIdPersona(idPersona);

        }

        // Obtener colaboradores por perfiles
        public List<Colaborador> ObtenerColaboradoresPorPuesto(List<int> idsPuesto)
        {
            return _colaboradorAD.ObtenerColaboradoresPorPuesto(idsPuesto);

        }

    }
}
