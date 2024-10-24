var builder = WebApplication.CreateBuilder(args);// Antes de crear la aplicacion

// Add Services to the Container (Inyection Dependencies)
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();// Despues de crear la aplicacion

// Configure the HTTP request Pipeline (Mapear las peticiones entrantes)
app.MapCarter();

app.Run();
