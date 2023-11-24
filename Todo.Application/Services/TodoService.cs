using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Domain.Models;

namespace Todo.Application.Services;

public class TodoService : ITodoService
{
    private readonly TodoDb _dbContext;

    public TodoService(TodoDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Add(TodoDomain todo)
    {
        _dbContext.Todos.Add(todo);
        return await _dbContext.SaveChangesAsync();
    }

    public async ValueTask<TodoDomain?> Find(int id)
    {
        return await _dbContext.Todos.FindAsync(id);
    }

    public async Task<List<TodoDomain>> GetAll()
    {
        return await _dbContext.Todos.ToListAsync();
    }

    public Task<List<TodoDomain>> GetIncompleteTodos()
    {
        return _dbContext.Todos.Where(t => t.IsDone == false).ToListAsync();
    }

    public async Task Remove(TodoDomain todo)
    {
        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(TodoDomain todo)
    {
        _dbContext.Todos.Update(todo);
        await _dbContext.SaveChangesAsync();
    }
}
