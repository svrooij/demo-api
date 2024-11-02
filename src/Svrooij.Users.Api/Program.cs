using Microsoft.AspNetCore.Http.HttpResults;
using Svrooij.Users.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Svrooij.Users.Api.Service.UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true) // always enable swagger
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/users", async (Svrooij.Users.Api.Service.UserRepository repository) =>
{
    return await repository.GetUsersAsync();
}).WithName("GetUsers").WithTags("Users").WithOpenApi();

app.MapGet("/users/{id:guid}", async (Svrooij.Users.Api.Service.UserRepository repository, string id) =>
{
    var user = await repository.GetUserAsync(id);
    if (user == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(user);
})
    .WithName("GetUser").WithTags("Users")
    .WithOpenApi()
    .ProducesProblem(404)
    .Produces<User>();

app.MapPost("/users", async (Svrooij.Users.Api.Service.UserRepository repository, Svrooij.Users.Api.Models.User user) =>
{
    return await repository.AddUserAsync(user);
}).WithName("AddUser").WithTags("Users").WithOpenApi()
    .ProducesValidationProblem()
    .Produces<User>();

app.MapPut("/users/{id:guid}", async (Svrooij.Users.Api.Service.UserRepository repository, string id, Svrooij.Users.Api.Models.User user) =>
{
    var updatedUser = await repository.UpdateUserAsync(id, user);
    if (updatedUser is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(updatedUser);
}).WithName("UpdateUser").WithTags("Users").WithOpenApi()
    .Produces<User>()
    .ProducesProblem(404);

app.MapDelete("/users/{id:guid}", async (Svrooij.Users.Api.Service.UserRepository repository, string id) =>
{
    var result = await repository.DeleteUserAsync(id);
    if (result)
    {
        return Results.Accepted();
    }
    return Results.NotFound();
}).WithName("DeleteUser").WithTags("Users").WithOpenApi()
.Produces(202)
.ProducesProblem(404);

app.MapGet("/weatherforecast", (int numberOfDays = 5) =>
{
    if (numberOfDays < 3)
    {
        return Results.BadRequest();
    }
    var forecast = Enumerable.Range(1, numberOfDays).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return Results.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithTags("Weather")
.WithOpenApi()
.Produces<IEnumerable<WeatherForecast>>()
.ProducesProblem(400);

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
