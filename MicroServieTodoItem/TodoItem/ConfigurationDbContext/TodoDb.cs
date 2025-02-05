using Microsoft.EntityFrameworkCore;
using TodoItem.Models;

namespace TodoItem.ConfigurationDbContext
{
    public class TodoDb: DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) 
            :base(options){}

        public DbSet<Todo> Todos { get; set; }
    }
}
