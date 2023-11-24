using Todo.Application;
using Todo.Data;
using TodoApiMinimal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterData();
builder.Services.InstallAppServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
});

var app = builder.Build();

app.MapGroup("/todoitems")
    .MapTodoListApi()
    .WithTags("Todo Endpoints");



app.Run();
