using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Shareholders
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shareholder Shareholder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareholder = await _context.Shareholders.FirstOrDefaultAsync(m => m.Id == id);

            if (shareholder is not null)
            {
                Shareholder = shareholder;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareholder = await _context.Shareholders.FindAsync(id);
            if (shareholder != null)
            {
                Shareholder = shareholder;
                _context.Shareholders.Remove(Shareholder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
