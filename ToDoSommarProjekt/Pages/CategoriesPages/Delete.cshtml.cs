using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoSommarProjekt.Data;

namespace ToDoSommarProjekt.Pages.CategoriesPages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Category Category { get; set; }
        public async Task OnGetAsync(int id)
        {
            Category = await _context.Categories.FindAsync(id);
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/categoriesPages/Index");
        }
    }
}
