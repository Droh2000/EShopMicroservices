using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);// Antes de crear la aplicacion

// Add Services to the Container (Inyection Dependencies)
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

// Manejo Global de Excepciones
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();// Despues de crear la aplicacion

// Configure the HTTP request Pipeline (Mapear las peticiones entrantes)
app.MapCarter();

app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
