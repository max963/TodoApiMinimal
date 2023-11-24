using Todo.Domain.Models;

namespace Todo.Application.Services;

public interface ITodoService
{
    Task<List<TodoDomain>> GetAll();

    Task<List<TodoDomain>> GetIncompleteTodos();

    ValueTask<TodoDomain?> Find(int id);

    Task<int> Add(TodoDomain todo);

    Task Update(TodoDomain todo);

    Task Remove(TodoDomain todo);
}
