using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Implementacion
{
    public class BonificacionPuestoLN : IBonificacionPuestoLN
    {
        private readonly IBonificacionPuestoAD _bonificacionPuestoAD;

        public BonificacionPuestoLN(IBonificacionPuestoAD bonificacionPuestoAD)
        {
            _bonificacionPuestoAD = bonificacionPuestoAD;
        }

        public IEnumerable<Catalogo_perfil_puesto> ObtenerListaBonificacionPuesto()
        {
            return _bonificacionPuestoAD.ObtenerListaBonificacionPuesto();
        }

        public Catalogo_perfil_puesto ObtenerBonificacionPuestoPorId(int id_perfil_puesto)
        {
            return _bonificacionPuestoAD.ObtenerBonificacionPuestoPorId(id_perfil_puesto);
        }

        public void AsignarBonificacion(int id_puesto, int id_bonificacion)
        {
            _bonificacionPuestoAD.AsignarBonificacion(id_puesto, id_bonificacion);
        }

        public void QuitarBonificacion(int id_puesto, int id_bonificacion)
        {
            _bonificacionPuestoAD.QuitarBonificacion(id_puesto, id_bonificacion);
        }


    }
}
