using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace pyt_dunamis_v2.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly ICatalogosLN _catalogosLN;

        public CatalogosController(ICatalogosLN catalogosLN)
        {
            _catalogosLN = catalogosLN;
        }

        [HttpGet]
        public IActionResult ObtenerCantones(int idProvincia)
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
        public IActionResult ObtenerDistritos(int idCanton)
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
