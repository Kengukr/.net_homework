﻿namespace task_new.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BirthYear { get; set; }


        public ICollection<Book>? Books { get; set; }
    }
}