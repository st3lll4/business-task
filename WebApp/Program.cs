using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


if (!Directory.Exists(FileHelper.BasePath))
{
    Directory.CreateDirectory(FileHelper.BasePath);
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

connectionString = connectionString.Replace("<%location%>", FileHelper.BasePath);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    AppDbContextInitializer.InitializeDb(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages().WithStaticAssets();
app.Run();