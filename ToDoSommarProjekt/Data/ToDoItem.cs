using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoSommarProjekt.Data
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateOnly CreatedAt { get; set; }

        public DateOnly? Deadline { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
