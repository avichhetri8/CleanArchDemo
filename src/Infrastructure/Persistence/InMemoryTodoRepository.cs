using System.Collections.Concurrent;
using CleanArch.Application.Abstractions;
using CleanArch.Domain.Entities;

namespace CleanArch.Infrastructure.Persistence;

// Swap this for an EF Core / Dapper repository without touching Domain or Application.
public class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<Guid, TodoItem> _store = new();

    public Task AddAsync(TodoItem item)
    {
        _store[item.Id] = item;
        return Task.CompletedTask;
    }

    public Task<TodoItem?> GetByIdAsync(Guid id)
        => Task.FromResult(_store.GetValueOrDefault(id));

    public Task<IReadOnlyList<TodoItem>> GetAllAsync()
        => Task.FromResult<IReadOnlyList<TodoItem>>(_store.Values.ToList());
}
