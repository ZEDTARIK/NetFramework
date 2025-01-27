namespace ApiTodo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
