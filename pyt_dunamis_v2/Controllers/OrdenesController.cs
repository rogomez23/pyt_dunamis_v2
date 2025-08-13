using Entidades;
using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Helpers;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly IOrdenesLN _ordenesLN;
        private readonly IColaboradorLN _colaboradorLN;
        private readonly ICatalogosLN _catalogoLN;
        public OrdenesController(IOrdenesLN ordenesLN, IColaboradorLN colaboradorLN, ICatalogosLN catalogosLN)
        {
            _ordenesLN = ordenesLN;
            _colaboradorLN = colaboradorLN;
            _catalogoLN = catalogosLN;
        }

        [HttpGet]
        public IActionResult PanelOrdenes()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var colaborador = _colaboradorLN.ObtenerColaboradorPorId(idColaborador);
            var Historial = _ordenesLN.ObtenerOrdenesPorColaborador(idColaborador);
            
            var modelo = new OrdenesViewModel
            {
                Colaborador = colaborador,
                HistorialOrdenes = Historial
            };
            return View(modelo);
        }

        [HttpGet]
        public IActionResult HistorialOrdenesPorColaborador()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");
            var lista = _ordenesLN.ObtenerOrdenesPorColaborador(idColaborador);
            return View(lista);
        }


        [HttpGet]
        public IActionResult ListaOrdenes()
        {
            var lista = _ordenesLN.ObtenerTodasOrdenes();
            return View(lista);
        }

        [HttpGet]
        public IActionResult BandejaOrdenes(int? idestado)
        {
            var estados = _catalogoLN.ObtenerCatalogoEstadoOrden();

            var lista = idestado.HasValue
                ? _ordenesLN.ObtenerOrdenesPorEstado(idestado.Value)
                : _ordenesLN.ObtenerTodasOrdenes();

            var vm = new OrdenesViewModel
            {
                HistorialOrdenes = lista,
                TipoEstadoOrden = estados.Select(e => new SelectListItem
                {
                    Value = e.id_estado_orden.ToString(),
                    Text = e.descripcion_estado,
                    Selected = e.id_estado_orden == idestado
                }).ToList()
            };

            return View(vm);
        }

        //Aprobación de ordenes
        [HttpGet]
        public IActionResult AprobacionOrdenes() // 1 = Pendiente por defecto
        {
            List<Ordenes> Lista;

            if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_ORDENES_JEFES"))
            {
                Lista = _ordenesLN.ListaOrdenesPorIdEstadoAprobacion(1);
            }
            else if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_ORDENES_PLANILLA"))
            {
                Lista = _ordenesLN.ListaOrdenesPorIdEstadoAprobacion(2);
            }
            else
            {
                return RedirectToAction("PanelOrdenes");
            }
            return View(Lista);
        }

        [HttpPost]
        public IActionResult ActualizarEstadosOrdenes(List<int> ids, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("AprobacionOrdenes");

            _ordenesLN.ActualizarEstadosAprobacionOrdenes(ids, nuevoEstado);
            return RedirectToAction("AprobacionOrdenes");
        }


        //Ejecución para nómina
        [HttpGet]
        public IActionResult EjecutarOrdenesNomina()
        {
            if (!SesionHelper.TienePermiso(HttpContext, "GESTIONAR_ORDENES_PLANILLA"))
                return RedirectToAction("PanelOrdenes");

            var ordenes = _ordenesLN.ListaOrdenesPorIdEstadoAprobacion(3);
            var periodos = _catalogoLN.ObtenerCatalogoPeriodoNomina();

            ViewBag.Periodos = new SelectList(periodos, "id_periodo_nomina", "nombre_periodo"); // o el campo que usés para mostrar
            return View(ordenes);
        }

        [HttpPost]
        public IActionResult ActualizarPeriodo(List<int> ids, int idPeriodoSeleccionado, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("EjecutarOrdenesNomina");

            _ordenesLN.ActualizarEstadosAprobacionYPeriodoOrdenes(ids, nuevoEstado, idPeriodoSeleccionado); // nuevo método
            return RedirectToAction("EjecutarOrdenesNomina");
        }














        [HttpGet]
        public IActionResult CrearOrden()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            ViewBag.TiposTrabajo = new SelectList(new[]
            {
                "Instalación de servicio",
                "Soporte técnico",
                "Reinstalación interna",
                "Reinstalación externa"
            });

            ViewBag.Estados = new SelectList(_catalogoLN.ObtenerCatalogoEstadoOrden(), "id_estado_orden", "descripcion_estado");

            var supervisor = _colaboradorLN.ObtenerColaboradoresPorPuesto(new List<int> { 8 }).FirstOrDefault(); // Suponiendo 1 = Supervisor de Operaciones

            var modelo = new Ordenes
            {
                //fecha_creacion = DateTime.Now,
                fecha_visita = DateTime.Now,
                colaborador_id_colaborador = supervisor?.id_colaborador ?? 0,
                periodo_nomina_id_periodo_nomina = 1, // Por defecto
                estado_orden_id_estado_orden = 1 // Asumimos "1" es estado pendiente
            };

            return View(modelo);
        }

        [HttpPost]
        public IActionResult CrearOrden(Ordenes model)
        {
            if (_ordenesLN.ExisteOrdenClienteMismaFecha(model.contrato_cliente, model.fecha_visita))
            {

                ViewBag.OrdenDuplicada = "Ya se encuentra una orden de trabajo registrada para este cliente en la misma fecha.";
                ViewBag.TiposTrabajo = new SelectList(new[]
                {
                    "Instalación de servicio",
                    "Soporte técnico",
                    "Reinstalación interna",
                    "Reinstalación externa"
                });

                ViewBag.Estados = new SelectList(_catalogoLN.ObtenerCatalogoEstadoOrden(), "id_estado_orden", "descripcion_estado");
                return View(model);
            }

            try
            {
                _ordenesLN.CrearOrden(model);

                TempData["Mensaje"] = "Orden creada exitosamente.";
                return RedirectToAction("CrearOrden");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al crear la orden: " + ex.Message);
                ViewBag.TiposTrabajo = new SelectList(new[]
                {
                    "Instalación de servicio",
                    "Soporte técnico",
                    "Reinstalación interna",
                    "Reinstalación externa"
                });
                ViewBag.Estados = new SelectList(_catalogoLN.ObtenerCatalogoEstadoOrden(), "id_estado_orden", "descripcion_estado");
                return View(model);




            }

        }

            //Actualizar orden
            [HttpPost]
            public IActionResult EditarOrden(OrdenesViewModel orden, string vistaOrigen)
            {
                try
                {
                    _ordenesLN.ActualizarOrden(new Ordenes
                    {
                        id_orden = orden.id_orden,
                        contrato_cliente = orden.contrato_cliente,
                        tipo_trabajo = orden.tipo_trabajo,
                        fecha_visita = orden.fecha_visita,
                        descripcion_trabajo = orden.descripcion_trabajo,
                        colaborador_id_colaborador = orden.colaborador_id_colaborador,
                        periodo_nomina_id_periodo_nomina = orden.periodo_nomina_id_periodo_nomina,
                        estado_orden_id_estado_orden = orden.estado_orden_id_estado_orden,
                        catalogo_estado_aprobacion_id_estado_aprobacion = orden.catalogo_estado_aprobacion_id_estado_aprobacion
                    });

                if (vistaOrigen == "Panel")
                    return RedirectToAction("PanelOrdenes");
                else if (vistaOrigen == "BandejaOrdenes")
                    return RedirectToAction("BandejaOrdenes");
                return RedirectToAction("BandejaOrdenes");

                }
                catch (Exception)
                {

                    if (!ModelState.IsValid)
                    {
                        // Log interno para saber qué falló
                        var errores = string.Join(" | ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));
                        Console.WriteLine("Errores de validación: " + errores);
                    }

                    ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar la orden.");
                    return PartialView("_ModalEditarOrden", orden);
                }
            }

            public IActionResult ModalEditarOrden(int id, string vistaOrigen)
            {
                var orden = _ordenesLN.ObtenerOrdenPorIdOrden(id);
                if (orden == null)
                {
                    return NotFound();
                }
                
                var vm = new OrdenesViewModel
                {
                    id_orden = orden.id_orden,
                    contrato_cliente = orden.contrato_cliente,
                    tipo_trabajo = orden.tipo_trabajo,
                    fecha_visita = orden.fecha_visita,
                    descripcion_trabajo = orden.descripcion_trabajo,
                    colaborador_id_colaborador = orden.colaborador_id_colaborador,
                    periodo_nomina_id_periodo_nomina = orden.periodo_nomina_id_periodo_nomina,
                    estado_orden_id_estado_orden = orden.estado_orden_id_estado_orden,
                    Colaborador = orden.Colaborador,
                    catalogo_estado_aprobacion_id_estado_aprobacion = orden.catalogo_estado_aprobacion_id_estado_aprobacion,
                    TipoEstadoOrden = _catalogoLN.ObtenerCatalogoEstadoOrden()
                        .Select(e => new SelectListItem
                        {
                            Value = e.id_estado_orden.ToString(),
                            Text = e.descripcion_estado
                        }).ToList()
                };
            ViewBag.VistaOrigen = vistaOrigen;
            return PartialView("_ModalEditarOrden", vm);
            }

        //Actualizar orden
        [HttpPost]
        public IActionResult AsignarOrden(OrdenesViewModel orden, string vistaOrigen)
        {
            try
            {
                _ordenesLN.ActualizarOrden(new Ordenes
                {
                    id_orden = orden.id_orden,
                    contrato_cliente = orden.contrato_cliente,
                    tipo_trabajo = orden.tipo_trabajo,
                    fecha_visita = orden.fecha_visita,
                    descripcion_trabajo = orden.descripcion_trabajo,
                    colaborador_id_colaborador = orden.colaborador_id_colaborador,
                    periodo_nomina_id_periodo_nomina = orden.periodo_nomina_id_periodo_nomina,
                    estado_orden_id_estado_orden = orden.estado_orden_id_estado_orden,
                    catalogo_estado_aprobacion_id_estado_aprobacion = orden.catalogo_estado_aprobacion_id_estado_aprobacion
                });

                if (vistaOrigen == "BandejaOrdenes")
                    return RedirectToAction("BandejaOrdenes");
                else if (vistaOrigen == "ListaOrdenes")
                    return RedirectToAction("ListaOrdenes");
                return RedirectToAction("ListaOrdenes");

            }
            catch (Exception)
            {

                if (!ModelState.IsValid)
                {
                    // Log interno para saber qué falló
                    var errores = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    Console.WriteLine("Errores de validación: " + errores);
                }

                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar la orden.");
                return PartialView("_ModalAsignarOrden", orden);
            }
        }

        public IActionResult ModalAsignarOrden(int id, string vistaOrigen)
        {
            var orden = _ordenesLN.ObtenerOrdenPorIdOrden(id);
            if (orden == null)
            {
                return NotFound();
            }

            var vm = new OrdenesViewModel
            {
                id_orden = orden.id_orden,
                contrato_cliente = orden.contrato_cliente,
                tipo_trabajo = orden.tipo_trabajo,
                fecha_visita = orden.fecha_visita,
                descripcion_trabajo = orden.descripcion_trabajo,
                colaborador_id_colaborador = orden.colaborador_id_colaborador,
                periodo_nomina_id_periodo_nomina = orden.periodo_nomina_id_periodo_nomina,
                estado_orden_id_estado_orden = 2,
                catalogo_estado_aprobacion_id_estado_aprobacion = orden.catalogo_estado_aprobacion_id_estado_aprobacion,
                Colaborador = orden.Colaborador,
                TipoEstadoOrden = _catalogoLN.ObtenerCatalogoEstadoOrden()
                    .Select(e => new SelectListItem
                    {
                        Value = e.id_estado_orden.ToString(),
                        Text = e.descripcion_estado
                    }).ToList(),
                TiposPuestos = _catalogoLN.ObtenerPuestosTecnicos()
                    .Select(p => new SelectListItem
                    {
                        Value = p.id_perfil_puesto.ToString(),
                        Text = p.descripcion_puesto
                    }).ToList(),

                Colaboradores = _colaboradorLN.ObtenerColaboradoresPorPuesto(new List<int> { 10 }) // por defecto 'Técnico de Instalación'
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_colaborador.ToString(),
                        Text = c.Persona.nombre + " " + c.Persona.apellido_1 + " " + c.Persona.apellido_2
                    }).ToList(),

            };
            ViewBag.VistaOrigen = vistaOrigen;
            return PartialView("_ModalAsignarOrden", vm);
        }

        // Acción para actualizar el combo de colaboradores según el tipo de puesto seleccionado
        [HttpGet]
        public IActionResult ObtenerColaboradoresPorPuesto(int puestoId)
        {
            // Obtener la lista de colaboradores basados en el puesto
            var colaboradores = _colaboradorLN.ObtenerColaboradoresPorPuesto(new List<int> { puestoId })
                .Select(c => new { value = c.id_colaborador, text = c.Persona.nombre + " " + c.Persona.apellido_1 + " " + c.Persona.apellido_2 })
                .ToList();

            return Json(colaboradores);
        }


    }
}
