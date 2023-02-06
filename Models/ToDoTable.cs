using System;
using System.Collections.Generic;

namespace ToDoApi.Models
{
    public partial class ToDoTable
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Completed { get; set; }
        public bool Deleted { get; set; }
    }
}
