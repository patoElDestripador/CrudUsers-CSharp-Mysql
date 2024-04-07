using Microsoft.EntityFrameworkCore;
using CrudUsers0.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("LaConexionCita"),Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));

/*
builder.Services: Esto es como agregar una nueva función a nuestra aplicación. El objeto 'builder' representa la aplicación web en sí misma, 
y 'Services' nos da acceso a un lugar especial donde registramos nuevas funcionalidades que nuestra aplicación utilizará al iniciarse.

AddDbContext: Esto es como decirle a nuestra aplicación que tenga en cuenta una nueva herramienta llamada 'dbContext'. 
Recuerden que en los controladores mencionamos algo llamado dbContext. Bueno, esto es exactamente eso. 
Es la configuración de la base de datos. Le estamos diciendo al código, "oiga mire vea vengase a cali para que ve.. bueno cuando te mencionen 'dbContext', 
aquí es donde debes buscar la información de la base de datos".

UseMySql: Esto le dice a nuestra aplicación qué tipo de base de datos estamos utilizando.
Por ejemplo, si estuviéramos usando una base de datos PostgreSQL, sería 'UseNpgsql'.
Y si estuviéramos usando MongoDB, sería 'UseMongoDb'.

GetConnectionString: Esto es como abrir una caja fuerte.
Obtiene la información de conexión necesaria para acceder a nuestra base de datos
desde un lugar seguro y protegido llamado 'appsettings.json' Chaaanchhaaaan. 
El nombre 'LaConexionCita' puede ser cualquier cosa, 
pero es importante mantenerlo en secreto, como si fuera una combinación de caja fuerte y tales.

Microsoft.EntityFrameworkCore.ServerVersion : ya lo otro me da pereza escribirlo son como las 6 am entonces sirve para algo asi para especificar la versión del servidor
que MySQL que se utilizará

*/

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
