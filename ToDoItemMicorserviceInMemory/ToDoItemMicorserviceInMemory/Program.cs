using Microsoft.EntityFrameworkCore;
using ToDoItemMicorserviceInMemory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ToDoDb>(options => options.UseInMemoryDatabase("ToDoList"));

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

// Get todoItems
app.MapGet("/todoitems", async(ToDoDb db) => await db.ToDoItems.ToListAsync());
app.MapGet("/todoitems/{id}", async (int id, ToDoDb db) => await db.ToDoItems.FindAsync(id));
//Post TodoItems
app.MapPost("/todoitems", async (ToDoItem toDoItem,ToDoDb db) => 
{
    db.ToDoItems.Add(toDoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{toDoItem.id}", toDoItem);
});

app.Run();

