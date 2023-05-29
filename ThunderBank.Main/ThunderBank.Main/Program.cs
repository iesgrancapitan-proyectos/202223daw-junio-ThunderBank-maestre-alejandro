using Microsoft.AspNetCore.Identity;
using ThunderBank.Main.Auth;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;
using ThunderBank.Services.Repositorios;
using ThunderBank.Services.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var sqlConfiguration = new SqlConfiguration(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton(sqlConfiguration);

builder.Services.AddTransient<IRepositorioCliente, RepositorioCliente>();
builder.Services.AddTransient<IRepositorioCuenta, RepositorioCuenta>();
builder.Services.AddTransient<IRepositorioTarjeta, RepositorioTarjeta>();
builder.Services.AddTransient<IRepositorioResponsable, RepositorioResponsable>();
builder.Services.AddTransient<IRepositorioMovimiento, RepositorioMovimiento>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddTransient<IUserStore<Usuario>,UsuarioStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SignInManager<Usuario>>();


builder.Services.AddIdentityCore<Usuario>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    opt.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme);


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
