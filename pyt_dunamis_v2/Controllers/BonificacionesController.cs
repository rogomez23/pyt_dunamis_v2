using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;
namespace pyt_dunamis_v2.Controllers
{
    public class BonificacionesController : Controller
    {

        private readonly IBonificacionPuestoLN _bonificacionPuestoLN;
        private readonly ICatalogosLN _catalogoLN;
        private readonly IBonificacionesLN _bonificacionesLN;
        public BonificacionesController(IBonificacionPuestoLN bonificacionPuestoLN, ICatalogosLN catalogosLN, IBonificacionesLN bonificacionesLN)
        {
            _bonificacionPuestoLN = bonificacionPuestoLN;
            _catalogoLN = catalogosLN;
            _bonificacionesLN = bonificacionesLN;
        }

        [HttpGet]
        public IActionResult BonificacionesPuesto()
        {
            var puestos = _bonificacionPuestoLN.ObtenerListaBonificacionPuesto();
            var bonifs = _catalogoLN.ObtenerCatalogoBonificacion();

            var vm = new BonificacionPuestoViewModel
            {
                Puestos = puestos
                    .Select(p => new SelectListItem(p.descripcion_puesto,
                                                    p.id_perfil_puesto.ToString()))
                    .ToList(),
                Bonificaciones = bonifs
                    .Select(b => new SelectListItem(b.descripcion_bonificacion,
                                                    b.id_catalogo_bonificacion.ToString()))
                    .ToList(),
                PuestosConBon = puestos
                    .Where(p => p.Bonificacion_puesto.Any())
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Asignar(BonificacionPuestoViewModel vm)
        {
            // 1) Valida que se haya seleccionado algo
            if (vm.SelectedPuesto <= 0 || vm.SelectedBonificacion <= 0)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar puesto y bonificación.");
            }
            else
            {
                // 2) Comprueba duplicado
                var puesto = _bonificacionPuestoLN.ObtenerBonificacionPuestoPorId(vm.SelectedPuesto);
                bool yaExiste = puesto.Bonificacion_puesto
                                     .Any(bp => bp.catalogo_bonificaciones_id_catalogo_bonificacion
                                                == vm.SelectedBonificacion);
                if (yaExiste)
                {
                    ModelState.AddModelError(string.Empty,
                        "Esa bonificación ya está asignada al puesto seleccionado.");
                }
                else
                {
                    // 3) Si todo bien, asigna y redirige
                    _bonificacionPuestoLN.AsignarBonificacion(vm.SelectedPuesto, vm.SelectedBonificacion);
                    return RedirectToAction(nameof(BonificacionesPuesto));
                }
            }

            // 4) Si hubo errores, recarga los dropdowns y la tabla
            var puestos = _bonificacionPuestoLN.ObtenerListaBonificacionPuesto();
            var bonifs = _catalogoLN.ObtenerCatalogoBonificacion();
            vm.Puestos = puestos
                .Select(p => new SelectListItem(p.descripcion_puesto, p.id_perfil_puesto.ToString()))
                .ToList();
            vm.Bonificaciones = bonifs
                .Select(b => new SelectListItem(b.descripcion_bonificacion, b.id_catalogo_bonificacion.ToString()))
                .ToList();
            vm.PuestosConBon = puestos.Where(p => p.Bonificacion_puesto.Any());

            return View(nameof(BonificacionesPuesto), vm);
        }

        [HttpPost]
        public IActionResult Quitar(BonificacionPuestoViewModel vm)
        {
            // 1) Validar selección
            if (vm.SelectedPuesto <= 0 || vm.SelectedBonificacion <= 0)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar puesto y bonificación.");
            }
            else
            {
                // 2) Recuperar el puesto y ver si existe la bonificación allí
                var puesto = _bonificacionPuestoLN.ObtenerBonificacionPuestoPorId(vm.SelectedPuesto);
                bool existeRelacion = puesto.Bonificacion_puesto
                                          .Any(bp => bp.catalogo_bonificaciones_id_catalogo_bonificacion
                                                     == vm.SelectedBonificacion);
                if (!existeRelacion)
                {
                    // No existía: informar
                    ModelState.AddModelError(string.Empty,
                        "Esa bonificación no está asignada al puesto seleccionado, no hay nada que quitar.");
                }
                else
                {
                    // 3) Si existe, quitar y redirigir
                    _bonificacionPuestoLN.QuitarBonificacion(vm.SelectedPuesto, vm.SelectedBonificacion);
                    return RedirectToAction(nameof(BonificacionesPuesto));
                }
            }

            // 4) Si hay errores, recargar datos para la vista
            var puestos = _bonificacionPuestoLN.ObtenerListaBonificacionPuesto();
            var bonifs = _catalogoLN.ObtenerCatalogoBonificacion();
            vm.Puestos = puestos
                .Select(p => new SelectListItem(p.descripcion_puesto,
                                                p.id_perfil_puesto.ToString()))
                .ToList();
            vm.Bonificaciones = bonifs
                .Select(b => new SelectListItem(b.descripcion_bonificacion,
                                                b.id_catalogo_bonificacion.ToString()))
                .ToList();
            vm.PuestosConBon = puestos
                .Where(p => p.Bonificacion_puesto.Any());

            // 5) Volver a la misma vista para mostrar errores
            return View(nameof(BonificacionesPuesto), vm);
        }


