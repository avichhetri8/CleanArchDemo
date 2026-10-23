using CleanArch.Domain.Entities;

namespace CleanArch.Application.Abstractions;

// The Application layer DEFINES the interface; Infrastructure IMPLEMENTS it.
// This is the dependency inversion that makes the whole thing "clean".
public interface ITodoRepository
{
    Task AddAsync(TodoItem item);
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<TodoItem>> GetAllAsync();
}
