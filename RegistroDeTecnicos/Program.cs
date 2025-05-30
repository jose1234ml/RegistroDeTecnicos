using Microsoft.EntityFrameworkCore;
using RegistroDeTecnicos.Components.DAL;
using RegistroDeTecnicos.Components.Service;
using RegistroDeTecnicos.Components;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("SqliteConStr");
builder.Services.AddDbContextFactory<Contexto>(o => o.UseSqlite(ConStr));
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<TecnicoService>();
builder.Services.AddScoped<ClienteService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<Contexto>>();
    using var db = dbFactory.CreateDbContext();
    db.Database.Migrate();
}

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