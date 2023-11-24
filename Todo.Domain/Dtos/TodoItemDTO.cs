
namespace Todo.Domain.Dtos;

public record TodoItemDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }

    public TodoItemDTO() { }
}