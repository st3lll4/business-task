using Domain;

namespace DAL;

public static class AppDbContextInitializer
{
    public static void InitializeDb(AppDbContext context)
    {
        if (context.Persons.Any() || context.Businesses.Any()) return;
        

        var persons = new[]
        {
            new Person { FirstName = "Tiina", LastName = "Miina", IdCode = "48507083221" },
            new Person { FirstName = "Jaana", LastName = "Lind", IdCode = "49001018742" },
            new Person { FirstName = "Leiutajateküla", LastName = "Lotte", IdCode = "60103310291" }
        };
        context.Persons.AddRange(persons);
        context.SaveChanges();

        var businesses = new[]
        {
            new Business
            {
                BusinessName = "OÜ Building Solutions",
                RegistryCode = "1234567",
                FoundingDate = new DateTime(2020, 4, 6),
                TotalCapital = 2500
            },
            new Business
            {
                BusinessName = "MEM Cafe OÜ",
                RegistryCode = "1010101",
                FoundingDate = new DateTime(2021, 1, 15),
                TotalCapital = 5000
            }
        };
        context.Businesses.AddRange(businesses);
        context.SaveChanges();

        var personShareholders = persons.Select(p => new Shareholder
        {
            Person = p,
        }).ToList();
        context.Shareholders.AddRange(personShareholders);
        context.SaveChanges();

        var businessShareholders = businesses.Select(b => new Shareholder
        {
            ShareholderBusiness = b,
        }).ToList();
        context.Shareholders.AddRange(businessShareholders);
        context.SaveChanges();

        var shareholderInBusiness = new[]
        {
            new ShareholderInBusiness
            {
                Business = businesses[0],
                Shareholder = personShareholders[0],
                ShareCapital = 1500,
                IsFounder = true
            },
            new ShareholderInBusiness
            {
                Business = businesses[0],
                Shareholder = personShareholders[1],
                ShareCapital = 1000
            },
            new ShareholderInBusiness
            {
                Business = businesses[1],
                Shareholder = businessShareholders[0],
                ShareCapital = 5000,
                IsFounder = true
            }
        };
        context.ShareholdersInBusinesses.AddRange(shareholderInBusiness);
        context.SaveChanges();
    }
}