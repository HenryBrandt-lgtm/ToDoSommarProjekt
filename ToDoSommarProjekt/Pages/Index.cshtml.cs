using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoSommarProjekt.Data;
using Microsoft.EntityFrameworkCore;
using ToDoSommarProjekt.ViewModels;

namespace ToDoSommarProjekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ToDoItemViewModel> ToDoItems { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public bool VisaKlara { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool VisaEjKlara { get; set; } = true;

        public async Task OnGetAsync()
        {
            var query = _context.ToDoItems.Include(t => t.Category).AsQueryable();

            if (VisaKlara && !VisaEjKlara)
                query = query.Where(t => t.IsCompleted);
            else if (!VisaKlara && VisaEjKlara)
                query = query.Where(t => !t.IsCompleted);
            else if (!VisaKlara && !VisaEjKlara)
                query = query.Where(t => false);

            ToDoItems = await query.OrderBy(t => t.IsCompleted).ThenBy(t => t.Deadline).Select(t => new ToDoItemViewModel
            {
                Id = t.Id,
                CategoryId = t.CategoryId,
                Title = t.Title,
                IsCompleted = t.IsCompleted,
                Deadline = t.Deadline,
                Category = t.Category
            }).ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem != null)
            {
                toDoItem.IsCompleted = !toDoItem.IsCompleted;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }
    }
}
