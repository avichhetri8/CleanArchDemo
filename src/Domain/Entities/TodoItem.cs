namespace CleanArch.Domain.Entities;

// Core business rules live here. No EF, no ASP.NET, no external packages.
public class TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsDone { get; private set; }

    private TodoItem() { }

    public static TodoItem Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        return new TodoItem { Id = Guid.NewGuid(), Title = title.Trim() };
    }

    public void MarkDone() => IsDone = true;
}
