using System.ComponentModel.DataAnnotations;
using ToDoSommarProjekt.Data;

namespace ToDoSommarProjekt.ViewModels
{
    public class ToDoItemViewModel
    {

        [Required]
        public string Title { get; set; }

        public bool IsCompleted { get; set; } = false;

        public int CategoryId { get; set; }
        public DateOnly? Deadline { get; set; }

        public Category? Category { get; set; }

        public string Info { get; set; }
    }
}
