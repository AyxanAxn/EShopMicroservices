var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;


builder
    .Services
    .AddMediatR(config =>
    {
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        config.RegisterServicesFromAssemblies(assembly);
    });

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();