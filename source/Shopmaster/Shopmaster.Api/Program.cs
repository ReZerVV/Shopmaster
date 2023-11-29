using Shopmaster.Application;
using Shopmaster.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}
var app = builder.Build();
{
    app.UseExceptionHandler("/api/errors");
    app.UseHttpsRedirection();
    app.MapControllers();
}
app.Run();
