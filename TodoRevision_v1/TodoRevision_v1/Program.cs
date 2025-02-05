using Microsoft.EntityFrameworkCore;
using TodoRevision_v1;

var builder = WebApplication.CreateBuilder(args);

// add DI 
builder.Services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("db_List"));

var app = builder.Build();

// add service pipline ( Methods ) 

app.MapGet("/item", async (ToDoContext db) => await db.Todos.ToListAsync());

app.MapGet("/item/{id}", async (ToDoContext db, int id) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo) return Results.Ok(todo);
    return Results.NotFound($"Item with parameter {id} not found in the server!!");
});


app.MapPost("/item", async (ToDoContext db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/item/{todo.Id}", todo);
});

app.MapPut("/item/{id}", async(ToDoContext db, Todo todo) =>
{
    var todoOptional = await db.Todos.FindAsync(todo.Id);

    if( todoOptional == null )
    {
        return Results.NotFound($"item to be updated not found in the server!!");
    } else
    {
        todoOptional.Id = todo.Id;
        todoOptional.Title = todo.Title;
        todoOptional.IsCompleted = todo.IsCompleted;

        db.Todos.Add(todoOptional);
        await db.SaveChangesAsync();
        return Results.Ok(todoOptional);
    }
    
});


app.MapDelete("/item/{id}", async (ToDoContext db, int id) =>
{
    var todoOptional = await db.Todos.FindAsync(id);
    if (todoOptional == null) return Results.NotFound($"item t deleted not found in the server!!");

    db.Todos.Remove(todoOptional);
    await db.SaveChangesAsync();
    return Results.Ok("item deleted with successfull.");
});

app.Run();
