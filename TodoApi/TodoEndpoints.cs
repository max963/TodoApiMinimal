using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Domain.Dtos;
using Todo.Domain.Models;

namespace TodoApiMinimal
{
    public static class TodoEndpoints
    {
        public static RouteGroupBuilder MapTodoListApi(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/", GetAllTodos);
            groupBuilder.MapGet("/complete", GetCompletedTodos);
            groupBuilder.MapGet("/{id}", GetTodo);
            groupBuilder.MapPost("/", CreateTodo);
            groupBuilder.MapPut("/{id}", UpdateTodo);
            groupBuilder.MapDelete("/{id}", DeleteTodo);

            return groupBuilder;
        }

        static async Task<IResult> GetAllTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.ToArrayAsync());
        }

        static async Task<IResult> GetCompletedTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Where(t => t.IsDone).ToListAsync());
        }

        static async Task<IResult> GetTodo(int id, TodoDb db)
        {
            return await db.Todos.FindAsync(id)
                is TodoDomain todo
                ? TypedResults.Ok(todo)
                : TypedResults.NotFound();
        }

        static async Task<IResult> CreateTodo(TodoItemDTO todoDto, TodoDb db)
        {
            var todo = new TodoDomain
            {
                IsDone = todoDto.IsDone,
                Title = todoDto.Title,
                Description = todoDto.Description
            };

            db.Todos.Add(todo);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/{todo.Id}", todoDto);
        }

        static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoDto, TodoDb db)
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return TypedResults.NotFound();

            todo.Title = todoDto.Title;
            todo.Description = todoDto.Description;
            todo.IsDone = todoDto.IsDone;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        static async Task<IResult> DeleteTodo(int id, TodoDb db)
        {
            if (await db.Todos.FindAsync(id) is TodoDomain todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }

    }
}