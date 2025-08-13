using Entidades;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailLN _emailLN;
        private readonly ICatalogosLN _catalogosLN;

        public EmailController(IEmailLN emailLN, ICatalogosLN catalogosLN)
        {
            _emailLN = emailLN;
            _catalogosLN = catalogosLN;
        }

        //*Agregar correo vista modal
        [HttpPost]
        public IActionResult AgregarEmail(EmailViewModel vm, string origen)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _emailLN.InsertarEmail(new Email
            {
                id_email = vm.id_email,
                email = vm.email,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = vm.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = vm.persona_id_persona
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";
            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalAgregarEmail(string idPersona)
        {
            var vm = new EmailViewModel
            {
                persona_id_persona = idPersona,
                TiposContacto = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList()
            };

            return PartialView("_ModalAgregarEmail", vm);
        }




        //*Editar correo vista modal
        [HttpPost]
        public IActionResult EditarEmail(EmailViewModel vm, string origen)
        {
            if (!ModelState.IsValid)
            {
                // si quisieras devolver errores y re–renderizar el partialView:
                // vm.TiposContacto = …;
                // return PartialView("_ModalEditarEmail", vm);

                // Para hacerlo simple con AJAX:
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _emailLN.ActualizarEmail(new Email
            {
                id_email = vm.id_email,
                email = vm.email,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = vm.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = vm.persona_id_persona
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";
            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalEditarEmail(int id)
        {
            var email = _emailLN.ObtenerEmailPorId(id);
            if (email == null) return NotFound();

            var vm = new EmailViewModel
            {
                id_email = email.id_email,
                email = email.email,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = email.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = email.persona_id_persona,
                TiposContacto = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList()
            };

            return PartialView("_ModalEditarEmail", vm);
        }


        //*Eliminar correo vista modal
        [HttpPost]
        public IActionResult EliminarEmail(EmailViewModel vm, string origen)
        {
            try
            {
                _emailLN.EliminarEmail(vm.id_email);

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

        public IActionResult ModalEliminarEmail(int id)
        {
            var email = _emailLN.ObtenerEmailPorId(id);
            if (email == null) return NotFound();

            var vm = new EmailViewModel
            {
                id_email = email.id_email,
                email = email.email,
                catalogo_tipo_contacto_id_catalogo_tipo_contacto = email.catalogo_tipo_contacto_id_catalogo_tipo_contacto,
                persona_id_persona = email.persona_id_persona
            };

            return PartialView("_ModalEliminarEmail", vm);
        }


    }
}
