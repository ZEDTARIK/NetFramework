using Microsoft.EntityFrameworkCore;
using TodoItem.ConfigurationDbContext;
using TodoItem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add DI Dbcontext
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// add pipline - UseMethod

app.MapGet("/todoitems", async(TodoDb db) => await db.Todos.ToListAsync());

app.MapGet("/todoitems/{id}", async (TodoDb db, int id) => 
    await db.Todos.FindAsync(id));

app.MapPost("/todoitems", async (TodoDb db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (TodoDb db, int id, Todo todo) =>
{
    var todoEdit = await db.Todos.FindAsync(id);
    if (todoEdit == null)
    {
        return Results.NotFound();
    }
    else
    {
        todoEdit.Title = todo.Title;
        todoEdit.IsCompleted = todo.IsCompleted;
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
});

app.MapDelete("/todoitems/{id}", async (TodoDb db, int id) =>
{
    if( await db.Todos.FindAsync(id) is Todo todo )
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

// Run Application
app.Run();


