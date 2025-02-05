using Microsoft.EntityFrameworkCore;

namespace TodoRevision_v1
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) {}
        public DbSet<Todo> Todos { get; set; }
    }
}
