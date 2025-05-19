using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Service;
using RegistroDeTecnicos.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
//builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlServer(ConStr));

var ConStr = builder.Configuration.GetConnectionString("SqliteConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlite(ConStr));

// 💡 Registrar el servicio personalizado
builder.Services.AddScoped<TecnicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();