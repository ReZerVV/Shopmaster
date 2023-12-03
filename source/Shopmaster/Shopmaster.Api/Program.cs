using Shopmaster.Application;
using Shopmaster.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddHttpContextAccessor();
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
}
var app = builder.Build();
{
    app.UseCors(options => 
        options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader()
    );
    app.UseExceptionHandler("/api/errors");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}
app.Run();
