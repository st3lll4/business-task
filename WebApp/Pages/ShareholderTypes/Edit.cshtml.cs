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

namespace WebApp.Pages.ShareholderTypes
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShareholderType ShareholderType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareholdertype =  await _context.ShareholdersTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (shareholdertype == null)
            {
                return NotFound();
            }
            ShareholderType = shareholdertype;
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

            _context.Attach(ShareholderType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareholderTypeExists(ShareholderType.Id))
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

        private bool ShareholderTypeExists(int id)
        {
            return _context.ShareholdersTypes.Any(e => e.Id == id);
        }
    }
}
