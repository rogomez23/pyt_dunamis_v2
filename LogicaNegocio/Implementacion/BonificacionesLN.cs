using AccesoDatos.Interfaz;
using Entidades;
using Entidades.DTO;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class BonificacionesLN : IBonificacionesLN
    {
        private readonly IBonificacionesAD _bonificacionesAD;
        private readonly IOrdenesAD _ordenesAD;
        private readonly IBonificacionPuestoAD _bonificacionPuestoAD;
        private readonly ICatalogosAD _catalogosAD;

        public BonificacionesLN(IBonificacionesAD bonificacionesAD, IOrdenesAD ordenesAD, IBonificacionPuestoAD bonificacionPuestoAD, ICatalogosAD catalogosAD)
        {
            _bonificacionesAD = bonificacionesAD;
            _ordenesAD = ordenesAD;
            _bonificacionPuestoAD = bonificacionPuestoAD;
            _catalogosAD = catalogosAD;
        }

        public List<PreviewBonificacion> ObtenerBonificacionesPreview()
        {
            var preview = new List<PreviewBonificacion>();
            var perfiles = _bonificacionPuestoAD.ObtenerListaBonificacionPuesto();

            foreach (var perfil in perfiles)
                foreach (var col in perfil.Colaboradores)
                {
                    int total = _ordenesAD.ContarOrdenesCompletadasAprobadas(col.id_colaborador);

                    foreach (var bp in perfil.Bonificacion_puesto)
                    {
                        var cat = bp.catalogo_Bonificaciones;
                        decimal monto = 0;

                        if (cat.tipo_bonificacion == "MONTO_FIJO")
                        {
                            monto = cat.monto_fijo;
                        }
                        else if (cat.tipo_bonificacion == "RANGO_TRABAJO" &&
                                 total >= cat.rango_min && total <= cat.rango_max)
                        {
                            monto = perfil.salario_base * (cat.porcentaje / 100m);
                        }
                        else if (cat.tipo_bonificacion == "PORCENTAJE_FIJO")
                        {
                            monto = perfil.salario_base * (cat.porcentaje / 100m);
                        }
                        else continue;

                        preview.Add(new PreviewBonificacion
                        {
                            ColaboradorId = col.id_colaborador,
                            NombreColaborador = col.Persona.nombre + " " + col.Persona.apellido_1,
                            DescripcionPuesto = perfil.descripcion_puesto,
                            TipoBonificacion = cat.tipo_bonificacion,
                            MontoCalculado = monto,
                            CantidadOrdenes = total,
                            CatalogoBonificacionId = cat.id_catalogo_bonificacion,
                            DescripcionBonificacion = cat.descripcion_bonificacion,
                        });
                    }
                }

            return preview;
        }

        public void GuardarBonificaciones(List<PreviewBonificacion> items, int idPeriodo, int idEstadoAprobacion)
        {
            foreach (var p in items)
            {
                var entidad = new Bonificaciones
                {
                    fecha_asignacion = DateTime.Today,
                    colaborador_id_colaborador = p.ColaboradorId,
                    catalogo_bonificacion_id_catalogo_bonificacion = p.CatalogoBonificacionId,
                    periodo_nomina_id_periodo_nomina = idPeriodo,
                    monto_calculado = p.MontoCalculado,
                    catalogo_estado_aprobacion_id_estado_aprobacion = idEstadoAprobacion // Aprobado por Planilla
                };
                _bonificacionesAD.InsertarBonificacion(entidad);
            }
        }


        public List<PreviewBonificacion> ObtenerBonificacionesParaProcesar(int estadoId)
        {
            var entidades = _bonificacionesAD.ObtenerBonificacionesPorEstado(estadoId);
            // Mapear a DTO PreviewBonificacion (añadimos IdBD para actualizar)
            return entidades.Select(b => new PreviewBonificacion
            {
                BonificacionId = b.id_bonificaciones,
                ColaboradorId = b.colaborador_id_colaborador,
                NombreColaborador = b.Colaborador.Persona.nombre + " " + b.Colaborador.Persona.apellido_1,
                DescripcionPuesto = b.Colaborador.Catalogo_perfil_puesto.descripcion_puesto,
                TipoBonificacion = b.CatalogoBonificaciones.tipo_bonificacion,
                CantidadOrdenes = 0, // opcional: volver a contar o mostrar b...?
                MontoCalculado = b.monto_calculado,
                CatalogoBonificacionId = b.catalogo_bonificacion_id_catalogo_bonificacion,
                DescripcionBonificacion = b.CatalogoBonificaciones.descripcion_bonificacion,
                PeriodoNominaId = b.periodo_nomina_id_periodo_nomina,
                PeriodoNominaDesc = b.Periodo_nomina.nombre_periodo,
                EstadoAprobacion = b.Catalogo_estado_aprobacion.descripcion_estado
            }).ToList();
        }

        public void ActualizarEstadoBonificaciones(List<int> idsBonificaciones, int nuevoEstadoAprobacion)
        {
            _bonificacionesAD.ActualizarEstadoBonificaciones(
                idsBonificaciones, nuevoEstadoAprobacion
            );
        }

        public void ActualizarPeriodoBonificaciones(List<int> idsBonificaciones, int nuevoPeriodoNomina)
        {
            _bonificacionesAD.ActualizarPeriodoBonificaciones(
                idsBonificaciones, nuevoPeriodoNomina
            );
        }





    }
}
