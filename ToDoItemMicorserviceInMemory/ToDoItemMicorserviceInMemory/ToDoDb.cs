using Microsoft.EntityFrameworkCore;

namespace ToDoItemMicorserviceInMemory
{
    public class ToDoDb:DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options):base(options) { }
        
        public DbSet<ToDoItem> ToDoItems { get; set; }   
    }
}
