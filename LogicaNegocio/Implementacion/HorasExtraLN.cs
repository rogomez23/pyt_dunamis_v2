using AccesoDatos.Implementacion;
using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class HorasExtraLN : IHorasExtraLN
    {
        private readonly IHorasExtraAD _horasExtraAD;
        private readonly IColaboradorAD _colaboradorAD;
        public HorasExtraLN(IHorasExtraAD horasExtraAD, IColaboradorAD colaboradorAD)
        {
            _horasExtraAD = horasExtraAD;
            _colaboradorAD = colaboradorAD;
        }

        public List<Horas_extra> ListaHorasExtraPorColaborador(int idColaborador)
        {
            return _horasExtraAD.ListaHorasExtraPorColaborador(idColaborador);
        }
        public List<Horas_extra> ListaHorasExtraPorIdEstadoAprobacion(int idEstado)
        {
            return _horasExtraAD.ListaHorasExtraPorIdEstadoAprobacion(idEstado);
        }
        public string SolicitarHorasExtra(Horas_extra he)
        {

            if (_horasExtraAD.ExisteSolicitudEnFecha(he.colaborador_id_colaborador, he.fecha_hora_extras))
            {
                return "Ya existe una solicitud de horas extra para esta fecha.";
            }



            var colaborador = _colaboradorAD.ObtenerColaboradorPorId(he.colaborador_id_colaborador);

            // Suponiendo que el salario base está en el perfil de puesto
            var salarioBase = colaborador.Catalogo_perfil_puesto.salario_base;
            decimal diasdelmes = 30; // Ajustable si es necesario
            decimal horasPorDia = 8; // Asumiendo una jornada de 8 horas diarias
            decimal tarifa = ((salarioBase / diasdelmes)/horasPorDia) * 1.5m;

            he.tarifa_por_hora = Math.Round(tarifa, 2);
            he.monto_extras = Math.Round(tarifa * he.cantidad_extras, 2);

            _horasExtraAD.SolicitarHorasExtra(he);

            return null;
        }

        public void EditarHorasExtra(Horas_extra he)
        {
            _horasExtraAD.EditarHorasExtra(he);
        }
        public void EliminarHorasExtra(int id)
        {
            _horasExtraAD.EliminarHorasExtra(id);
        }
        public List<Horas_extra> ObtenerHorasExtraProcesadas(int idColaborador)
        {
            return _horasExtraAD.ObtenerHorasExtraProcesadas(idColaborador);
        }
        public void ActualizarEstadosHorasExtra(List<int> ids, int nuevoEstado)
        {
            _horasExtraAD.ActualizarEstadosHorasExtra(ids, nuevoEstado);
        }
        public void ActualizarEstadosYPeriodoHorasExtra(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            _horasExtraAD.ActualizarEstadosYPeriodoHorasExtra(ids, nuevoEstado, idPeriodo);


        }


    }
}
