using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.ShareholdersInBusiness
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
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

            var shareholderinbusiness = await _context.ShareholdersInBusinesses.FirstOrDefaultAsync(m => m.Id == id);

            if (shareholderinbusiness is not null)
            {
                ShareholderInBusiness = shareholderinbusiness;

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

            var shareholderinbusiness = await _context.ShareholdersInBusinesses.FindAsync(id);
            if (shareholderinbusiness != null)
            {
                ShareholderInBusiness = shareholderinbusiness;
                _context.ShareholdersInBusinesses.Remove(ShareholderInBusiness);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
