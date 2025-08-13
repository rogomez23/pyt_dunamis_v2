using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LogicaNegocio.Interfaz;
using LogicaNegocio.Implementacion;
using pyt_dunamis_v2.Models;

namespace pyt_dunamis_v2.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioLN _usuarioLN;
        private readonly IRolesLN _rolesLN;
        private readonly IColaboradorLN _colaboradorLN;

        public UsuarioController(IUsuarioLN usuarioLN, IRolesLN rolesLN, IColaboradorLN colaboradorLN)
        {
            _usuarioLN = usuarioLN;
            _rolesLN = rolesLN;
            _colaboradorLN = colaboradorLN;
        }

        /*--------------Lista de usuarios--------------*/

        public ActionResult ListaUsuarios()
        {
            var lista = _usuarioLN.ObtenerTodos();
            return View(lista);
        }



        /*--------------Crear usuario--------------*/
        public IActionResult Crear(int colaboradorId, string nombre, string apellido1, string apellido2, string idpersona)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido1))
            {
                return BadRequest("Faltan datos necesarios para crear el nombre de usuario.");
            }

            // Generar nombre de usuario automáticamente
            string baseUsername = (nombre.FirstOrDefault().ToString().ToLower() ?? "")
                                + (apellido1?.ToLower() ?? "")
                                + (apellido2?.FirstOrDefault().ToString().ToLower() ?? "");

            // Buscar si ya existe un usuario con ese nombre
            string finalUsername = baseUsername;
            int contador = 1;
            while (_usuarioLN.ExisteNombreUsuario(finalUsername))
            {
                finalUsername = $"{baseUsername}_{contador}";
                contador++;
            }

            var vm = new UsuarioViewModel
            {
                id_persona = idpersona,
                colaborador_id_colaborador = colaboradorId,
                nombre_usuario = finalUsername, // Ya viene cargado en la vista
                RolesDisponibles = _rolesLN.ObtenerTodos()
                    .Select(r => new SelectListItem { Value = r.id_rol.ToString(), Text = r.nombre_rol })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Crear(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.RolesDisponibles = _rolesLN.ObtenerTodos()
                    .Select(r => new SelectListItem { Value = r.id_rol.ToString(), Text = r.nombre_rol })
                    .ToList();
                return View(vm);
            }

            try
            {
                var usuario = new Usuarios
                {
                    nombre_usuario = vm.nombre_usuario,
                    inactivo = vm.inactivo,
                    colaborador_id_colaborador = vm.colaborador_id_colaborador
                };

                _usuarioLN.Agregar(usuario, vm.contrasena);

                foreach (var idRol in vm.rolesSeleccionados)
                {
                    _usuarioLN.AsignarRol(usuario.id_usuario, idRol);
                }

                var colaborador = _colaboradorLN.ObtenerColaboradorPorId(vm.colaborador_id_colaborador);
                if (colaborador == null)
                {
                    return NotFound("Colaborador no encontrado.");
                }

                return RedirectToAction("ObtenerColaboradorDatosCompletos", "Persona", new { idPersona = colaborador.persona_id_persona });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                // Recargar roles por si vuelve a la vista
                vm.RolesDisponibles = _rolesLN.ObtenerTodos()
                    .Select(r => new SelectListItem { Value = r.id_rol.ToString(), Text = r.nombre_rol })
                    .ToList();

                return View(vm);
            }
        }

        public IActionResult Editar(int id)
        {
            var usuario = _usuarioLN.ObtenerPorId(id);
            if (usuario == null) return NotFound();

            var vm = new UsuarioViewModel
            {
                id_usuario = usuario.id_usuario,
                nombre_usuario = usuario.nombre_usuario,
                inactivo = usuario.inactivo,
                colaborador_id_colaborador = usuario.colaborador_id_colaborador,
                rolesSeleccionados = usuario.UsuarioRoles.Select(ur => ur.id_rol).ToList(),
                RolesDisponibles = _rolesLN.ObtenerTodos()
                    .Select(r => new SelectListItem { Value = r.id_rol.ToString(), Text = r.nombre_rol })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.RolesDisponibles = _rolesLN.ObtenerTodos()
                    .Select(r => new SelectListItem { Value = r.id_rol.ToString(), Text = r.nombre_rol })
                .ToList();
                return View(vm);
            }

            var usuario = _usuarioLN.ObtenerPorId(vm.id_usuario ?? 0);
            if (usuario == null) return NotFound();

            usuario.nombre_usuario = vm.nombre_usuario;
            usuario.inactivo = vm.inactivo;

            _usuarioLN.Actualizar(usuario);

            // Limpiar roles actuales y asignar los nuevos
            var rolesActuales = usuario.UsuarioRoles.Select(x => x.id_rol).ToList();
            foreach (var rol in rolesActuales.Except(vm.rolesSeleccionados))
            {
                _usuarioLN.QuitarRol(usuario.id_usuario, rol);
            }

            foreach (var rol in vm.rolesSeleccionados.Except(rolesActuales))
            {
                _usuarioLN.AsignarRol(usuario.id_usuario, rol);
            }

            return RedirectToAction("ListaUsuarios");
        }
    }
}

