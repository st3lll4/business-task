using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Shareholders
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
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

            var shareholder =  await _context.Shareholders.FirstOrDefaultAsync(m => m.Id == id);
            if (shareholder == null)
            {
                return NotFound();
            }
            Shareholder = shareholder;
           ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
           ViewData["ShareholderBusinessId"] = new SelectList(_context.Businesses, "Id", "BusinessName");
           ViewData["ShareholderTypeId"] = new SelectList(_context.ShareholdersTypes, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Shareholder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareholderExists(Shareholder.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ShareholderExists(int id)
        {
            return _context.Shareholders.Any(e => e.Id == id);
        }
    }
}
