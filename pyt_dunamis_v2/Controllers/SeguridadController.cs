using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;
using Entidades;

namespace TuProyecto.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly IUsuarioLN _usuarioLN;
        private readonly IRolesLN _rolesLN;
        private readonly IPermisosLN _permisosLN;

        public SeguridadController(IUsuarioLN usuarioLN, IRolesLN rolesLN, IPermisosLN permisosLN)
        {
            _usuarioLN = usuarioLN;
            _rolesLN = rolesLN;
            _permisosLN = permisosLN;
        }

        // Vista para la lista de usuarios
        public IActionResult Usuarios()
        {
            var usuarios = _usuarioLN.ObtenerTodos();
            return View(usuarios);
        }

        // Vista para la creación y edición de usuarios
        public IActionResult CrearEditarUsuario(int? id)
        {
            var usuario = id.HasValue ? _usuarioLN.ObtenerPorId(id.Value) : new Usuarios();
            return View(usuario);
        }

        // Acción para guardar o actualizar un usuario
        [HttpPost]
        public IActionResult CrearEditarUsuario(Usuarios usuario, string contrasenaPlano)
        {
            if (ModelState.IsValid)
            {
                if (usuario.id_usuario == 0)
                {
                    _usuarioLN.Agregar(usuario, contrasenaPlano);
                }
                else
                {
                    _usuarioLN.Actualizar(usuario);
                }
                return RedirectToAction("Usuarios");
            }
            return View(usuario);
        }

        // Acción para eliminar un usuario
        public IActionResult EliminarUsuario(int id)
        {
            _usuarioLN.Eliminar(id);
            return RedirectToAction("Usuarios");
        }

        // Vista para asignar roles a un usuario
        public IActionResult AsignarRoles(int id_usuario)
        {
            var usuario = _usuarioLN.ObtenerPorId(id_usuario);
            var roles = _rolesLN.ObtenerTodos();
            ViewBag.Roles = roles;
            return View(usuario);
        }

        // Acción para asignar un rol a un usuario
        [HttpPost]
        public IActionResult AsignarRol(int id_usuario, int id_rol)
        {
            _usuarioLN.AsignarRol(id_usuario, id_rol);
            return RedirectToAction("Usuarios");
        }

        // Acción para quitar un rol de un usuario
        public IActionResult QuitarRol(int id_usuario, int id_rol)
        {
            _usuarioLN.QuitarRol(id_usuario, id_rol);
            return RedirectToAction("Usuarios");
        }

        // Vista para ver los roles
        public IActionResult Roles()
        {
            var roles = _rolesLN.ObtenerTodos();
            return View(roles);
        }

        // Vista para crear o editar un rol
        public IActionResult CrearEditarRol(int? id)
        {
            var rol = id.HasValue ? _rolesLN.ObtenerPorId(id.Value) : new Roles();
            return View(rol);
        }

        // Acción para guardar o actualizar un rol
        [HttpPost]
        public IActionResult CrearEditarRol(Roles rol)
        {
            if (ModelState.IsValid)
            {
                if (rol.id_rol == 0)
                {
                    _rolesLN.Agregar(rol);
                }
                else
                {
                    _rolesLN.Actualizar(rol);
                }
                return RedirectToAction("Roles");
            }
            return View(rol);
        }

        // Acción para eliminar un rol
        public IActionResult EliminarRol(int id)
        {
            _rolesLN.Eliminar(id);
            return RedirectToAction("Roles");
        }

        // Vista para asignar permisos a un rol
        public IActionResult AsignarPermisos(int id_rol)
        {
            var rol = _rolesLN.ObtenerPorId(id_rol);
            var permisos = _permisosLN.ObtenerPermisos();
            ViewBag.Permisos = permisos;
            return View(rol);
        }

        // Acción para asignar un permiso a un rol
        [HttpPost]
        public IActionResult AsignarPermiso(int id_rol, int id_permiso)
        {
            _rolesLN.AsignarPermiso(id_rol, id_permiso);
            return RedirectToAction("Roles");
        }

        // Acción para quitar un permiso de un rol
        public IActionResult QuitarPermiso(int id_rol, int id_permiso)
        {
            _rolesLN.QuitarPermiso(id_rol, id_permiso);
            return RedirectToAction("Roles");
        }

        // Vista para ver los permisos
        public IActionResult Permisos()
        {
            var permisos = _permisosLN.ObtenerPermisos();
            return View(permisos);
        }

        // Vista para crear o editar un permiso
        public IActionResult CrearEditarPermiso(int? id)
        {
            var permiso = id.HasValue ? _permisosLN.ObtenerPermisosPorId(id.Value) : new Permisos();
            return View(permiso);
        }

        // Acción para guardar o actualizar un permiso
        [HttpPost]
        public IActionResult CrearEditarPermiso(Permisos permiso)
        {
            if (ModelState.IsValid)
            {
                if (permiso.id_permiso == 0)
                {
                    _permisosLN.Agregar(permiso);
                }
                else
                {
                    _permisosLN.Actualizar(permiso);
                }
                return RedirectToAction("Permisos");
            }
            return View(permiso);
        }

        // Acción para eliminar un permiso
        public IActionResult EliminarPermiso(int id)
        {
            _permisosLN.Eliminar(id);
            return RedirectToAction("Permisos");
        }
    }
}
