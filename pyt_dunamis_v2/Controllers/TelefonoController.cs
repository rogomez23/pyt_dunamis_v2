using Entidades;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class TelefonoController : Controller
    {
        private readonly ITelefonoLN _telefonoLN;
        private readonly ICatalogosLN _catalogosLN;

        public TelefonoController(ITelefonoLN telefonoLN, ICatalogosLN catalogosLN)
        {
            _telefonoLN = telefonoLN;
            _catalogosLN = catalogosLN;
        }

        //*Agregar telefono vista modal
        [HttpPost]
        public IActionResult AgregarTelefono(TelefonoViewModel vm , string origen)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _telefonoLN.InsertarTelefono(new Telefono
            {
                id_telefono = vm.id_telefono,
                numero = vm.numero,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = vm.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = vm.persona_id_persona
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";

            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalAgregarTelefono(string idPersona)
        {
            var vm = new TelefonoViewModel
            {
                persona_id_persona = idPersona,
                TiposContacto = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList()
            };

            return PartialView("_ModalAgregarTelefono", vm);
        }




        //*Editar telefono vista modal
        [HttpPost]
        public IActionResult EditarTelefono(TelefonoViewModel vm, string origen)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _telefonoLN.ActualizarTelefono(new Telefono
            {
                id_telefono = vm.id_telefono,
                numero = vm.numero,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = vm.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = vm.persona_id_persona
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";

            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalEditarTelefono(int id)
        {
            var email = _telefonoLN.ObtenerTelefonoPorId(id);
            if (email == null) return NotFound();

            var vm = new TelefonoViewModel
            {
                id_telefono = email.id_telefono,
                numero = email.numero,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = email.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = email.persona_id_persona,
                TiposContacto = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList()
            };

            return PartialView("_ModalEditarTelefono", vm);
        }


        //*Eliminar telefono vista modal
        [HttpPost]
        public IActionResult EliminarTelefono(TelefonoViewModel vm, string origen)
        {
            try
            {
                _telefonoLN.EliminarTelefono(vm.id_telefono);

                string redirectUrl = origen == "colaborador"
                    ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                    : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";

                return Json(new { success = true, redirectUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public IActionResult ModalEliminarTelefono(int id)
        {
            var email = _telefonoLN.ObtenerTelefonoPorId(id);
            if (email == null) return NotFound();

            var vm = new TelefonoViewModel
            {
                id_telefono = email.id_telefono,
                numero = email.numero,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = email.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = email.persona_id_persona
            };

            return PartialView("_ModalEliminarTelefono", vm);
        }
    }
}
