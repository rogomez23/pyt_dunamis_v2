using Entidades;
using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Helpers;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class ReportesController : Controller
    {

        private readonly IOrdenesLN _ordenesLN;
        private readonly ICatalogosLN _catalogoLN;

        public ReportesController(IOrdenesLN ordenesLN, ICatalogosLN catalogoLN)
        {
            _ordenesLN = ordenesLN;
            _catalogoLN = catalogoLN;
        }

        [HttpGet]
        public IActionResult ReportesOrdenes(int? idestado, DateTime? fecha_visita)
        {
            // Obtener lista de estados para el filtro
            var estados = _catalogoLN.ObtenerCatalogoEstadoOrden();

            // Obtener lista de órdenes con filtro
            var lista = _ordenesLN.ObtenerTodasOrdenes();

            if (idestado.HasValue)
            {
                lista = lista.Where(o => o.estado_orden_id_estado_orden == idestado.Value).ToList();
            }

            if (fecha_visita.HasValue)
            {
                lista = lista.Where(o => o.fecha_visita.Date == fecha_visita.Value.Date).ToList();
            }

            var vm = new OrdenesViewModel
            {
                HistorialOrdenes = lista,
                TipoEstadoOrden = estados.Select(e => new SelectListItem
                {
                    Value = e.id_estado_orden.ToString(),
                    Text = e.descripcion_estado,
                    Selected = (idestado.HasValue && e.id_estado_orden == idestado.Value)
                }).ToList(),
                fecha_visita = fecha_visita ?? default(DateTime)
            };

            return View(vm);
        }

        public ActionResult ReportesEmpleados()
        {
            return View();
        }

        public ActionResult ReportesSalarios()
        {
            return View();
        }

        public ActionResult ReportesVacaciones()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: ReportesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
