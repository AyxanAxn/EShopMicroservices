using Ordering.Infrastructure.Data.Extensions;
using Ordering.Infrastructure;
using Ordering.Application;
using Ordering.API;

var builder = WebApplication.CreateBuilder(args);

#region Added services
builder.Services.AddApiServices(builder.Configuration)
                .AddApplicationServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration);
#endregion

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    //when application starts it will use update db
    await app.InitialiseDatabaseAsync();
}

app.Run();