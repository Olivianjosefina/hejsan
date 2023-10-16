using VinApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Lägg till services i behållaren.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Lägg till sessionshantering
builder.Services.AddDistributedMemoryCache(); // Använd memory cache för sessionsdata

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ange sessionstiden här
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Konfigurera HTTP-request-pipelinen.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapGet("/hi", () => "Hello!");

app.MapDefaultControllerRoute();
app.MapRazorPages();

// Aktivera sessionshantering
app.UseSession();

app.Run();
