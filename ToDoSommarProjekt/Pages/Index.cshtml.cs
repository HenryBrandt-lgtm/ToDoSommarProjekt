using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoSommarProjekt.Data;
using Microsoft.EntityFrameworkCore;
using ToDoSommarProjekt.ViewModels;

namespace ToDoSommarProjekt.Pages
{
    public class IndexModel(ApplicationDbContext context) : PageModel
    {
        public List<ToDoItemViewModel> ToDoItems { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public bool VisaKlara { get; set; } = true;

        [BindProperty(SupportsGet = true)]
        public bool VisaEjKlara { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        private const int PageSize = 5;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            PageNumber = pageNumber;

            var query = context.ToDoItems.Include(t => t.Category).AsQueryable();

            if (VisaKlara && !VisaEjKlara)
                query = query.Where(t => t.IsCompleted);
            else if (!VisaKlara && VisaEjKlara)
                query = query.Where(t => !t.IsCompleted);
            else if (!VisaKlara && !VisaEjKlara)
                query = query.Where(t => false);

            var totalItems = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            
            ToDoItems = await query.OrderBy(t => t.IsCompleted)
                .ThenBy(t => t.Deadline)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(t => new ToDoItemViewModel
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
            var toDoItem = await context.ToDoItems.FindAsync(id);
            if (toDoItem != null)
            {
                toDoItem.IsCompleted = !toDoItem.IsCompleted;
                await context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }
    }
}
