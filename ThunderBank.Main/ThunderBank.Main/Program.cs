using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ThunderBank.Main.Auth;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.Repositorios;
using ThunderBank.Services.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

var sqlConfiguration = new SqlConfiguration(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(sqlConfiguration);

builder.Services.AddTransient<IRepositorioCliente, RepositorioCliente>();
builder.Services.AddTransient<IRepositorioCuenta, RepositorioCuenta>();
builder.Services.AddTransient<IRepositorioTarjeta, RepositorioTarjeta>();
builder.Services.AddTransient<IRepositorioResponsable, RepositorioResponsable>();
builder.Services.AddTransient<IRepositorioMovimiento, RepositorioMovimiento>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IUserStore<Usuario>, UsuarioStore>();
builder.Services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddTransient<SignInManager<Usuario>>();


builder.Services.AddIdentityCore<Usuario>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
}).AddRoles<ApplicationRole>()
.AddDefaultTokenProviders();

// Agregar los roles personalizados
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<ApplicationRole>>();

    // Crear roles si no existen
    var roles = new[] { "RESPONSABLE", "CLIENTE" };
    foreach (var roleName in roles)
    {
        if (!roleManager.RoleExistsAsync(roleName).Result)
        {
            var role = new ApplicationRole { Name = roleName };
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                // Manejar el error en caso de que no se pueda crear el rol
            }
        }
    }
}

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    opt.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme,options =>
{
    options.LoginPath = "/Usuario/Login";
});




var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
