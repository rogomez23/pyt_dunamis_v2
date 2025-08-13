using Entidades;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Helpers;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class HorasExtraController : Controller
    {
        private readonly IHorasExtraLN _horasextraLN;
        private readonly IColaboradorLN _colaboradorLN;
        private readonly ICatalogosLN _catalogoLN;
        public HorasExtraController(IHorasExtraLN horasextraLN, IColaboradorLN colaboradorLN, ICatalogosLN catalogosLN)
        {
            _horasextraLN = horasextraLN;
            _colaboradorLN = colaboradorLN;
            _catalogoLN = catalogosLN;
        }


        [HttpGet]
        public IActionResult PanelHorasExtra()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var colaborador = _colaboradorLN.ObtenerColaboradorPorId(idColaborador);
            var historial = _horasextraLN.ListaHorasExtraPorColaborador(idColaborador);

            var modelo = new PanelHorasExtraViewModel
            {
                Colaborador = colaborador,
                HistorialHorasExtra = historial
            };

            return View(modelo);
        }


        public IActionResult HistorialHorasExtra()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            var lista = _horasextraLN.ListaHorasExtraPorColaborador(idColaborador);
            return View(lista);
        }


        //Solictar extras
        [HttpGet]
        public IActionResult SolicitarHorasExtra()
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            var modelo = new Horas_extra
            {
                fecha_solicitud = DateTime.Today,
                fecha_hora_extras = DateTime.Today,
            };

            return View(modelo);
        }

        [HttpPost]
        public IActionResult SolicitarHorasExtra(Horas_extra model)
        {
            int idColaborador = HttpContext.Session.GetInt32("IdColaborador") ?? 0;
            if (idColaborador == 0) return RedirectToAction("Login", "Login");

            if (model.cantidad_extras > 12)
            {
                ModelState.AddModelError("cantidad_extras", "No puede solicitar más de 12 horas extra.");
                return View(model);
            }

            model.colaborador_id_colaborador = idColaborador;
            model.fecha_solicitud = DateTime.Now;
            model.catalogo_estado_aprobacion_id_estado_aprobacion = 1; // Asignar estado "pendiente"
            model.periodo_nomina_idperiodo_nomina = 1; // Al inicio no se asigna a ningún periodo de nómina específico, eso lo hace el de planilla


            string resultado = _horasextraLN.SolicitarHorasExtra(model);

            if (!string.IsNullOrEmpty(resultado))
            {
                ModelState.AddModelError(string.Empty, resultado); // Mostrar mensaje
                return View(model); // Devuelve la vista con el mensaje
            }

            //_horasextraLN.SolicitarHorasExtra(model);

            TempData["Mensaje"] = "Horas extra solicitadas exitosamente.";
            return RedirectToAction("PanelHorasExtra");
        }



        //Aprobación de horas extra
        [HttpGet]
        public IActionResult AprobacionHorasExtra() // 1 = Pendiente por defecto
        {
            List<Horas_extra> Lista;

            if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_JEFES"))
            {
                Lista = _horasextraLN.ListaHorasExtraPorIdEstadoAprobacion(1);
            }
            else if (SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_PLANILLA"))
            {
                Lista = _horasextraLN.ListaHorasExtraPorIdEstadoAprobacion(2);
            }
            else
            {
                return RedirectToAction("PanelHorasExtra");
            }
            return View(Lista);
        }

        [HttpPost]
        public IActionResult ActualizarEstadosHorasExtra(List<int> ids, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("AprobacionHorasExtra");

            _horasextraLN.ActualizarEstadosHorasExtra(ids, nuevoEstado);
            return RedirectToAction("AprobacionHorasExtra");
        }




        //Ejecución para nómina
        [HttpGet]
        public IActionResult EjecutarHorasExtraNomina()
        {
            if (!SesionHelper.TienePermiso(HttpContext, "GESTIONAR_VACACIONES_PLANILLA"))
                return RedirectToAction("PanelHorasExtra");

            var vacaciones = _horasextraLN.ListaHorasExtraPorIdEstadoAprobacion(3);
            var periodos = _catalogoLN.ObtenerCatalogoPeriodoNomina();

            ViewBag.Periodos = new SelectList(periodos, "id_periodo_nomina", "nombre_periodo");
            return View(vacaciones);
        }

        [HttpPost]
        public IActionResult ActualizarPeriodoHorasExtra(List<int> ids, int idPeriodoSeleccionado, int nuevoEstado)
        {
            if (ids == null || !ids.Any())
                return RedirectToAction("EjecutarHorasExtraNomina");

            _horasextraLN.ActualizarEstadosYPeriodoHorasExtra(ids, nuevoEstado, idPeriodoSeleccionado);
            return RedirectToAction("EjecutarHorasExtraNomina");
        }
    }
}
