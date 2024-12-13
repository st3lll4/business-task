using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private AppDbContext _context;
    
    [BindProperty] public string Search { get; set; } = default!;
    public List<Business> Businesses { get; set; } = new();

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        
        // Businesses = await _context.Businesses
        //     .Include(b => b.ShareholdersInBusiness!)
        //     .ThenInclude(ShareholdersInBusiness => ShareholdersInBusiness.Shareholder)
        //     .ThenInclude(s => s!.Person)
        //     .Include(b => b.ShareholdersInBusiness!)
        //     .ThenInclude(ShareholdersInBusiness => ShareholdersInBusiness.Shareholder)
        //     .ThenInclude(s => s!.Business)
        //     .ToListAsync();
    }

    public async Task<IActionResult> OnPost()
    {
        Search = Search.Trim().ToLower();
        
        Businesses = await _context.Businesses
            .Include(b => b.ShareholdersInBusiness!)
            .ThenInclude(shareholdersInBusiness => shareholdersInBusiness.Shareholder)
            .ThenInclude(s => s.Person)
            .Include(b => b.ShareholdersInBusiness!)
            .ThenInclude(shareholdersInBusiness => shareholdersInBusiness.Shareholder)
            .ThenInclude(s => s.ShareholderBusiness)
            .Where(b =>
                b.BusinessName.ToLower().Contains(Search) ||
                b.RegistryCode.ToLower().Contains(Search) ||
                b.ShareholdersInBusiness!.Any(shareholdersInBusiness => 
                    shareholdersInBusiness.Shareholder!.Person != null && 
                    (
                        shareholdersInBusiness.Shareholder.Person.FirstName.ToLower().Contains(Search) ||
                        shareholdersInBusiness.Shareholder.Person.LastName.ToLower().Contains(Search) ||
                        shareholdersInBusiness.Shareholder.Person.IdCode.Contains(Search)
                    )
                ) ||
                b.ShareholdersInBusiness!.Any(shareholdersInBusiness => 
                    shareholdersInBusiness.Shareholder!.ShareholderBusiness != null && 
                    (
                        shareholdersInBusiness.Shareholder.ShareholderBusiness.BusinessName.ToLower().Contains(Search) ||
                        shareholdersInBusiness.Shareholder.ShareholderBusiness.RegistryCode.Contains(Search)
                    )
                )
            )
            .ToListAsync();
            
        return Page();
    }
}