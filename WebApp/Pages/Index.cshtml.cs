using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private AppDbContext _context;

    [BindProperty(SupportsGet = true)] public string? Search { get; set; }
    public List<Business> Businesses { get; set; } = new();
    [BindProperty(SupportsGet = true)] public string? Message { get; set; }
    [BindProperty(SupportsGet = true)] public int? BusinessId { get; set; }


    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    private async Task<List<Business>> GetSearchResults(string searchTerm)
    {
        return await _context.Businesses
            .Include(b => b.ShareholdersInBusiness!)
            .ThenInclude(shareholdersInBusiness => shareholdersInBusiness.Shareholder)
            .ThenInclude(s => s.Person)
            .Include(b => b.ShareholdersInBusiness!)
            .ThenInclude(shareholdersInBusiness => shareholdersInBusiness.Shareholder)
            .ThenInclude(s => s.ShareholderBusiness)
            .Where(b =>
                b.BusinessName.ToLower().Contains(searchTerm) ||
                b.RegistryCode.ToLower().Contains(searchTerm) ||
                b.ShareholdersInBusiness!.Any(shareholdersInBusiness =>
                    shareholdersInBusiness.Shareholder!.Person != null &&
                    (
                        shareholdersInBusiness.Shareholder.Person.FirstName.ToLower().Contains(searchTerm) ||
                        shareholdersInBusiness.Shareholder.Person.LastName.ToLower().Contains(searchTerm) ||
                        shareholdersInBusiness.Shareholder.Person.IdCode.Contains(searchTerm)
                    )
                ) ||
                b.ShareholdersInBusiness!.Any(shareholdersInBusiness =>
                    shareholdersInBusiness.Shareholder!.ShareholderBusiness != null &&
                    (
                        shareholdersInBusiness.Shareholder.ShareholderBusiness.BusinessName.ToLower()
                            .Contains(searchTerm) ||
                        shareholdersInBusiness.Shareholder.ShareholderBusiness.RegistryCode.Contains(searchTerm)
                    )
                )
            )
            .ToListAsync();
    }

    public async Task<IActionResult> OnGet()
    {
        if (!string.IsNullOrEmpty(Search))
        {
            Businesses = await GetSearchResults(Search.Trim().ToLower());
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (Search != null) Search = Search.Trim().ToLower();

        Businesses = await GetSearchResults(Search);

        if (!Businesses.IsNullOrEmpty()) return Page();
        Message = "No businesses with such info were found!";
        return RedirectToPage("/Index",
            new { message = Message }
        );
    }
}