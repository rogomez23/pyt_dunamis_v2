using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class DireccionLN : IDireccionLN
    {
        private readonly IDireccionAD _direccionAD;
        public DireccionLN(IDireccionAD direccionAD)
        {
            _direccionAD = direccionAD;
        }

        public void InsertarDireccion(Direccion direccion)
        {
            _direccionAD.InsertarDireccion(direccion);
        }
        public void ActualizarDireccion(Direccion direccion)
        {
            _direccionAD.ActualizarDireccion(direccion);
        }
        public void EliminarDireccion(int idDireccion)
        {
            _direccionAD.EliminarDireccion(idDireccion);
        }

        public Direccion ObtenerDireccionPorId(int idDireccion)
        {
            return _direccionAD.ObtenerDireccionPorId(idDireccion);
        }
        public List<Direccion> ObtenerDireccionesPorPersona(string personaId)
        {
            return _direccionAD.ObtenerDireccionesPorPersona(personaId);
        }
        public List<Direccion> ObtenerDireccionCompletaPorPersona(string personaId)
        {
            return _direccionAD.ObtenerDireccionCompletaPorPersona(personaId);
        }
    }
}
