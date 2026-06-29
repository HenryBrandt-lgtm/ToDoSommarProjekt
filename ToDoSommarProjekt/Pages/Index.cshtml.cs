using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoSommarProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoSommarProjekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ToDoItem> ToDoItems { get; set; }

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

            ToDoItems = await query.OrderBy(t => t.IsCompleted).ToListAsync();
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
