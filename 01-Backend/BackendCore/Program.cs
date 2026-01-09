using System.Text.Encodings.Web;
using System.Text.Unicode;
using BackendCore.Interfaces;
using BackendCore.Services;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// 1. REGISTRO DE SERVICIOS
// -----------------------------------------------------------
builder.Services.AddHttpClient("RickAndMorty", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["RickAndMortyApi:BaseUrl"] ?? "https://rickandmortyapi.com/api/");
});

builder.Services.AddScoped<IEpisodeService, EpisodeService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

// Configuración de Políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy.WithOrigins("https://prueba-tecnica-chile-autos-p-o.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// -----------------------------------------------------------
// 2. CONFIGURACIÓN DEL PIPELINE (ORDEN IMPORTANTE)
// -----------------------------------------------------------

// 1. Manejo de errores siempre al principio
app.UseMiddleware<BackendCore.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Enrutamiento
app.UseRouting();

// 3. Aplicación Dinámica de CORS (Solo una vez)
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAngularDev");
}
else
{
    app.UseCors("ProductionPolicy");
}

// 4. Redirección HTTPS en producción
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// 5. Autorización y Mapeo
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();