using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaz
{
    public interface IDireccionAD
    {
        void InsertarDireccion(Direccion direccion);
        void ActualizarDireccion(Direccion direccion);
        void EliminarDireccion(int idDireccion);
        Direccion ObtenerDireccionPorId(int idDireccion);
        List<Direccion> ObtenerDireccionesPorPersona(string personaId);
        List<Direccion> ObtenerDireccionCompletaPorPersona(string personaId);

    }
}
