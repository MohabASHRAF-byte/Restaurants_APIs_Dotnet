using Restaurants.API.Extensions;
using Restaurants.API.MiddleWares;
using Restaurants.Domain.Entities;
using Restuarants.Application.Extensions;
using Restuarants.infrastructure.Extentions;
using Restuarants.infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<GlobalErrorHandling>();
builder.Services.AddScoped<RequestTimeLogging>();


var app = builder.Build();
var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await serviceProvider.Seed();
// app.Services.Seed();

app.UseMiddleware<GlobalErrorHandling>();
app.UseMiddleware<RequestTimeLogging>();
app.UseHttpsRedirection();
app.MapGroup("api/Identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();
app.UseAuthorization();

app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

public partial class Program
{
}