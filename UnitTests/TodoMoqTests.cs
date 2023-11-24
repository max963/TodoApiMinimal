using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.Services;
using Todo.Domain.Models;
using TodoApi;

namespace UnitTests
{
    public class TodoMoqTests
    {
        [Fact]
        public async Task GetTodoReturnsNotFoundIfNotExists()
        {
            // arrange
            var mock = new Mock<ITodoService>();

            mock.Setup(m => m.Find(It.Is<int>(id => id == 1)))
                .ReturnsAsync((TodoDomain?)null);

            // act
            var result = await TodoEndpointsV2.GetTodo(1, mock.Object);

            // assert
            Assert.IsType<Results<Ok<TodoDomain>, NotFound>>(result);

            var notFoundResult = (NotFound)result.Result;

            Assert.NotNull(notFoundResult);
        }
    }
}