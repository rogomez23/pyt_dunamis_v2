using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Interfaz
{
    public interface IBonificacionPuestoLN
    {
        IEnumerable<Catalogo_perfil_puesto> ObtenerListaBonificacionPuesto();
        Catalogo_perfil_puesto ObtenerBonificacionPuestoPorId(int id_perfil_puesto);
        void AsignarBonificacion(int id_puesto, int id_bonificacion);
        void QuitarBonificacion(int id_puesto, int id_bonificacion);
    }
}
