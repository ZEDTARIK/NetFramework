﻿namespace ToDoItemMicorserviceInMemory
{
    public class ToDoItem
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public bool? isCompleted { get; set; }
    }
}
