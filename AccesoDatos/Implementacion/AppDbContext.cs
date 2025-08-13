using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Entidades;
using Entidades.DTOs;

namespace AccesoDatos.Implementacion
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
        public DbSet<Colaborador> Colaborador { get; set; }


        public DbSet<Catalogo_tipo_contacto> Catalogo_tipo_contacto { get; set; }
        public DbSet<Catalogo_tipo_direccion> Catalogo_tipo_direccion { get; set; }
        public DbSet<Catalogo_provincia> Catalogo_provincia { get; set; }
        public DbSet<Catalogo_canton> Catalogo_canton { get; set; }
        public DbSet<Catalogo_distrito> Catalogo_distrito { get; set; }
        public DbSet<Catalogo_perfil_puesto> Catalogo_perfil_puesto { get; set; }
        public DbSet<Catalogo_estado_orden> Catalogo_estado_orden { get; set; }
        public DbSet<Catalogo_estado_aprobacion> Catalogo_estado_aprobacion { get; set; }
        public DbSet<Periodo_nomina> Periodo_nomina { get; set; }
        public DbSet<Catalogo_bonificaciones> Catalogo_Bonificaciones { get; set; }
        public DbSet<Bonificacion_puesto> Bonificacion_puesto { get; set; }


        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Usuarios_roles> UsuarioRoles { get; set; }
        public DbSet<Permisos_roles> PermisoRoles { get; set; }


        public DbSet<Vacaciones> Vacaciones { get; set; }
        public DbSet<Horas_extra> Horas_extra { get; set; }
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Bonificaciones> Bonificaciones { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasKey(p => p.id_persona);
            modelBuilder.Entity<Email>().HasKey(p => p.id_email);
            modelBuilder.Entity<Cliente>().HasKey(p => p.id_contrato);
            modelBuilder.Entity<Direccion>().HasKey(p => p.id_direccion);
            modelBuilder.Entity<Telefono>().HasKey(p => p.id_telefono);
            modelBuilder.Entity<Colaborador>().HasKey(p => p.id_colaborador);


            modelBuilder.Entity<Catalogo_tipo_contacto>().HasKey(p => p.id_catalogo_tipo_contacto);
            modelBuilder.Entity<Catalogo_tipo_direccion>().HasKey(p => p.id_tipo_direccion);
            modelBuilder.Entity<Catalogo_provincia>().HasKey(p => p.id_provincia);
            modelBuilder.Entity<Catalogo_canton>().HasKey(p => p.id_canton);
            modelBuilder.Entity<Catalogo_distrito>().HasKey(p => p.id_distrito);
            modelBuilder.Entity<Catalogo_perfil_puesto>().HasKey(p => p.id_perfil_puesto);
            modelBuilder.Entity<Catalogo_estado_aprobacion>().HasKey(e => e.id_estado_aprobacion);
            modelBuilder.Entity<Periodo_nomina>().HasKey(p => p.id_periodo_nomina);
            modelBuilder.Entity<Catalogo_estado_orden>().HasKey(e => e.id_estado_orden);
            modelBuilder.Entity<Catalogo_bonificaciones>().HasKey(e => e.id_catalogo_bonificacion);
            modelBuilder.Entity<Bonificacion_puesto>()
                .HasKey(pr => new { pr.catalogo_perfil_puesto_id_perfil_puesto, pr.catalogo_bonificaciones_id_catalogo_bonificacion });

            modelBuilder.Entity<Usuarios_roles>().HasKey(ur => new { ur.id_usuario, ur.id_rol });
            modelBuilder.Entity<Permisos_roles>().HasKey(pr => new { pr.id_rol, pr.id_permiso });
            modelBuilder.Entity<Permisos>().HasKey(p => p.id_permiso);
            modelBuilder.Entity<Roles>().HasKey(r => r.id_rol);
            modelBuilder.Entity<Usuarios>().HasKey(u => u.id_usuario);

            modelBuilder.Entity<Vacaciones>().HasKey(v => v.id_vacaciones);
            modelBuilder.Entity<Horas_extra>().HasKey(h => h.id_horas_extra);
            modelBuilder.Entity<Ordenes>().HasKey(o => o.id_orden);
            modelBuilder.Entity<Bonificaciones>().HasKey(o => o.id_bonificaciones);





            modelBuilder.Entity<Permisos_roles>(entity =>
            {
                entity.HasOne(e => e.Permiso)
                        .WithMany(p => p.PermisoRol)
                        .HasForeignKey(e => e.id_permiso);
                entity.HasOne(e => e.Rol)
                        .WithMany(p => p.PermisoRol)
                        .HasForeignKey(e => e.id_rol);
            });


            modelBuilder.Entity<Usuarios_roles>(entity =>
            {
                entity.HasOne(e => e.Usuario)
                        .WithMany(p => p.UsuarioRoles)
                        .HasForeignKey(e => e.id_usuario);
                entity.HasOne(e => e.Rol)
                        .WithMany(p => p.UsuarioRoles)
                        .HasForeignKey(e => e.id_rol);
            });


            modelBuilder.Entity<Bonificacion_puesto>(entity =>
            {
                entity.HasOne(e => e.catalogo_Bonificaciones)
                        .WithMany(p => p.Bonificacion_puestos)
                        .HasForeignKey(e => e.catalogo_bonificaciones_id_catalogo_bonificacion);
                entity.HasOne(e => e.catalogo_Perfil_Puesto)
                        .WithMany(p => p.Bonificacion_puesto)
                        .HasForeignKey(e => e.catalogo_perfil_puesto_id_perfil_puesto);
            });

            // Relación uno a muchos: Persona → Emails

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasOne(e => e.Persona)
                        .WithMany(p => p.emails)
                        .HasForeignKey(e => e.persona_id_persona);
                entity.HasOne(e => e.Catalogo_tipo_contacto)
                        .WithMany(p => p.Emails)
                        .HasForeignKey(e => e.catalogo_tipo_contacto_id_catalogo_tipo_contacto);
            });



            modelBuilder.Entity<Telefono>(entity =>
            {
                entity.HasOne(e => e.Persona)
                      .WithMany(p => p.telefono)
                      .HasForeignKey(e => e.persona_id_persona);

                entity.HasOne(e => e.Catalogo_tipo_contacto)
                      .WithMany(p => p.Telefonos)
                      .HasForeignKey(e => e.catalogo_tipo_contacto_id_catalogo_tipo_contacto);
            });


            modelBuilder.Entity<Colaborador>(entity =>
            {

                entity.HasOne(e => e.Persona)
                      .WithOne(p => p.colaborador)
                      .HasForeignKey<Colaborador>(e => e.persona_id_persona);

                entity.HasOne(e => e.Catalogo_perfil_puesto)
                      .WithMany(p => p.Colaboradores)
                      .HasForeignKey(e => e.catalogo_perfil_puesto_id_perfil_puesto);
            });


            modelBuilder.Entity<Vacaciones>(entity =>
            {
                entity.HasOne(e => e.Colaborador)
                      .WithMany(p => p.Vacaciones)
                      .HasForeignKey(e => e.colaborador_id_colaborador);

                entity.HasOne(e => e.Catalogo_estado_aprobacion)
                      .WithMany(p => p.Vacaciones)
                      .HasForeignKey(e => e.catalogo_estado_aprobacion_id_estado_aprobacion);

                entity.HasOne(e => e.Periodo_nomina)
                      .WithMany(p => p.Vacaciones)
                      .HasForeignKey(e => e.periodo_nomina_idperiodo_nomina);
            });


            modelBuilder.Entity<Bonificaciones>(entity =>
            {
                entity.HasOne(e => e.Colaborador)
                      .WithMany(p => p.Bonificaciones)
                      .HasForeignKey(e => e.colaborador_id_colaborador);

                entity.HasOne(e => e.Catalogo_estado_aprobacion)
                      .WithMany(p => p.Bonificaciones)
                      .HasForeignKey(e => e.catalogo_estado_aprobacion_id_estado_aprobacion);

                entity.HasOne(e => e.Periodo_nomina)
                      .WithMany(p => p.Bonificaciones)
                      .HasForeignKey(e => e.periodo_nomina_id_periodo_nomina);

                entity.HasOne(e => e.CatalogoBonificaciones)
                      .WithMany(p => p.Bonificaciones)
                      .HasForeignKey(e => e.catalogo_bonificacion_id_catalogo_bonificacion);
            });







            modelBuilder.Entity<Horas_extra>(entity =>
            {
                entity.HasOne(e => e.Colaborador)
                      .WithMany(p => p.Horas_extra)
                      .HasForeignKey(e => e.colaborador_id_colaborador);

                entity.HasOne(e => e.Catalogo_estado_aprobacion)
                      .WithMany(p => p.Horas_extra)
                      .HasForeignKey(e => e.catalogo_estado_aprobacion_id_estado_aprobacion);

                entity.HasOne(e => e.Periodo_nomina)
                      .WithMany(p => p.Horas_extras)
                      .HasForeignKey(e => e.periodo_nomina_idperiodo_nomina);
            });

            modelBuilder.Entity<Ordenes>(entity =>
            {
                entity.HasOne(e => e.Colaborador)
                      .WithMany(p => p.Ordenes)
                      .HasForeignKey(e => e.colaborador_id_colaborador);

                entity.HasOne(e => e.Periodo_nomina)
                      .WithMany(p => p.Ordenes)
                      .HasForeignKey(e => e.periodo_nomina_id_periodo_nomina);

                entity.HasOne(e => e.Estado_orden)
                      .WithMany(p => p.Ordenes)
                      .HasForeignKey(e => e.estado_orden_id_estado_orden);

                entity.HasOne(e => e.Catalogo_estado_aprobacion)
                      .WithMany(p => p.Ordenes)
                      .HasForeignKey(e => e.catalogo_estado_aprobacion_id_estado_aprobacion);
            });



            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.HasOne(e => e.Persona)
                      .WithMany(p => p.direcciones)
                      .HasForeignKey(e => e.persona_id_persona);

                entity.HasOne(e => e.Catalogo_distrito)
                      .WithMany(p => p.Direcciones)
                      .HasForeignKey(e => e.catalogo_distrito_id_distrito);
                entity.HasOne(e => e.Catalogo_canton)
                        .WithMany(p => p.Direcciones)
                        .HasForeignKey(e => e.catalogo_canton_id_canton);
                entity.HasOne(e => e.Catalogo_provincia)
                        .WithMany(p => p.Direcciones)
                        .HasForeignKey(e => e.catalogo_provincia_id_provincia);
                entity.HasOne(e => e.Catalogo_tipo_direccion)
                        .WithMany(p => p.Direcciones)
                        .HasForeignKey(e => e.catalogo_tipo_direccion_id_tipo_direccion);
            });


                        // Relación uno a muchos: Persona → Contratos
            modelBuilder.Entity<Cliente>()
                .HasOne(e => e.Persona)
                .WithMany(p => p.clientes)
                .HasForeignKey(e => e.persona_id_persona);

            




            // Relación uno a muchos: Canton y distrito → Provincias
            modelBuilder.Entity<Catalogo_canton>()
                .HasOne(e => e.Catalogo_provincia)
                .WithMany(p => p.Catalogo_cantones)
                .HasForeignKey(e => e.catalogo_provincia_id_provincia);

            modelBuilder.Entity<Catalogo_distrito>()
                .HasOne(e => e.Catalogo_canton)
                .WithMany(p => p.Catalogo_distritos)
                .HasForeignKey(e => e.catalogo_canton_id_canton);

            modelBuilder.Entity<Catalogo_distrito>()
                .HasOne(e => e.Catalogo_provincia)
                .WithMany(p => p.Catalogo_distritos)
                .HasForeignKey(e => e.catalogo_provincia_id_provincia);


        }
    }
}
