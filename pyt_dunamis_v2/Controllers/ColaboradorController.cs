using Entidades;
using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class ColaboradorController : Controller
    {
        private readonly IColaboradorLN _colaboradorLN;
        private readonly ICatalogosLN _catalogosLN;

        public ColaboradorController(IColaboradorLN colaboradorLN, ICatalogosLN catalogosLN)
        {
            _colaboradorLN = colaboradorLN;
            _catalogosLN = catalogosLN;
        }

        //*Editar telefono vista modal
        [HttpPost]
        public IActionResult EditarColaborador(ColaboradorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _colaboradorLN.ActualizarColaborador(new Colaborador
            {
                id_colaborador = vm.id_colaborador,
                fecha_ingreso = vm.fecha_ingreso,
                persona_id_persona = vm.persona_id_persona,
                catalogo_perfil_puesto_id_perfil_puesto = vm.catalogo_perfil_puesto_id_perfil_puesto

            });

            var redirectUrl = Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona });
            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalEditarColaborador(int id)
        {
            var col = _colaboradorLN.ObtenerColaboradorPorId(id);
            if (col == null) return NotFound();

            var vm = new ColaboradorViewModel
            {
                id_colaborador = col.id_colaborador,
                fecha_ingreso = col.fecha_ingreso,
                catalogo_perfil_puesto_id_perfil_puesto = col.catalogo_perfil_puesto_id_perfil_puesto,
                persona_id_persona = col.persona_id_persona,
                PerfilesPuestos = _catalogosLN.ObtenerCatalogoPerfilPuesto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_perfil_puesto.ToString(),
                        Text = c.descripcion_puesto
                    }).ToList()
            };

            return PartialView("_ModalEditarColaborador", vm);
        }


        //*Eliminar colaborador vista modal
        [HttpPost]
        public IActionResult EliminarColaborador(Colaborador colaborador)
        {
            try
            {
                _colaboradorLN.EliminarColaborador(colaborador);

                var redirectUrl = Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = colaborador.persona_id_persona });
                return Json(new { success = true, redirectUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public IActionResult ModalEliminarColaborador(int id)
        {
            var col = _colaboradorLN.ObtenerColaboradorPorId(id);
            if (col == null) return NotFound();

            var vm = new ColaboradorViewModel
            {
                id_colaborador = col.id_colaborador,
                fecha_ingreso = col.fecha_ingreso,
                catalogo_perfil_puesto_id_perfil_puesto = col.catalogo_perfil_puesto_id_perfil_puesto,
                persona_id_persona = col.persona_id_persona,
            };

            return PartialView("_ModalEliminarColaborador", vm);
        }




    }
}
