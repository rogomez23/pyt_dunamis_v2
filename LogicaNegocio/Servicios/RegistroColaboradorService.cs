using AccesoDatos.Implementacion;
using Entidades.DTOs;
using LogicaNegocio.Interfaz;

namespace LogicaNegocio.Servicios
{
    public class RegistroColaboradorService : IRegistroColaboradorServiceLN
    {
        private readonly IPersonaLN _personaLN;
        private readonly IColaboradorLN _colaboradorLN;
        private readonly IDireccionLN _direccionLN;
        private readonly IEmailLN _emailLN;
        private readonly ITelefonoLN _telefonoLN;
        private readonly AppDbContext _context;

        public RegistroColaboradorService(
            AppDbContext context,
            IPersonaLN personaLN,
            IColaboradorLN colaboradorLN,
            IDireccionLN direccionLN,
            IEmailLN emailLN,
            ITelefonoLN telefonoLN)
        {
            _context = context;
            _personaLN = personaLN;
            _colaboradorLN = colaboradorLN;
            _direccionLN = direccionLN;
            _emailLN = emailLN;
            _telefonoLN = telefonoLN;
        }

        public void InsertarColaborador(PersonaCompletaDTO vm)
        {
           using (var transaction = _context.Database.BeginTransaction())
           {
              try
              {
                    // Insertar persona
                    _personaLN.InsertarPersona(vm.Persona);
                    string personaId = vm.Persona.id_persona;

                    // Emails
                    if (vm.Emails != null)
                    {
                        foreach (var email in vm.Emails)
                        {
                            email.persona_id_persona = personaId;
                            _emailLN.InsertarEmail(email);
                        }
                    }

                    // Teléfonos
                    if (vm.Telefonos != null)
                    {
                        foreach (var tel in vm.Telefonos)
                        {
                            tel.persona_id_persona = personaId;
                            _telefonoLN.InsertarTelefono(tel);
                        }
                    }

                    // Direcciones
                    if (vm.Direcciones != null)
                    {
                        foreach (var dir in vm.Direcciones)
                        {
                            dir.persona_id_persona = personaId;
                            _direccionLN.InsertarDireccion(dir);
                        }
                    }

                    // Colaborador
                    if (vm.Colaborador != null)
                    {
                        vm.Colaborador.persona_id_persona = personaId;
                        _colaboradorLN.InsertarColaborador(vm.Colaborador);
                    }

                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error en InsertarPersonaCompleta: " + ex.Message);
                    Console.WriteLine("StackTrace: " + ex.StackTrace);
                    transaction.Rollback();
                    throw;
                }
           }
        }
    }
}
