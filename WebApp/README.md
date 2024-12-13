dotnet aspnet-codegenerator razorpage -m Person -dc AppDbContext -udl -outDir Pages/Persons --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Business -dc AppDbContext -udl -outDir Pages/Businesses --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Shareholder -dc AppDbContext -udl -outDir Pages/Shareholders --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m ShareholderType -dc AppDbContext -udl -outDir Pages/ShareholderTypes --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m ShareholderInBusiness -dc AppDbContext -udl -outDir Pages/ShareholdersInBusiness --referenceScriptLibraries


