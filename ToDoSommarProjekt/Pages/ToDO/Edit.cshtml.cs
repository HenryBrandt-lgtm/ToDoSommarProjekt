using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoSommarProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoSommarProjekt.Pages.ToDo
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ToDoItem ToDoItem { get; set; }

        public SelectList Categories { get; set; }
        public async Task OnGetAsync(int id)
        {
            ToDoItem = await _context.ToDoItems.FindAsync(id);
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
                return Page();
            }
            _context.ToDoItems.Update(ToDoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
