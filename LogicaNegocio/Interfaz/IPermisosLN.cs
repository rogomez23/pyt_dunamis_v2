using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Interfaz
{
    public interface IPermisosLN
    {
        List<Entidades.Permisos> ObtenerPermisos();
        Entidades.Permisos ObtenerPermisosPorId(int id_permiso);
        void Agregar(Entidades.Permisos p);
        void Actualizar(Entidades.Permisos p);
        void Eliminar(int id_permiso);
    }
}
