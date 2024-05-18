using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.API;

var builder = WebApplication.CreateBuilder(args);

#region Added services
builder.Services.AddApiServices()
                .AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration);
#endregion

var app = builder.Build();

app.UseApiServices();

app.Run();