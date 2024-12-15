using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class CreateBusiness : PageModel
{
    private readonly AppDbContext _context;

    [Required(ErrorMessage = "Fill all fields!")]
    [BindProperty]
    public string BusinessName { get; set; } = default!;

    [Required(ErrorMessage = "Fill all fields!")]
    [BindProperty]
    public string RegistryCode { get; set; } = default!;

    [Required(ErrorMessage = "Fill all fields!")]
    [BindProperty]
    public DateTime FoundingDate { get; set; }

    [Required(ErrorMessage = "Fill all fields!")]
    [Range(2500, int.MaxValue, ErrorMessage = "Capital has to be at least 2500 euros")]
    [BindProperty]
    public int TotalCapital { get; set; }

    public SelectList ShareholderSelectList { get; set; } = default!;

    [BindProperty(SupportsGet = true)] public string? Message { get; set; } = default!;

    [Required(ErrorMessage = "Add at least one shareholder!")]
    [BindProperty]
    public string Shareholder1 { get; set; } = default!;

    [BindProperty] public string? Shareholder2 { get; set; }

    [BindProperty] public string? Shareholder3 { get; set; }

    [BindProperty] public string? Shareholder4 { get; set; }

    [Required(ErrorMessage = "Add share for one shareholder!")]
    [BindProperty]
    public int? Shareholder1Share { get; set; }

    [BindProperty] public int? Shareholder2Share { get; set; }

    [BindProperty] public int? Shareholder3Share { get; set; }

    [BindProperty] public int? Shareholder4Share { get; set; }
    
    [BindProperty(SupportsGet = true)] public bool ShowSweetAlert { get; set; }
    
    [BindProperty(SupportsGet = true)] public int? BusinessId { get; set; }


    public CreateBusiness(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGet()
    {
        FoundingDate = DateTime.Today;

        await GetShareholders();
        ModelState.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {

        if (string.IsNullOrWhiteSpace(BusinessName) || BusinessName.Length is < 3 or > 100 )
        {
            ModelState.AddModelError("BusinessName",
                "Name has to be 3-100 characters.");
        }

        if (string.IsNullOrWhiteSpace(RegistryCode) || RegistryCode.Length != 7)
        {
            ModelState.AddModelError("RegistryCode",
                "Registry code has to be 7 digits.");
        }

        if (string.IsNullOrWhiteSpace(Shareholder1))
        {
            ModelState.AddModelError("Shareholder1",
                "Add at least one shareholder!");
        }

        if (FoundingDate > DateTime.Today)
        {
            ModelState.AddModelError("FoundingDate",
                "Founding date has to be today or earlier.");
        }

        if (TotalCapital is 0 or < 2500)
        {
            ModelState.AddModelError("TotalCapital",
                "Capital has to be at least 2500 euros.");
        }

        if (Shareholder1Share is null or < 1)
        {
            ModelState.AddModelError("Shareholder1Share",
                "Shareholder has to have at 1 euro in shares.");
        }

        if (!string.IsNullOrWhiteSpace(Shareholder2) && Shareholder2Share is null or < 1)
        {
            ModelState.AddModelError("Shareholder2Share",
                "Shareholder has to have at 1 euro in shares.");
        }

        if (!string.IsNullOrWhiteSpace(Shareholder3) && Shareholder3Share is null or < 1)
        {
            ModelState.AddModelError("Shareholder3Share",
                "Shareholder has to have at 1 euro in shares.");
        }

        if (!string.IsNullOrWhiteSpace(Shareholder4) && Shareholder4Share is null or < 1)
        {
            ModelState.AddModelError("Shareholder4Share",
                "Shareholder has to have at 1 euro in shares.");
        }
        

        if (Shareholder1Share + Shareholder2Share + Shareholder3Share + Shareholder4Share < TotalCapital)
        {
            ModelState.AddModelError("ShareholderShares",
                $"Total shares have to be equal to {TotalCapital}.");
        }

        if (ModelState.IsValid)
        {
            var newBusiness = new Business
            {
                BusinessName = BusinessName,
                RegistryCode = RegistryCode,
                FoundingDate = FoundingDate,
                TotalCapital = TotalCapital
            };

            await _context.Businesses.AddAsync(newBusiness);
            await _context.SaveChangesAsync(); 

            var shareholders = new List<(string Name, int? Share)>
            {
                (Shareholder1, Shareholder1Share),
                (Shareholder2, Shareholder2Share),
                (Shareholder3, Shareholder3Share),
                (Shareholder4, Shareholder4Share)
            }.Where(s => !string.IsNullOrWhiteSpace(s.Name)).ToList();

            foreach (var shareholder in shareholders)
            {
                await _context.ShareholdersInBusinesses.AddAsync(new ShareholderInBusiness
                {
                    IsFounder = true,
                    BusinessId = newBusiness.Id,
                    ShareholderId = await _context.Shareholders
                        .Where(s =>
                            (s.Person != null && s.Person.FirstName + " " + s.Person.LastName == shareholder.Name) ||
                            (s.ShareholderBusiness != null && s.ShareholderBusiness.BusinessName == shareholder.Name))
                        .Select(s => s.Id)
                        .SingleOrDefaultAsync(),
                    ShareCapital = shareholder.Share!.Value
                });

                
                await _context.Shareholders.AddAsync(new Shareholder
                {
                    PersonId = await _context.Persons
                        .Where(p => p.FirstName + " " + p.LastName == shareholder.Name)
                        .Select(p => p.Id)
                        .SingleOrDefaultAsync(),
                    ShareholderBusinessId = await _context.Businesses
                        .Where(b => b.BusinessName == shareholder.Name)
                        .Select(b => b.Id)
                        .SingleOrDefaultAsync()
                });
            }

            await _context.SaveChangesAsync();
            ShowSweetAlert = true;
            return RedirectToPage("/CreateBusiness", new
            {
                showSweetAlert = ShowSweetAlert,
                businessId = newBusiness.Id
            } );
        }

        await GetShareholders();
        Message = "Something went wrong";
        return Page();
    }
    
    private async Task GetShareholders()
    {
        var persons = await _context.Persons
            .Select(p => p.FirstName + " " + p.LastName)
            .ToListAsync();

        var businesses = await _context.Businesses
            .Select(b => b.BusinessName)
            .ToListAsync();

        if (persons.Count == 0 && businesses.Count == 0)
        {
            Message = "Add some shareholders to database first!";
        }

        var selectListData = new List<string> { " " };
        selectListData.AddRange(persons.Concat(businesses));

        ShareholderSelectList = new SelectList(selectListData);
    }
}