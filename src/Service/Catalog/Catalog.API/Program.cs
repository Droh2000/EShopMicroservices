var builder = WebApplication.CreateBuilder(args);// Antes de crear la aplicacion

// Add Services to the Container (Inyection Dependencies)
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();// Despues de crear la aplicacion

// Configure the HTTP request Pipeline (Mapear las peticiones entrantes)
app.MapCarter();

app.Run();
