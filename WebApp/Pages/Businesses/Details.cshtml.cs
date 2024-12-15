using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace WebApp.Pages.Businesses
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Business Business { get; set; } = default!;

        public List<Shareholder> PersonShareholders { get; set; } = default!;

        public List<Shareholder> BusinessShareholders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Businesses.FirstOrDefaultAsync(m => m.Id == id);

            var shareholderList = await _context.ShareholdersInBusinesses
                .Include(s => s.Shareholder)
                .ThenInclude(s => s.Person)
                .Include(s => s.Shareholder)
                .ThenInclude(s => s.ShareholderBusiness)
                .Where(s => s.BusinessId == id)
                .ToListAsync();

            PersonShareholders = new List<Shareholder>();
            BusinessShareholders = new List<Shareholder>();
            
            foreach (var shareholder in shareholderList)
            {
                if (shareholder.Shareholder == null) continue;
                //proovi kuidagi shareholdereid displayda 
            }

            if (business is null) return NotFound();
            Business = business;
            return Page();
        }
    }
}