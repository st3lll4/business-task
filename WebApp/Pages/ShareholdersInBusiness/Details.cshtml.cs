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
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
