using ApiTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTodo.DataContext
{
    public class DbContextTodo: DbContext
    {
        public DbContextTodo(DbContextOptions<DbContextTodo> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
