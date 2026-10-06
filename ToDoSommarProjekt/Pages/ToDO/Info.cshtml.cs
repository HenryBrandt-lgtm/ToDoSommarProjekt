using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoSommarProjekt.Data;
using ToDoSommarProjekt.ViewModels;

namespace ToDoSommarProjekt.Pages.ToDO
{
    public class InfoModel(ApplicationDbContext db) : PageModel
    {
        public ToDoItem? ToDoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToDoItem = await db.ToDoItems.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);

            if (ToDoItem == null)
            {
                NotFound();
            }
            return Page();
        }
    }
}
