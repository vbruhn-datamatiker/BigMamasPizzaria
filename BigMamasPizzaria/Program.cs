using BMPClassLibrary.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Singletons so in-memory lists survive between requests
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<BookingRepository>();
// Later, when you add services:
// builder.Services.AddSingleton<IBookingService, BookingService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();