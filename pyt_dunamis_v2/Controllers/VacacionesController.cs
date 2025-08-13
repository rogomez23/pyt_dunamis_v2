using Entidades;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Helpers;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class VacacionesController : Controller
    {

        private readonly IVacacionesLN _vacacionesLN;
        private readonly IColaboradorLN _colaboradorLN;
        private readonly ICatalogosLN _catalogoLN;
        public VacacionesController(IVacacionesLN vacacionesLN, IColaboradorLN colaboradorLN, ICatalogosLN catalogosLN )
        {
            _vacacionesLN = vacacionesLN;
            _colaboradorLN = colaboradorLN;
            _catalogoLN = catalogosLN;
        }


        [HttpGet]
        public IActionResult PanelVacaciones()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var colaborador = _colaboradorLN.ObtenerColaboradorPorId(idColaborador);
            var historial = _vacacionesLN.ListaVacacionesPorColaborador(idColaborador);

            var modelo = new PanelVacacionesViewModel
            {
                Colaborador = colaborador,
                HistorialVacaciones = historial
            };

            return View(modelo);
        }


        public IActionResult HistorialVacaciones()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            var lista = _vacacionesLN.ListaVacacionesPorColaborador(idColaborador);
            return View(lista);
        }


        //Solictar las vacaciones
        [HttpGet]
        public IActionResult SolicitarVacaciones()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var diasDisponibles = _vacacionesLN.CalcularDiasDisponibles(idColaborador);
            ViewBag.DiasDisponibles = diasDisponibles;

            var modelo = new Vacaciones
            {
                fecha_solicitud = DateTime.Today,
                fecha_inicio_vacaciones = DateTime.Today,
                fecha_fin_vacaciones = DateTime.Today,
            };

            return View(modelo);
        }

        [HttpPost]
        public IActionResult SolicitarVacaciones(Vacaciones model)
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var diasDisponibles = _vacacionesLN.CalcularDiasDisponibles(idColaborador);

            if (model.cantidad_dias_solicitados > diasDisponibles)
            {
                ModelState.AddModelError("cantidad_dias_solicitados", "No puede solicitar más días de los disponibles.");
                ViewBag.DiasDisponibles = diasDisponibles;
                return View(model);
            }

            model.colaborador_id_colaborador = idColaborador;
            model.fecha_solicitud = DateTime.Now;
            model.catalogo_estado_aprobacion_id_estado_aprobacion = 1; // Asignar estado "pendiente" u otro
            model.periodo_nomina_idperiodo_nomina = 1; // Al inicio no se asigna a ningún periodo de nómina específico, eso lo hace el de planilla

            string resultado = _vacacionesLN.SolicitarVacaciones(model);

            if (!string.IsNullOrEmpty(resultado))
            {
                ModelState.AddModelError(string.Empty, resultado); // Mostrar mensaje
                ViewBag.DiasDisponibles = diasDisponibles;
                return View(model); // Devuelve la vista con el mensaje
            }

            //_vacacionesLN.SolicitarVacaciones(model);

            TempData["Mensaje"] = "Vacaciones solicitadas exitosamente.";
            return RedirectToAction("PanelVacaciones");
        }



        //Aprobación de vacaciones
        [HttpGet]
        public IActionResult AprobacionVacaciones() // 1 = Pendiente por defecto
        {
            List<Vacaciones> Lista;

            if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_JEFES"))
            {
                Lista = _vacacionesLN.ListaVacacionesPorIdEstadoAprobacion(1);
            }
            else if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_PLANILLA"))
            {
                Lista = _vacacionesLN.ListaVacacionesPorIdEstadoAprobacion(2);
            }
            else
            {
                return RedirectToAction("PanelVacaciones");
            }
            return View(Lista);
        }

        [HttpPost]
        public IActionResult ActualizarEstadosVacaciones(List<int> ids, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("AprobacionVacaciones");

            _vacacionesLN.ActualizarEstadosVacaciones(ids, nuevoEstado);
            return RedirectToAction("AprobacionVacaciones");
        }




        //Ejecución para nómina
        [HttpGet]
        public IActionResult EjecutarVacacionesNomina()
        {
            if (!SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_PLANILLA"))
                return RedirectToAction("PanelVacaciones");

            var vacaciones = _vacacionesLN.ListaVacacionesPorIdEstadoAprobacion(3);
            var periodos = _catalogoLN.ObtenerCatalogoPeriodoNomina();

            ViewBag.Periodos = new SelectList(periodos, "id_periodo_nomina", "nombre_periodo"); // o el campo que usés para mostrar
            return View(vacaciones);
        }

        [HttpPost]
        public IActionResult ActualizarPeriodo(List<int> ids, int idPeriodoSeleccionado, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("EjecutarVacacionesNomina");

            _vacacionesLN.ActualizarEstadosYPeriodoVacaciones(ids, nuevoEstado, idPeriodoSeleccionado); // nuevo método
            return RedirectToAction("EjecutarVacacionesNomina");
        }
    }
}
