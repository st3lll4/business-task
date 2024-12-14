using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace WebApp.Pages.ShareholdersInBusiness
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BusinessId"] = new SelectList(_context.Businesses, "Id", "BusinessName");
        ViewData["ShareholderId"] = new SelectList(_context.Shareholders, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public ShareholderInBusiness ShareholderInBusiness { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ShareholdersInBusinesses.Add(ShareholderInBusiness);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