        //Calcular Bonificaciones
        [HttpGet]
        public IActionResult CalcularBonificaciones()
        {

            var preview = _bonificacionesLN.ObtenerBonificacionesPreview();
            return View(preview);
        }

        [HttpPost]
        public IActionResult CalcularBonificaciones(int[] seleccionados)
        {
            const int periodoId = 1; 
            const int estadoAprobado = 1; 

            // 1) Recalcular el preview siempre
            var allPreview = _bonificacionesLN.ObtenerBonificacionesPreview();

            // 2) Si no marcó nada, devolvemos la vista con error y dropdown
            if (seleccionados == null || seleccionados.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar al menos una bonificación.");

                return View(allPreview);
            }

            // 3) Si sí marcó, filtra y guarda
            var toSave = allPreview
                         .Where((p, idx) => seleccionados.Contains(idx))
                         .ToList();

            _bonificacionesLN.GuardarBonificaciones(toSave, periodoId, estadoAprobado);

            // 4) Redirige a la lista principal
            return RedirectToAction(nameof(BonificacionesPuesto));
        }





        //Aprobacion de las bonificaciones por periodo de nómina

        [HttpGet]
        public IActionResult AprobarBonificaciones()
        {
            const int estadoPendiente = 1;

            ViewBag.Estados = _catalogoLN.ObtenerCatalogoEstadoAprobacion()
                .Select(e => new SelectListItem(e.descripcion_estado, e.id_estado_aprobacion.ToString()))
                .ToList();

            // 2) Preview desde BD
            var preview = _bonificacionesLN.ObtenerBonificacionesParaProcesar(estadoPendiente);
            return View(preview);
        }

        [HttpPost]
        public IActionResult AprobarBonificaciones(int nuevoEstado, int[] seleccionados)
        {
            // Recargar dropdowns
            ViewBag.Estados = _catalogoLN.ObtenerCatalogoEstadoAprobacion()
                .Select(e => new SelectListItem(e.descripcion_estado, e.id_estado_aprobacion.ToString()))
                .ToList();

            if (seleccionados == null || !seleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos una fila.");
                var previewErr = _bonificacionesLN.ObtenerBonificacionesParaProcesar(3);
                return View(previewErr);
            }

            _bonificacionesLN.ActualizarEstadoBonificaciones(seleccionados.ToList(), nuevoEstado);
            return RedirectToAction(nameof(BonificacionesPuesto));
        }




        //Procesar a Nómina
        [HttpGet]
        public IActionResult EjecutarBonificacionesNomina()
        {
            const int estadoPendiente = 3; // “Aprobado por planilla” u “Obtener las que quieras procesar”

            var periodos = _catalogoLN.ObtenerCatalogoPeriodoNomina();

            ViewBag.Periodos = new SelectList(periodos, "id_periodo_nomina", "nombre_periodo");

            // 2) Preview desde BD
            var preview = _bonificacionesLN.ObtenerBonificacionesParaProcesar(estadoPendiente);
            return View(preview);
        }

        [HttpPost]
        public IActionResult EjecutarBonificacionesNomina(
            int idPeriodoSeleccionado,
            int[] seleccionados)
        {
            // Recargar dropdowns
            var periodos = _catalogoLN.ObtenerCatalogoPeriodoNomina();

            ViewBag.Periodos = new SelectList(periodos, "id_periodo_nomina", "nombre_periodo");

            if (seleccionados == null || !seleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos una fila.");
                var previewErr = _bonificacionesLN.ObtenerBonificacionesParaProcesar(3);
                return View(previewErr);
            }

            _bonificacionesLN.ActualizarPeriodoBonificaciones(seleccionados.ToList(), idPeriodoSeleccionado);
            return RedirectToAction(nameof(BonificacionesPuesto));
        }


    }
}
