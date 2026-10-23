using CleanArch.Application;
using CleanArch.Application.DTOs;
using CleanArch.Application.Services;
using CleanArch.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Composition root: the only place that knows about ALL layers.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapGet("/todos", (TodoService svc) => svc.GetAllAsync());

app.MapPost("/todos", async (CreateTodoRequest req, TodoService svc) =>
{
    try
    {
        var created = await svc.CreateAsync(req);
        return Results.Created($"/todos/{created.Id}", created);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/todos/{id:guid}/complete", async (Guid id, TodoService svc) =>
    await svc.CompleteAsync(id) is { } todo ? Results.Ok(todo) : Results.NotFound());

app.Run();
