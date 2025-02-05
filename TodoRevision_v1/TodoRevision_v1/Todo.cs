namespace TodoRevision_v1
{
    public class Todo
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? Title { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
