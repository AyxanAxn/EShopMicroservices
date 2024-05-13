
var builder = WebApplication.CreateBuilder(args);
var DBConnection = builder.Configuration.GetConnectionString("Database")!;
var assembly = typeof(Program).Assembly;

builder.Services.AddCarter();

builder
    .Services
    .AddMediatR(config =>
{
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.RegisterServicesFromAssemblies(assembly);
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(opts =>
{
    opts.Connection(DBConnection);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(DBConnection);
var app = builder.Build();

app.MapCarter();

// We are relaying on the method that we wrote.
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();