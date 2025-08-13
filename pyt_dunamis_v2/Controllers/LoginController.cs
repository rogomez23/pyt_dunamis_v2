using LogicaNegocio.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace pyt_dunamis_v2.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginLN _loginLN;

        public LoginController(ILoginLN loginLN)
        {
            _loginLN = loginLN;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Usuario, string Contrasena)
        {
            var usuario = _loginLN.Autenticar(Usuario, Contrasena);

            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.nombre_usuario);
                HttpContext.Session.SetInt32("IdColaborador", usuario.colaborador_id_colaborador);

                // Guardar roles
                var roles = usuario.UsuarioRoles.Select(ur => ur.Rol.nombre_rol).ToList();
                HttpContext.Session.SetString("Roles", string.Join(",", roles));

                // Guardar permisos
                var permisos = usuario.UsuarioRoles
                    .SelectMany(ur => ur.Rol.PermisoRol)
                    .Select(pr => pr.Permiso.nom_permiso)
                    .Distinct()
                    .ToList();
                HttpContext.Session.SetString("Permisos", string.Join(",", permisos));






                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

