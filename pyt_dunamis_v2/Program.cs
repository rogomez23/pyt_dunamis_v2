using AccesoDatos.Implementacion;
using AccesoDatos.Interfaz;
using Entidades;
using LogicaNegocio.Implementacion;
using LogicaNegocio.Interfaz;
using LogicaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);



// Conexión a MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ConexionMySQL")));

// Inyección de dependencias
builder.Services.AddScoped<IPersonaAD, PersonaAD>();
builder.Services.AddScoped<IPersonaLN, PersonaLN>();

builder.Services.AddScoped<IEmailAD, EmailAD>();
builder.Services.AddScoped<IEmailLN, EmailLN>();

builder.Services.AddScoped<ICatalogosAD, CatalogosAD>();
builder.Services.AddScoped<ICatalogosLN, CatalogosLN>();

builder.Services.AddScoped<IDireccionAD, DireccionAD>();
builder.Services.AddScoped<IDireccionLN, DireccionLN>();

builder.Services.AddScoped<ITelefonoAD, TelefonoAD>();
builder.Services.AddScoped<ITelefonoLN, TelefonoLN>();

builder.Services.AddScoped<IColaboradorAD, ColaboradorAD>();
builder.Services.AddScoped<IColaboradorLN, ColaboradorLN>();

builder.Services.AddScoped<IRegistroColaboradorServiceLN, RegistroColaboradorService>();

builder.Services.AddScoped<IUsuarioAD, UsuarioAD>();
builder.Services.AddScoped<IUsuarioLN, UsuarioLN>();


builder.Services.AddScoped<IRolesAD, RolesAD>();
builder.Services.AddScoped<IRolesLN, RolesLN>();

builder.Services.AddScoped<IPermisosAD, PermisosAD>();
builder.Services.AddScoped<IPermisosLN, PermisosLN>();

builder.Services.AddScoped<ILoginAD, LoginAD>();
builder.Services.AddScoped<ILoginLN, LoginLN>();

builder.Services.AddScoped<IVacacionesAD, VacacionesAD>();
builder.Services.AddScoped<IVacacionesLN, VacacionesLN>();

builder.Services.AddScoped<IHorasExtraAD, HorasExtraAD>();
builder.Services.AddScoped<IHorasExtraLN, HorasExtraLN>();

builder.Services.AddScoped<IOrdenesAD, OrdenesAD>();
builder.Services.AddScoped<IOrdenesLN, OrdenesLN>();

builder.Services.AddScoped<IBonificacionPuestoAD, BonificacionPuestoAD>();
builder.Services.AddScoped<IBonificacionPuestoLN, BonificacionPuestoLN>();

builder.Services.AddScoped<IBonificacionesAD, BonificacionesAD>();
builder.Services.AddScoped<IBonificacionesLN, BonificacionesLN>();



// Agrega servicios necesarios
builder.Services.AddControllersWithViews();

// ?? Agrega servicio de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Tiempo de inactividad
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Usa sesiones
app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
