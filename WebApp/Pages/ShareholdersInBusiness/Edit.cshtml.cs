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

namespace WebApp.Pages.ShareholdersInBusiness
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ShareholderInBusiness ShareholderInBusiness { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareholderinbusiness =  await _context.ShareholdersInBusinesses.FirstOrDefaultAsync(m => m.Id == id);
            if (shareholderinbusiness == null)
            {
                return NotFound();
            }
            ShareholderInBusiness = shareholderinbusiness;
           ViewData["BusinessId"] = new SelectList(_context.Businesses, "Id", "BusinessName");
           ViewData["ShareholderId"] = new SelectList(_context.Shareholders, "Id", "Id");
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

            _context.Attach(ShareholderInBusiness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareholderInBusinessExists(ShareholderInBusiness.Id))
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

        private bool ShareholderInBusinessExists(int id)
        {
            return _context.ShareholdersInBusinesses.Any(e => e.Id == id);
        }
    }
}
