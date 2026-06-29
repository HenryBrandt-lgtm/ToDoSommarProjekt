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

        public async Task OnGetAsync()
        {
            ToDoItems = await _context.ToDoItems.Include(t => t.Category).ToListAsync();
        }
    }
}