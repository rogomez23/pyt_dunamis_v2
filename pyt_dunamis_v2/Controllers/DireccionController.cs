using Entidades;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class DireccionController : Controller
    {
        private readonly IDireccionLN _direccionLN;
        private readonly ICatalogosLN _catalogosLN;

        public DireccionController(IDireccionLN direccionLN, ICatalogosLN catalogosLN)
        {
            _direccionLN = direccionLN;
            _catalogosLN = catalogosLN;
        }

        //*Agregar correo vista modal
        [HttpPost]
        public IActionResult AgregarDireccion(DireccionViewModel vm, string origen)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _direccionLN.InsertarDireccion(new Direccion
            {
                id_direccion = vm.id_direccion,
                puntos_referencia = vm.puntos_referencia,
                catalogo_tipo_direccion_id_tipo_direccion = vm.catalogo_tipo_direccion_id_tipo_direccion,
                persona_id_persona = vm.persona_id_persona,
                catalogo_distrito_id_distrito = vm.catalogo_distrito_id_distrito,
                catalogo_canton_id_canton = vm.catalogo_canton_id_canton,
                catalogo_provincia_id_provincia = vm.catalogo_provincia_id_provincia
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";
            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalAgregarDireccion(string idPersona)
        {

            var provincias = _catalogosLN.ObtenerCatalogo_Provincias()
                    .Select(p => new SelectListItem
                    {
                        Value = p.id_provincia.ToString(),
                        Text = p.descripcion_provincia
                    }).ToList();

            provincias.Insert(0, new SelectListItem { Value = "", Text = "Seleccionar provincia --" });

            var vm = new DireccionViewModel
            {
                persona_id_persona = idPersona,
                TipoDireccion = _catalogosLN.ObtenerCatalogoDireccion()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_tipo_direccion.ToString(),
                        Text = c.descripcion_tipo_direccion
                    }).ToList(),
                Provincias = provincias,
                Cantones = new List<SelectListItem>(),
                Distritos = new List<SelectListItem>()
            };

            return PartialView("_ModalAgregarDireccion", vm);
        }




        //*Editar direccion vista modal
        [HttpPost]
        public IActionResult EditarDireccion(DireccionViewModel vm, string origen)
        {
            if (!ModelState.IsValid)
            {

                return Json(new { success = false, errors = ModelState });
            }

            // guardas
            _direccionLN.ActualizarDireccion(new Direccion
            {
                id_direccion = vm.id_direccion,
                puntos_referencia = vm.puntos_referencia,
                catalogo_tipo_direccion_id_tipo_direccion = vm.catalogo_tipo_direccion_id_tipo_direccion,
                persona_id_persona = vm.persona_id_persona,
                catalogo_distrito_id_distrito = vm.catalogo_distrito_id_distrito,
                catalogo_canton_id_canton = vm.catalogo_canton_id_canton,
                catalogo_provincia_id_provincia = vm.catalogo_provincia_id_provincia
            });

            string redirectUrl = origen == "colaborador"
                ? Url.Action("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = vm.persona_id_persona }) ?? ""
                : Url.Action("ObtenerPersonaCompleta", "Persona", new { idPersona = vm.persona_id_persona }) ?? "";
            return Json(new { success = true, redirectUrl });
        }

        public IActionResult ModalEditarDireccion(int id)
        {
            var direcc = _direccionLN.ObtenerDireccionPorId(id);
            if (direcc == null) return NotFound();

            var provincias = _catalogosLN.ObtenerCatalogo_Provincias()
                .Select(p => new SelectListItem
                {
                    Value = p.id_provincia.ToString(),
                    Text = p.descripcion_provincia
                }).ToList();

            provincias.Insert(0, new SelectListItem { Value = "", Text = "Seleccionar provincia --" });

            var vm = new DireccionViewModel
            {
                id_direccion = direcc.id_direccion,
                puntos_referencia = direcc.puntos_referencia,
                catalogo_tipo_direccion_id_tipo_direccion = direcc.catalogo_tipo_direccion_id_tipo_direccion,
                persona_id_persona = direcc.persona_id_persona,
                catalogo_distrito_id_distrito = 0, // obligar a seleccionar de nuevo
                catalogo_canton_id_canton = 0,
                catalogo_provincia_id_provincia = 0,
                Provincias = provincias,
                Cantones = new List<SelectListItem>(),
                Distritos = new List<SelectListItem>(),
                TipoDireccion = _catalogosLN.ObtenerCatalogoDireccion()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_tipo_direccion.ToString(),
                        Text = c.descripcion_tipo_direccion
                    }).ToList()
            };

            return PartialView("_ModalEditarDireccion", vm);
        }


        //*Eliminar direccion vista modal
        [HttpPost]
        public IActionResult EliminarDireccion(DireccionViewModel vm, string origen)
        {
            try
            {
                _direccionLN.EliminarDireccion(vm.id_direccion);

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

        public IActionResult ModalEliminarDireccion(int id)
        {
            var direcc = _direccionLN.ObtenerDireccionPorId(id);
            if (direcc == null) return NotFound();

            var vm = new DireccionViewModel
            {
                id_direccion = direcc.id_direccion,
                puntos_referencia = direcc.puntos_referencia,
                catalogo_tipo_direccion_id_tipo_direccion = direcc.catalogo_tipo_direccion_id_tipo_direccion,
                persona_id_persona = direcc.persona_id_persona,
                Provincias = _catalogosLN.ObtenerCatalogo_Provincias()
                    .Select(p => new SelectListItem { Value = p.id_provincia.ToString(), Text = p.descripcion_provincia }).ToList(),

                Cantones = _catalogosLN.ObtenerCatalogo_Cantones(direcc.catalogo_provincia_id_provincia)
                    .Select(c => new SelectListItem { Value = c.id_canton.ToString(), Text = c.descripcion_canton }).ToList(),

                Distritos = _catalogosLN.ObtenerCatalogo_Distritos(direcc.catalogo_canton_id_canton)
                    .Select(d => new SelectListItem { Value = d.id_distrito.ToString(), Text = d.descripcion_distrito }).ToList(),
            };

            return PartialView("_ModalEliminarDireccion", vm);
        }





        //**Obtener provincias, cantones y distritos para los dropdowns

        [HttpGet]
        public IActionResult ObtenerCantonesPorProvincia(int idProvincia)
        {
            var cantones = _catalogosLN.ObtenerCatalogo_Cantones(idProvincia)
                .Select(c => new SelectListItem
                {
                    Value = c.id_canton.ToString(),
                    Text = c.descripcion_canton
                }).ToList();

            return Json(cantones);
        }

        [HttpGet]
        public IActionResult ObtenerDistritosPorCanton(int idCanton)
        {
            var distritos = _catalogosLN.ObtenerCatalogo_Distritos(idCanton)
                .Select(d => new SelectListItem
                {
                    Value = d.id_distrito.ToString(),
                    Text = d.descripcion_distrito
                }).ToList();

            return Json(distritos);
        }
    }
}
