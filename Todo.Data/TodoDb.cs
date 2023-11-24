using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;

namespace Todo.Data;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options): base(options) { }

    public DbSet<TodoDomain> Todos => Set<TodoDomain>();
}