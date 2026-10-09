using BMPClassLibrary.Repository;
using BMPClassLibrary.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Singletons so in-memory lists survive between requests
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<BookingRepository>();
// Later, when you add services:
// builder.Services.AddSingleton<IBookingService, BookingService>();
builder.Services.AddSingleton<TableRepository>(serviceProvider =>
{
    var tableRepo = new TableRepository();

    // Initialize sample tables
    tableRepo.AddTable(new Table { TableId = 1, Capacity = 2, IsAvailable = true });
    tableRepo.AddTable(new Table { TableId = 2, Capacity = 2, IsAvailable = true });
    tableRepo.AddTable(new Table { TableId = 3, Capacity = 4, IsAvailable = true });
    tableRepo.AddTable(new Table { TableId = 4, Capacity = 4, IsAvailable = true });
    tableRepo.AddTable(new Table { TableId = 5, Capacity = 6, IsAvailable = true });
    tableRepo.AddTable(new Table { TableId = 6, Capacity = 8, IsAvailable = true });

    return tableRepo;
});

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