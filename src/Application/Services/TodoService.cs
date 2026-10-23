using CleanArch.Application.Abstractions;
using CleanArch.Application.DTOs;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Services;

// Use cases: orchestrates domain objects via abstractions.
// Knows nothing about HTTP, databases, or frameworks.
public class TodoService
{
    private readonly ITodoRepository _repo;
    public TodoService(ITodoRepository repo) => _repo = repo;

    public async Task<TodoDto> CreateAsync(CreateTodoRequest request)
    {
        var item = TodoItem.Create(request.Title);
        await _repo.AddAsync(item);
        return ToDto(item);
    }

    public async Task<IReadOnlyList<TodoDto>> GetAllAsync()
        => (await _repo.GetAllAsync()).Select(ToDto).ToList();

    public async Task<TodoDto?> CompleteAsync(Guid id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item is null) return null;
        item.MarkDone();
        return ToDto(item);
    }

    private static TodoDto ToDto(TodoItem t) => new(t.Id, t.Title, t.IsDone);
}
