using Microsoft.EntityFrameworkCore;
using PuntoCuatroSQLite.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// --- conexión SQLite ---
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "app.db");
// si querés usar siempre el de appsettings.json, podés dejar solo la línea siguiente:
// builder.Services.AddDbContext<LaboratorioContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<LaboratorioContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

var app = builder.Build();

// --- aplica migraciones y seed opcional ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LaboratorioContext>();
    db.Database.Migrate();

    // SEED opcional: datos de prueba si la base está vacía
    if (!db.Muestras.Any())
    {
        var mu = new PuntoCuatro.Models.Muestra
        {
            Nombre = "Marcelo",
            Matriz = "Agua",
            FechaToma = DateTime.Today
        };
        db.Muestras.Add(mu);
        db.SaveChanges();

        db.Ensayos.Add(new PuntoCuatro.Models.Ensayo
        {
            MuestraId = mu.Id,
            Tipo = "pH",
            Resultado = "7,20",
            Fecha = DateTime.Today
        });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


