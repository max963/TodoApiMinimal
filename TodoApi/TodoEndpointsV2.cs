using Microsoft.AspNetCore.Http.HttpResults;
using Todo.Application.Services;
using Todo.Domain.Dtos;
using Todo.Domain.Models;

namespace TodoApi
{
    public static class TodoEndpointsV2
    {
        public static RouteGroupBuilder MapTodosApiV2(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/", GetAllTodos);
            groupBuilder.MapGet("/incomplete", GetInCompletedTodos);
            groupBuilder.MapGet("/{id}", GetTodo);
            groupBuilder.MapPost("/", CreateTodo).AddEndpointFilter(async (efiContext, next) =>
            {
                var param = efiContext.GetArgument<TodoItemDTO>(0);

                var validationErrors = Utilities.IsValid(param);

                if (validationErrors.Any())
                {
                    return Results.ValidationProblem(validationErrors);
                }

                return await next(efiContext);
            }); ;
            groupBuilder.MapPut("/{id}", UpdateTodo);
            groupBuilder.MapDelete("/{id}", DeleteTodo);

            return groupBuilder;
        }

        public static async Task<IResult> GetAllTodos(ITodoService todoService)
        {
            var todos = await todoService.GetAll();
            return TypedResults.Ok(todos);
        }

        public static async Task<IResult> GetInCompletedTodos(ITodoService todoService)
        {
            var incomplete = await todoService.GetIncompleteTodos();
            return TypedResults.Ok(incomplete);
        }

        public static async Task<Results<Ok<TodoDomain>, NotFound>> GetTodo(int id, ITodoService todoService)
        {
            var todo = await todoService.Find(id);

            if (todo != null)
            {
                return TypedResults.Ok(todo);
            }

            return TypedResults.NotFound();
        }

        public static async Task<IResult> CreateTodo(TodoItemDTO todoDto, ITodoService todoService)
        {

            var newTodo = new TodoDomain
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsDone = todoDto.IsDone
            };


            await todoService.Add(newTodo);


            return TypedResults.Created($"/{newTodo.Id}", todoDto);
        }

        public static async Task<IResult> UpdateTodo(int id, TodoDomain todo, ITodoService todoService)
        {
            var existingTodo = await todoService.Find(todo.Id);

            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;
                existingTodo.IsDone = todo.IsDone;

                await todoService.Update(existingTodo);

                return TypedResults.Created($"/todos/v1/{existingTodo.Id}", existingTodo);
            }

            return TypedResults.NotFound();
        }

        public static async Task<IResult> DeleteTodo(int id, ITodoService todoService)
        {
            var todo = await todoService.Find(id);

            if (todo != null)
            {
                await todoService.Remove(todo);
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
