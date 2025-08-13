using Entidades;
using Entidades.DTOs;
using LogicaNegocio.Interfaz;
using LogicaNegocio.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class PersonaController : Controller
    {
        private readonly IPersonaLN _personaLN;
        private readonly IEmailLN _emailLN;
        private readonly ITelefonoLN _telefonoLN;
        private readonly IDireccionLN _direccionLN;
        private readonly ICatalogosLN _catalogosLN;
        private readonly IColaboradorLN _colaboradorLN;
        private readonly IUsuarioLN _usuarioLN;

        private readonly IRegistroColaboradorServiceLN _registroColaboradorService;

        public PersonaController(
            IPersonaLN personaLN,
            IEmailLN emailLN,
            ITelefonoLN telefonoLN,
            IDireccionLN direccionLN,
            ICatalogosLN catalogosLN,
            IColaboradorLN colaboradorLN,

            IRegistroColaboradorServiceLN registroColaboradorService,
            IUsuarioLN usuarioLN)
        {
            _personaLN = personaLN;
            _emailLN = emailLN;
            _telefonoLN = telefonoLN;
            _direccionLN = direccionLN;
            _catalogosLN = catalogosLN;
            _colaboradorLN = colaboradorLN;

            _registroColaboradorService = registroColaboradorService;
            _usuarioLN = usuarioLN;
        }

        // GET: Persona
        public ActionResult ListaPersonas()
        {
            var personas = _personaLN.ObtenerPersonas();
            return View(personas);
        }


        public ActionResult ListaColaboradores()
        {
            // Obtener todos los colaboradores
            var listaColaboradores = _colaboradorLN.ObtenerColaboradores();

            // Lista para almacenar cada modelo completo
            var listaModelos = new List<PersonaCompletaViewModel>();

            foreach (var colaborador in listaColaboradores)
            {
                // Suponemos que cada colaborador tiene una propiedad IdPersona
                string idPersona = colaborador.persona_id_persona;
                int idPuesto = colaborador.catalogo_perfil_puesto_id_perfil_puesto;

                var modelo = new PersonaCompletaViewModel
                {
                    Colaborador = colaborador,
                    Persona = _personaLN.ObtenerPersonaPorId(idPersona),
                    Email = _emailLN.ObtenerEmailsPorPersona(idPersona),
                    Telefono = _telefonoLN.ObtenerTelefonosPorPersona(idPersona),
                    Direccion = _direccionLN.ObtenerDireccionCompletaPorPersona(idPersona),
                    Puesto = _catalogosLN.ObtenerCatalogoPuestosXId(idPuesto)
                };

                listaModelos.Add(modelo);
            }

            // Retornar la vista con la lista de modelos
            return View("ListaColaboradores", listaModelos);
        }


        public ActionResult BuscarPersona(string criterio, string valor)
        {
            var resultados = _personaLN.BuscarPersona(criterio, valor);
            return View(resultados);
        }


        public ActionResult ObtenerPersonaCompleta(string idPersona)
        {
            if (idPersona == null)

                return RedirectToAction("ListaPersonas");


            var modelo = new PersonaCompletaViewModel
            {
                Persona = _personaLN.ObtenerPersonaPorId(idPersona),
                Email = _emailLN.ObtenerEmailsPorPersona(idPersona),
                Telefono = _telefonoLN.ObtenerTelefonosPorPersona(idPersona),
                Direccion = _direccionLN.ObtenerDireccionCompletaPorPersona(idPersona)
            };

            return View("ObtenerPersonaCompleta", modelo);
        }


        // Obtener colaborador por ID y mostrar su información completa
        [HttpGet]
        public ActionResult ObtenerColaboradorDatosCompletos(string idPersona)
        {
            if (idPersona == null)

                return RedirectToAction("ListaColaboradores");




            var colaborador = _colaboradorLN.ObtenerColaboradorPorIdPersona(idPersona);

            bool tieneUsuario = false;
            if (colaborador != null)
            {
                tieneUsuario = _usuarioLN.ColaboradorTieneUsuario(colaborador.id_colaborador);
            }




            var modelo = new PersonaCompletaViewModel
            {
                Persona = _personaLN.ObtenerPersonaPorId(idPersona),
                Email = _emailLN.ObtenerEmailsPorPersona(idPersona),
                Telefono = _telefonoLN.ObtenerTelefonosPorPersona(idPersona),
                Direccion = _direccionLN.ObtenerDireccionCompletaPorPersona(idPersona),
                Colaborador = _colaboradorLN.ObtenerColaboradorPorIdPersona(idPersona),
                ColaboradorYaTieneUsuario = tieneUsuario
            };

            return View("ObtenerColaboradorDatosCompletos", modelo);
        }


        /*
        public ActionResult ObtenerColaborador(int idColaborador)
        {
            if (idColaborador == 0)

                return RedirectToAction("ListaColaboradores");


            var colaborador = _colaboradorLN.ObtenerColaboradorPorId(idColaborador);

            if (colaborador == null || colaborador.persona_id_persona == 0)
                return RedirectToAction("ListaColaboradores");

            int idPersona = colaborador.persona_id_persona;


            var modelo = new PersonaCompletaViewModel
            {
                Colaborador = colaborador,
                Persona = _personaLN.ObtenerPersonaPorId(idPersona),
                Email = _emailLN.ObtenerEmailsPorPersona(idPersona),
                Telefono = _telefonoLN.ObtenerTelefonosPorPersona(idPersona),
                Direccion = _direccionLN.ObtenerDireccionCompletaPorPersona(idPersona)
                
            };

            return View("ObtenerPersonaCompleta", modelo);
        }*/








        public ActionResult InsertarColaborador()
        {
            var model = new PersonaCompletaViewModel
            {
                Persona = new Persona
                {
                    fecha_nacimiento = DateTime.Today.AddYears(-18) // Fecha mínima para evitar errores
                },
                Telefono = new List<Telefono> { new Telefono() },
                Email = new List<Email> { new Email() },
                Direccion = new List<Direccion> { new Direccion() },
                Colaborador = new Colaborador
                {
                    fecha_ingreso = DateTime.Today
                },

                
            };

            CargarListasDesplegables(model); // Nuevo método abajo 👇

            return View(model); // Vista normal: Views/Persona/InsertarPersonaCompleta.cshtml
        }

        // POST: Insertar la persona con sus datos relacionados
        [HttpPost]
        public IActionResult InsertarColaborador(PersonaCompletaViewModel vm)
        {
            if (vm.Persona.id_persona == null || string.IsNullOrWhiteSpace(vm.Persona.nombre))
            {
                ModelState.AddModelError("", "Faltan datos obligatorios.");
                CargarListasDesplegables(vm);
                return View(vm);
            }

            if (_personaLN.ExistePersonaPorId(vm.Persona.id_persona))
            {
                ViewBag.PersonaDuplicada = "La persona ya se encuentra registrada en el sistema.";
                CargarListasDesplegables(vm);
                return View(vm);
            }


            try
            {
                // Mapear el ViewModel al DTO
                var dto = new PersonaCompletaDTO
                {

                    Persona = vm.Persona,
                    Colaborador = vm.Colaborador,
                    Emails = vm.Email,
                    Telefonos = vm.Telefono,
                    Direcciones = vm.Direccion
                };

                // Usar el servicio
                _registroColaboradorService.InsertarColaborador(dto);

                return RedirectToAction("ListaColaboradores");
           }
           catch (Exception ex)
           {
                Console.WriteLine("Error en InsertarPersonaCompleta: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                CargarListasDesplegables(vm);
                return View(vm);
           }
        }









        //Actualizar persona
        [HttpPost]
        public IActionResult EditarPersona(Persona persona)
        {
            try
            {
                _personaLN.ActualizarPersona(persona);
                return RedirectToAction("ObtenerPersonaCompleta", new { idPersona = persona.id_persona });
            }
            catch (Exception)
            {
                // Podés loguear el error o mostrar un mensaje personalizado
                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar la persona.");
                return PartialView("_ModalEditarPersona", persona);
            }
        }

        public IActionResult ModalEditarPersona(string id)
        {
            var persona = _personaLN.ObtenerPersonaPorId(id);
            if (persona == null)
            {
                return NotFound();
            }
            return PartialView("_ModalEditarPersona", persona);
        }



        //Eliminar persona
        [HttpPost]
        public IActionResult EliminarPersona(Persona persona)
        {
            try
            {
                _personaLN.EliminarPersona(persona);
                return RedirectToAction("ObtenerPersonaCompleta", new { idPersona = persona.id_persona });
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar la persona. Intente de nuevo.");
                return PartialView("_ModalEliminarPersona", persona);
            }
        }

        public IActionResult ModalEliminarPersona(string id)
        {
            var persona = _personaLN.ObtenerPersonaPorId(id);
            if (persona == null)
            {
                return NotFound();
            }
            return PartialView("_ModalEliminarPersona", persona);
        }












        // GET: Mostrar vista con todos los datos cargados
        public ActionResult InsertarPersonaCompleta()
        {
            var provincias = _catalogosLN.ObtenerCatalogo_Provincias()
                .Select(p => new SelectListItem
                {
                    Value = p.id_provincia.ToString(),
                    Text = p.descripcion_provincia
                }).ToList();

            // Inserta la opción "Seleccionar"
            provincias.Insert(0, new SelectListItem { Value = "", Text = "Seleccionar provincia --" });

            var model = new PersonaCompletaViewModel
            {
                Persona = new Persona
                {
                    fecha_nacimiento = DateTime.Today
                },
                Telefono = new List<Telefono> { new Telefono() },
                Email = new List<Email> { new Email() },
                Direccion = new List<Direccion> { new Direccion() },

                TiposTelefono = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList(),

                TiposEmail = _catalogosLN.ObtenerCatalogoContacto()
                    .Select(c => new SelectListItem
                    {
                        Value = c.id_catalogo_tipo_contacto.ToString(),
                        Text = c.descripcion_tipo_contacto
                    }).ToList(),


                Provincias = provincias, // Asignar la lista de provincias
                Cantones = new List<SelectListItem>(), // Se cargan por JS al seleccionar provincia
                Distritos = new List<SelectListItem>(),

                TiposDireccion = _catalogosLN.ObtenerCatalogoDireccion()
                    .Select(td => new SelectListItem
                    {
                        Value = td.id_tipo_direccion.ToString(),
                        Text = td.descripcion_tipo_direccion
                    }).ToList()
            };

            return View(model); // Vista normal: Views/Persona/InsertarPersonaCompleta.cshtml
        }

        // POST: Insertar la persona con sus datos relacionados
        [HttpPost]
        public IActionResult InsertarPersonaCompleta(PersonaCompletaViewModel vm)
        {
            try
            {
                _personaLN.InsertarPersona(vm.Persona);
                string personaId = vm.Persona.id_persona;

                if (vm.Email != null)
                {
                    foreach (var email in vm.Email)
                    {
                        email.persona_id_persona = personaId;
                        _emailLN.InsertarEmail(email);
                    }
                }

                if (vm.Telefono != null)
                {
                    foreach (var tel in vm.Telefono)
                    {
                        tel.persona_id_persona = personaId;
                        _telefonoLN.InsertarTelefono(tel);
                    }
                }

                if (vm.Direccion != null)
                {
                    foreach (var dir in vm.Direccion)
                    {
                        dir.persona_id_persona = personaId;
                        _direccionLN.InsertarDireccion(dir);
                    }
                }

                return RedirectToAction("ListaPersonas");
            }
            catch (Exception)
            {
                throw;
            }
        }















        /***----------------------------MÉTODOS------------------------------------***///

        private void CargarListasDesplegables(PersonaCompletaViewModel model)
        {
            model.Provincias = _catalogosLN.ObtenerCatalogo_Provincias()
                .Select(p => new SelectListItem
                {
                    Value = p.id_provincia.ToString(),
                    Text = p.descripcion_provincia
                }).ToList();
            model.Provincias.Insert(0, new SelectListItem { Value = "", Text = "Seleccionar provincia --" });

            model.PerfilesPuestos = _catalogosLN.ObtenerCatalogoPerfilPuesto()
                .Select(p => new SelectListItem
                {
                    Value = p.id_perfil_puesto.ToString(),
                    Text = p.descripcion_puesto
                }).ToList();
            model.PerfilesPuestos.Insert(0, new SelectListItem { Value = "", Text = "Seleccionar puesto --" });

            model.TiposTelefono = _catalogosLN.ObtenerCatalogoContacto()
                .Select(c => new SelectListItem
                {
                    Value = c.id_catalogo_tipo_contacto.ToString(),
                    Text = c.descripcion_tipo_contacto
                }).ToList();

            model.TiposEmail = model.TiposTelefono; // Reutilizamos misma lista si aplica

            model.TiposDireccion = _catalogosLN.ObtenerCatalogoDireccion()
                .Select(td => new SelectListItem
                {
                    Value = td.id_tipo_direccion.ToString(),
                    Text = td.descripcion_tipo_direccion
                }).ToList();

            model.Cantones = new List<SelectListItem>(); // Se cargan por JS
            model.Distritos = new List<SelectListItem>();
        }

    }
}
