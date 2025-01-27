using ApiTodo.DataContext;
using ApiTodo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextTodo>(options => options.UseInMemoryDatabase("TodoList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get All 
app.MapGet("/todoItem", async (DbContextTodo db) => await db.Todos.ToListAsync());

// Get todoItem by Id 
app.MapGet("/todoItem/{id}", async (DbContextTodo db, int id) => await db.Todos.FindAsync(id));

// Post ( Create todoItem ) 
app.MapPost("/todoItem", async (DbContextTodo db, Todo todo) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoItem/{todo.Id}", todo);
});

app.Run();
