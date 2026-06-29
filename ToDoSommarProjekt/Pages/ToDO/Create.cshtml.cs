using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoSommarProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoSommarProjekt.Pages.ToDo
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ToDoItem ToDoItem { get; set; }
        public SelectList Categories { get; set; }
        public async Task OnGetAsync()
        {
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ToDoItem.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ToDoItems.Add(ToDoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
