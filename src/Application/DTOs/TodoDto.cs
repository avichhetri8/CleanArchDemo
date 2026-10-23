namespace CleanArch.Application.DTOs;

public record TodoDto(Guid Id, string Title, bool IsDone);
public record CreateTodoRequest(string Title);
