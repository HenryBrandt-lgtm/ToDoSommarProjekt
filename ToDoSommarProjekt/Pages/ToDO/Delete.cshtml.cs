using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoSommarProjekt.Data;

namespace ToDoSommarProjekt.Pages.ToDo
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ToDoItem ToDoItem { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToDoItem = await _context.ToDoItems.FindAsync(id);
            if (ToDoItem == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem != null)
            {
                _context.ToDoItems.Remove(toDoItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }
    }
}
