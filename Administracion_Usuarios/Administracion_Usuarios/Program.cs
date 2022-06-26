using Administracion_Usuarios.Data;
using Administracion_Usuarios.Helpers;
using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyección de la dependencia de conexión de la Base de datos
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PcConnection"));
});



//Configuración para las notificaciones
builder.Services.AddNotyf(config => {
    config.DurationInSeconds = 9999999;
    config.IsDismissable = true;
    config.HasRippleEffect = false;
    config.Position = NotyfPosition.TopRight;
});


//Inyección del Combos Helper
builder.Services.AddScoped<ICombosHelper, CombosHelper>();

//Agregar para que actualice los cambios al momento del desarrollo
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//Inyección del feedDb 
builder.Services.AddTransient<SeedDb>();

var app = builder.Build();

//Llamar al feedDb
SeedData();

void SeedData()
{
    IServiceScopeFactory? scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopeFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb?>();
        service.SeedAsync().Wait();
    }
}

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
