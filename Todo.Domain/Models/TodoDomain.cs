namespace Todo.Domain.Models;

public class TodoDomain
{
    public TodoDomain() { }
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}