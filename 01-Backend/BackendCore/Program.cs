using System.Text.Encodings.Web;
using System.Text.Unicode;
using BackendCore.Interfaces; 
using BackendCore.Services;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// 1. REGISTRO DE SERVICIOS (Antes de builder.Build())
// -----------------------------------------------------------

// HttpClient para el BFF
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", policy =>
    {
        policy.WithOrigins("https://vercel.vercel.app") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Mantenemos la de desarrollo para tus pruebas locales
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -----------------------------------------------------------
// 2. CONSTRUCCIÓN DE LA APP
// -----------------------------------------------------------
var app = builder.Build();

// -----------------------------------------------------------
// 3. MIDDLEWARES (Configuración del Pipeline)
// -----------------------------------------------------------

app.UseMiddleware<BackendCore.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowAngularDev");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();