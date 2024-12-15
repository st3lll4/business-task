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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Shareholder> Shareholder { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Shareholder = await _context.Shareholders
                .Include(s => s.Person)
                .Include(s => s.ShareholderBusiness).ToListAsync();
        }
    }
}
