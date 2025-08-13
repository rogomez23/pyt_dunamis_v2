using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace pyt_dunamis_v2.Helpers
{
    public static class SesionHelper
    {
        public static bool TienePermiso(HttpContext context, string permiso)
        {
            var permisos = context.Session.GetString("Permisos");
            if (string.IsNullOrEmpty(permisos)) return false;
            return permisos.Split(',').Contains(permiso);
        }

        public static bool TieneRol(HttpContext context, string rol)
        {
            var roles = context.Session.GetString("Roles");
            if (string.IsNullOrEmpty(roles)) return false;
            return roles.Split(',').Contains(rol);
        }
    }
}
