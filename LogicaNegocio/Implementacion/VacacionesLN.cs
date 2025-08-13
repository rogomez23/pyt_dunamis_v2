using AccesoDatos.Implementacion;
using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class VacacionesLN : IVacacionesLN
    {
        private readonly IVacacionesAD _vacacionesAD;
        private readonly IColaboradorAD _colaboradorAD;

        public VacacionesLN(IVacacionesAD vacacionesAD, IColaboradorAD colaboradorAD)
        {
            _vacacionesAD = vacacionesAD;
            _colaboradorAD = colaboradorAD;
        }

        public List<Vacaciones> ListaVacacionesPorColaborador(int idColaborador)
        {
            return _vacacionesAD.ListaVacacionesPorColaborador(idColaborador);
        }

        public List<Vacaciones> ListaVacacionesPorIdEstadoAprobacion(int idEstado)
        {
            return _vacacionesAD.ListaVacacionesPorIdEstadoAprobacion(idEstado);
        }

        public string SolicitarVacaciones(Vacaciones v)
        {
            if (_vacacionesAD.ExisteSolicitudEnFecha(v.colaborador_id_colaborador, v.fecha_inicio_vacaciones, v.fecha_fin_vacaciones))
            {
                return "Ya existe una solicitud de vacaciones para este rango de fecha.";
            }
            _vacacionesAD.SolicitarVacaciones(v);

            return null;
        }

        public void EditarVacaciones(Vacaciones v)
        {
            _vacacionesAD.EditarVacaciones(v);
        }

        public void EliminarVacaciones(int id)
        {
            _vacacionesAD.EliminarVacaciones(id);
        }

        public List<Vacaciones> ObtenerVacacionesProcesadas(int idColaborador)
        {
            return _vacacionesAD.ObtenerVacacionesProcesadas(idColaborador);
        }

        public void ActualizarEstadosVacaciones(List<int> ids, int nuevoEstado)
        {
            _vacacionesAD.ActualizarEstadosVacaciones(ids, nuevoEstado);
        }

        public void ActualizarEstadosYPeriodoVacaciones(List<int> ids, int nuevoEstado, int idPeriodo)
        {
            _vacacionesAD.ActualizarEstadosYPeriodoVacaciones(ids, nuevoEstado, idPeriodo);
        }



        //Cálculo de días disponibles de vacaciones
        public decimal CalcularDiasDisponibles(int idColaborador)
        {
            var colaborador = _colaboradorAD.ObtenerColaboradorPorId(idColaborador);
            if (colaborador == null || colaborador.fecha_ingreso == null)
                return 0;

            var fechaIngreso = colaborador.fecha_ingreso;

            // Calcular meses completos transcurridos
            var mesesTranscurridos = ((DateTime.Now.Year - fechaIngreso.Year) * 12) + (DateTime.Now.Month - fechaIngreso.Month);

            // Acumulan 1 días por mes
            var diasAcumulados = mesesTranscurridos * 1m;

            // Obtener la suma de los días ya tomados en vacaciones procesadas (estado aprobación 4)
            var vacacionesProcesadas = _vacacionesAD.ObtenerVacacionesProcesadas(idColaborador);
            var diasTomados = vacacionesProcesadas.Sum(v => v.cantidad_dias_solicitados);

            var diasDisponibles = diasAcumulados - diasTomados;

            return diasDisponibles < 0 ? 0 : diasDisponibles;
        }
    }
}
