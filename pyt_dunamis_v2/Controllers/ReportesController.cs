using Entidades;
using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Helpers;
using pyt_dunamis_v2.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
        public IActionResult ReportesOrdenes(int? idestado, DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Obtener lista de estados para el filtro
            var estados = _catalogoLN.ObtenerCatalogoEstadoOrden();

            // Obtener lista de órdenes con filtro
            var lista = _ordenesLN.ObtenerTodasOrdenes();

            if (idestado.HasValue)
            {
                lista = lista.Where(o => o.estado_orden_id_estado_orden == idestado.Value).ToList();
            }

            if (fechaInicio.HasValue)
            {
                lista = lista.Where(o => o.fecha_visita.Date >= fechaInicio.Value.Date).ToList();
            }

            if (fechaFin.HasValue)
            {
                lista = lista.Where(o => o.fecha_visita.Date <= fechaFin.Value.Date).ToList();
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
                fechaInicio = fechaInicio,
                fechaFin = fechaFin
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult ReportesOrdenesPdf(int? idestado, DateTime? fechaInicio, DateTime? fechaFin)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var lista = _ordenesLN.ObtenerTodasOrdenes();

            if (idestado.HasValue)
            {
                lista = lista.Where(o => o.estado_orden_id_estado_orden == idestado.Value).ToList();
            }

            if (fechaInicio.HasValue)
            {
                lista = lista.Where(o => o.fecha_visita.Date >= fechaInicio.Value.Date).ToList();
            }

            if (fechaFin.HasValue)
            {
                lista = lista.Where(o => o.fecha_visita.Date <= fechaFin.Value.Date).ToList();
            }

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text("Reporte de órdenes").SemiBold().FontSize(20);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("N°");
                            header.Cell().Element(CellStyle).Text("Contrato");
                            header.Cell().Element(CellStyle).Text("Tipo");
                            header.Cell().Element(CellStyle).Text("Fecha");
                            header.Cell().Element(CellStyle).Text("Detalle");
                            header.Cell().Element(CellStyle).Text("Estado");
                            header.Cell().Element(CellStyle).Text("Colaborador");
                            header.Cell().Element(CellStyle).Text("Puesto");
                        });

                        foreach (var o in lista)
                        {
                            table.Cell().Element(CellStyle).Text(o.id_orden.ToString());
                            table.Cell().Element(CellStyle).Text(o.contrato_cliente);
                            table.Cell().Element(CellStyle).Text(o.tipo_trabajo);
                            table.Cell().Element(CellStyle).Text(o.fecha_visita.ToShortDateString());
                            table.Cell().Element(CellStyle).Text(o.descripcion_trabajo);
                            table.Cell().Element(CellStyle).Text(o.Estado_orden?.descripcion_estado ?? string.Empty);
                            table.Cell().Element(CellStyle).Text(o.Colaborador.Persona.nombre + " " + o.Colaborador.Persona.apellido_1);
                            table.Cell().Element(CellStyle).Text(o.Colaborador.Catalogo_perfil_puesto.descripcion_puesto);
                        }
                    });
                });
            });

            byte[] pdf = document.GeneratePdf();
            return File(pdf, "application/pdf", "ReporteOrdenes.pdf");

            static IContainer CellStyle(IContainer container)
            {
                return container.Padding(5).Border(1);
            }
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
