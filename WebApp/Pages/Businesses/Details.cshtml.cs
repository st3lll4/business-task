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
        
        [BindProperty(SupportsGet = true)] public int BusinessId { get; set; }
        
        [BindProperty]public List<ShareholderInBusiness> ShareholderList { get; set; } = default!;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Business Business { get; set; } = default!;
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var business = await _context.Businesses.FirstOrDefaultAsync(m => m.Id == id);

            ShareholderList = await _context.ShareholdersInBusinesses.Include(s => s.Shareholder)
                .ThenInclude(s => s!.Person)
                .Include(s => s.Shareholder)
                .ThenInclude(s => s!.ShareholderBusiness)
                .Where(s => s.BusinessId == id)
                .ToListAsync();

            BusinessId = id.Value;

            if (business is null) return NotFound();
            Business = business;
            return Page();
        }
    }
}