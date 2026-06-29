using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoSommarProjekt.Data;

namespace ToDoSommarProjekt.Pages.CategoriesPages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }
        public  void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return Redirect(ReturnUrl ?? "/CategoriesPages/Index");
        }
}
}
