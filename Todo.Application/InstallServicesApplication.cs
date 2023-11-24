using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;

namespace Todo.Application
{
    public static class InstallServicesApplication
    {
        public static void InstallAppServices(this IServiceCollection services)
        {
            services.AddTransient<ITodoService, TodoService>();
        }
    }
}
