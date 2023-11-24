using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Data
{
    public static class InstallData
    {
        public static void RegisterData(this IServiceCollection services)
        {
            services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
        }
    }
}
