using Restaurats.API.Middlewares;
using Restaurats.Application.Extensions;
using Restaurats.Domain.Entities;
using Restaurats.Infrastructure.Extensions;
using Serilog;
using Restaurats.API.Extensions;
using Restaurats.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);




var app = builder.Build();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var seeders=services.GetRequiredService<IRestaurantSeeder>();
await seeders.SeedAsync();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<ApplicationUser>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
