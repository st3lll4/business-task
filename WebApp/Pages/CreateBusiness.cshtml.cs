using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

// Osaühingu asutamise vorm – Vormil peab saama sisestada uue osaühingu järgnevaid andmeid:
//     • Nimi(3 kuni 100 tähte või numbrit)
//     • Registrikood(7 numbrit)
//     • Asutamiskuupäev(ilma kellaajata, väiksem või võrdne käesoleva kuupäevaga)
//     • Kogukapitali suurus eurodes(täisarv, vähemalt 2500)
//     • Osanike andmed, igaühe kohta:
//     • Osanikuks olev olemasolev füüsiline või juriidiline isik(võimalus otsida isikuid);
//     • Osaniku osa suurus eurodes(täisarv, vähemalt
// 1).
//
// Asutamise vormil märgitud osanikud märgitakse automaatselt asutajateks.
//     Osanike osade suuruste summa peab olema võrdne osaühingu kogukapitali suurusega.
//     Vormil peab olema salvestamise nupp, mis õnnestumisel viib osaühingu andmete vaatele.

public class CreateBusiness : PageModel
{
    [BindProperty] public string BusinessName { get; set; } = default!;

    [BindProperty] public string RegistryCode { get; set; } = default!;

    [BindProperty] public DateTime FoundingDate { get; set; }

    [BindProperty] public int TotalCapital { get; set; }

    public SelectList ShareholderSelectList { get; set; } = default!;

    private readonly AppDbContext _context;
    
    [BindProperty] public string Message { get; set; } = default!;

    [BindProperty] public string Shareholder1 { get; set; } = default!;
    [BindProperty] public string? Shareholder2 { get; set; } = default!;

    [BindProperty] public string? Shareholder3 { get; set; } = default!;

    [BindProperty] public string? Shareholder4 { get; set; } = default!;
    
    [BindProperty] public int? Shareholder1Share { get; set; }
    [BindProperty] public int? Shareholder2Share { get; set; }
    [BindProperty] public int? Shareholder3Share { get; set; }
    [BindProperty] public int? Shareholder4Share { get; set; }
    

    public CreateBusiness(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGet()
    {
        FoundingDate = DateTime.Today;

        var persons = await _context.Persons
            .Select(p => p.FirstName + " " + p.LastName)
            .ToListAsync();

        var businesses = await _context.Businesses
            .Select(b => b.BusinessName)
            .ToListAsync();

        if (persons.Count == 0 && businesses.Count == 0)
        {
            Message = "Add some shareholders to database first!";
            return Page();
        }
        
        var selectListData = new List<string> { " " };
        selectListData.AddRange(persons.Concat(businesses));

        ShareholderSelectList = new SelectList(selectListData);

        return Page();
    }

    public IActionResult OnPost()
    {
        return Page();
    }
}