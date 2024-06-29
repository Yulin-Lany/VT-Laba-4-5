using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stseniayeva.Domain.Entities;
using Stseniayeva.UI.Data;
using Steniayeva.API.Data;

namespace Stseniayeva.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Stseniayeva.UI.Data.AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public CreateModel(Steniayeva.API.Data.AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }
        public IActionResult OnGet()
        {
            
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "GroupName");
            return Page();
        }
        [BindProperty]
        public Moto Moto { get; set; } = default!;
        [BindProperty]
        public IFormFile? Images { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Motos.Add(Moto);
            await _context.SaveChangesAsync();
            if (Images != null)
            {
                var fileName = $"{Moto.Id}" +
                Path.GetExtension(Images.FileName);
                Moto.Images = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Images.CopyToAsync(fStream);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
