using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.ShareholderTypes
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public ShareholderType ShareholderType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shareholdertype = await _context.ShareholdersTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (shareholdertype is not null)
            {
                ShareholderType = shareholdertype;

                return Page();
            }

            return NotFound();
        }
    }
}
